using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Models.Account;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Utilities;
using Logistics.WinFormsUI.Forms;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucMyProfile : UserControl
    {
        private readonly User _user;
        private TextBox txtEmail = null!;
        private TextBox txtPhone = null!;
        private TextBox txtAddress = null!;
        private TextBox txtAvatarPath = null!;

        public ucMyProfile(User user)
        {
            _user = user;
            InitializeComponent();
            AddContactFields(profileCard);
            UIStyleHelper.ApplyGridViewStyle(dgvWork);
            LoadProfile();
        }

        private void Header_Resize(object? sender, EventArgs e)
        {
            btnChangePassword.Left = header.Width - 328;
            btnSave.Left = header.Width - 168;
        }

        private void AddContactFields(Panel card)
        {
            txtEmail = CreateTextBox(58);
            txtPhone = CreateTextBox(104);
            txtAddress = CreateTextBox(150);
            txtAvatarPath = CreateTextBox(196);

            card.Controls.Add(CreateLabel("Email", 58));
            card.Controls.Add(txtEmail);
            card.Controls.Add(CreateLabel("Số điện thoại", 104));
            card.Controls.Add(txtPhone);
            card.Controls.Add(CreateLabel("Địa chỉ", 150));
            card.Controls.Add(txtAddress);
            card.Controls.Add(CreateLabel("Ảnh đại diện", 196));
            card.Controls.Add(txtAvatarPath);
        }

        private void LoadProfile()
        {
            Person? person = ResolvePerson();
            lblName.Text = person != null ? person.FullName : _user.Username;
            lblRole.Text = EnumTranslator.TranslateUserRole(_user.Role) + " | Tài khoản: " + _user.Username;
            lblAccountStatus.Text = _user.IsActive ? "Trạng thái: đang hoạt động" : "Trạng thái: đã khóa";

            txtAvatarPath.Text = _user.AvatarPath;
            if (person != null)
            {
                txtEmail.Text = person.Email;
                txtPhone.Text = person.PhoneNumber;
                txtAddress.Text = person.HomeAddress != null ? person.HomeAddress.ToString() : string.Empty;
            }

            LoadWorkInfo(person);
            LoadSummary();
            LoadActivity();
        }

        private Person? ResolvePerson()
        {
            if (_user.Person != null)
            {
                return _user.Person;
            }

            if (_user.Role == UserRole.Driver)
            {
                return FindByAccount(DependencyContainer.GetDriverRepository().GetAll());
            }

            if (_user.Role == UserRole.Dispatcher)
            {
                return FindByAccount(DependencyContainer.GetDispatcherRepository().GetAll());
            }

            if (_user.Role == UserRole.WarehouseStaff)
            {
                return FindByAccount(DependencyContainer.GetWarehouseStaffRepository().GetAll());
            }

            return null;
        }

        private Staff? FindByAccount<T>(List<T> staffList) where T : Staff
        {
            for (int i = 0; i < staffList.Count; i++)
            {
                if (string.Equals(staffList[i].AccountID, _user.Username, StringComparison.OrdinalIgnoreCase))
                {
                    return staffList[i];
                }
            }

            return null;
        }

        private void LoadWorkInfo(Person? person)
        {
            workTable.Controls.Clear();
            workTable.RowStyles.Clear();

            Staff? staff = person as Staff;
            if (staff == null)
            {
                AddWorkRow("Loại tài khoản", EnumTranslator.TranslateUserRole(_user.Role));
                AddWorkRow("Ghi chú", "Tài khoản này chưa gắn với hồ sơ nhân viên.");
                return;
            }

            AddWorkRow("Mã nhân viên", staff.StaffID);
            AddWorkRow("Phòng ban", staff.Department);
            AddWorkRow("Trạng thái", EnumTranslator.TranslateWorkStatus(staff.WorkStatus));
            AddWorkRow("Ngày vào làm", staff.JoinDate.ToString("dd/MM/yyyy"));
            AddWorkRow("Thâm niên", staff.GetYearsOfService() + " năm");
            AddWorkRow("Lương dự kiến", staff.CalculateSalary().ToString("N0") + " VND");

            Driver? driver = staff as Driver;
            if (driver != null)
            {
                AddWorkRow("Bằng lái", driver.LicenseNumber + " - " + driver.LicenseType);
                AddWorkRow("Hạn bằng lái", driver.LicenseExpiryDate.ToString("dd/MM/yyyy"));
                AddWorkRow("Trạng thái tài xế", EnumTranslator.TranslateDriverStatus(driver.DriverStatus));
                AddWorkRow("Số chuyến giao", driver.DeliveryCount.ToString());
                return;
            }

            Dispatcher? dispatcher = staff as Dispatcher;
            if (dispatcher != null)
            {
                AddWorkRow("Khu vực", dispatcher.ManagedRegion);
                AddWorkRow("Phụ cấp khu vực", dispatcher.RegionAllowance.ToString("N0") + " VND");
                AddWorkRow("Thưởng KPI", dispatcher.KpiBonus.ToString("N0") + " VND");
                return;
            }

            WarehouseStaff? warehouseStaff = staff as WarehouseStaff;
            if (warehouseStaff != null)
            {
                AddWorkRow("Kho làm việc", warehouseStaff.WarehouseID);
                AddWorkRow("Ca làm", warehouseStaff.Shift);
                AddWorkRow("Phụ cấp ca", warehouseStaff.ShiftAllowance.ToString("N0") + " VND");
            }
        }

        private void LoadSummary()
        {
            summaryPanel.Controls.Clear();
            List<Order> orders = DependencyContainer.GetOrderService().GetAllOrders();

            int pending = CountByStatus(orders, OrderStatus.Pending);
            int inTransit = CountByStatus(orders, OrderStatus.InTransit);
            int delivered = CountByStatus(orders, OrderStatus.Delivered);
            int failed = CountByStatus(orders, OrderStatus.Failed);

            summaryPanel.Controls.Add(CreateMetric("Chờ xử lý", pending.ToString(), Color.FromArgb(245, 158, 11)));
            summaryPanel.Controls.Add(CreateMetric("Đang giao", inTransit.ToString(), Color.FromArgb(37, 99, 235)));
            summaryPanel.Controls.Add(CreateMetric("Đã giao", delivered.ToString(), Color.FromArgb(22, 163, 74)));
            summaryPanel.Controls.Add(CreateMetric("Sự cố", failed.ToString(), Color.FromArgb(220, 38, 38)));
        }

        private void LoadActivity()
        {
            List<object> rows = new List<object>();
            List<Order> orders = DependencyContainer.GetOrderService().GetAllOrders();

            Staff? staff = ResolvePerson() as Staff;
            for (int i = 0; i < orders.Count; i++)
            {
                bool include = _user.Role == UserRole.Admin || _user.Role == UserRole.Dispatcher || _user.Role == UserRole.WarehouseStaff;
                if (staff is Driver)
                {
                    include = orders[i].AssignedDriverID == staff.StaffID;
                }

                if (include)
                {
                    rows.Add(new
                    {
                        MaDon = orders[i].TrackingNumber,
                        TrangThai = EnumTranslator.TranslateOrderStatus(orders[i].CurrentStatus),
                        DichVu = EnumTranslator.TranslateServiceType(orders[i].ServiceType),
                        TaiXe = string.IsNullOrWhiteSpace(orders[i].AssignedDriverID) ? "Chưa gán" : orders[i].AssignedDriverID,
                        Xe = string.IsNullOrWhiteSpace(orders[i].AssignedVehicleID) ? "Chưa gán" : orders[i].AssignedVehicleID,
                        NgayTao = orders[i].CreatedDate.ToString("dd/MM/yyyy")
                    });
                }
            }

            dgvWork.DataSource = rows;

            if (dgvWork.Columns["MaDon"] != null) dgvWork.Columns["MaDon"]!.HeaderText = "Mã vận đơn";
            if (dgvWork.Columns["TrangThai"] != null) dgvWork.Columns["TrangThai"]!.HeaderText = "Trạng thái";
            if (dgvWork.Columns["DichVu"] != null) dgvWork.Columns["DichVu"]!.HeaderText = "Loại dịch vụ";
            if (dgvWork.Columns["TaiXe"] != null) dgvWork.Columns["TaiXe"]!.HeaderText = "Tài xế nhận";
            if (dgvWork.Columns["Xe"] != null) dgvWork.Columns["Xe"]!.HeaderText = "Phương tiện";
            if (dgvWork.Columns["NgayTao"] != null) dgvWork.Columns["NgayTao"]!.HeaderText = "Ngày tạo";
        }

        private int CountByStatus(List<Order> orders, OrderStatus status)
        {
            int count = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].CurrentStatus == status)
                {
                    count++;
                }
            }
            return count;
        }

        private void AddWorkRow(string name, string value)
        {
            int row = workTable.RowCount++;
            workTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            workTable.Controls.Add(new Label
            {
                Text = name,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(75, 85, 99),
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, row);
            workTable.Controls.Add(new Label
            {
                Text = value,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(31, 41, 55),
                TextAlign = ContentAlignment.MiddleLeft
            }, 1, row);
        }

        private static Panel CreateCard(string title)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 18, 18),
                Padding = new Padding(18)
            };

            Label label = new Label
            {
                Text = title,
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(18, 18)
            };
            card.Controls.Add(label);
            return card;
        }

        private static Label CreateLabel(string text, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(18, y),
                Size = new Size(130, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(75, 85, 99)
            };
        }

        private static TextBox CreateTextBox(int y)
        {
            return new TextBox
            {
                Location = new Point(150, y - 3),
                Size = new Size(235, 26),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 9.5F)
            };
        }

        private static Button CreateActionButton(string text, Color backColor)
        {
            Button button = new Button
            {
                Text = text,
                Size = new Size(140, 38),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private static Panel CreateMetric(string label, string value, Color accent)
        {
            Panel panel = new Panel
            {
                Width = 150,
                Height = 80,
                Margin = new Padding(0, 0, 14, 14),
                BackColor = Color.FromArgb(249, 250, 251)
            };
            panel.Controls.Add(new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = accent,
                Location = new Point(14, 10),
                AutoSize = true
            });
            panel.Controls.Add(new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(75, 85, 99),
                Location = new Point(16, 50),
                AutoSize = true
            });
            return panel;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                bool success = DependencyContainer.GetAccountManagementService().UpdateProfile(
                    _user.Username,
                    txtEmail.Text,
                    txtPhone.Text,
                    txtAddress.Text,
                    txtAvatarPath.Text
                );

                if (success)
                {
                    _user.AvatarPath = txtAvatarPath.Text.Trim();
                    if (_user.Person != null)
                    {
                        _user.Person.UpdateEmail(txtEmail.Text.Trim());
                        _user.Person.UpdatePhoneNumber(txtPhone.Text.Trim());
                        _user.Person.UpdateAddress(new Address(txtAddress.Text.Trim(), "", "", "", "", "Việt Nam"));
                    }
                    MessageBox.Show("Đã lưu hồ sơ cá nhân.", "Hồ sơ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProfile();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật thông tin hồ sơ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi định dạng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnChangePassword_Click(object? sender, EventArgs e)
        {
            using FrmChangePassword form = new FrmChangePassword(_user);
            form.ShowDialog(this);
        }
    }
}
