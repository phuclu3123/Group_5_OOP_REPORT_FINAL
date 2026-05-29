using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.DTOs;
using Logistics.Core.Mappings;
using Logistics.Core.Models.Account;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Common;
using Logistics.Core.Services.Interfaces;
using Logistics.WinFormsUI.Extensions;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucStaffManagement : UserControl
    {
        private IStaffManagementService? _staffService;

        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Button btnAddStaff = null!;
        private Panel pnlSearch = null!;
        private TextBox txtSearch = null!;
        private ComboBox cbRoleFilter = null!;
        private DataGridView dgvStaff = null!;

        public ucStaffManagement()
        {
            InitializeComponent();
            if (DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            _staffService = DependencyContainer.GetStaffManagementService();
            UIStyleHelper.ApplyGridViewStyle(dgvStaff);
            btnAddStaff.SetRoundedBorder(10);
            LoadStaffData();
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            if (_staffService == null)
            {
                return;
            }

            using (Forms.FrmStaffEditor editor = new Forms.FrmStaffEditor())
            {
                if (editor.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                Address address = new Address("N/A", "N/A", "N/A", "N/A", "000000", "Vietnam");
                string accountId = "ACC-" + Guid.NewGuid().ToString().Substring(0, 8);
                Staff? createdStaff = null;
                UserRole accountRole = UserRole.Driver;
                string serviceRole = "Driver";

                try
                {
                    if (IsDriverRole(editor.SelectedRole))
                    {
                        createdStaff = _staffService.AddDriver(editor.FullName, editor.Phone, editor.Email, address, DateTime.Now.AddYears(-25), Gender.Male, accountId, "Van tai", editor.BaseSalary, DateTime.Now, editor.SpecificInfo, "Class E", DateTime.Now.AddYears(5));
                        accountRole = UserRole.Driver;
                        serviceRole = "Driver";
                    }
                    else if (IsDispatcherRole(editor.SelectedRole))
                    {
                        createdStaff = _staffService.AddDispatcher(editor.FullName, editor.Phone, editor.Email, address, DateTime.Now.AddYears(-30), Gender.Male, accountId, "Dieu phoi", editor.BaseSalary, DateTime.Now, editor.SpecificInfo);
                        accountRole = UserRole.Dispatcher;
                        serviceRole = "Dispatcher";
                    }
                    else if (IsWarehouseRole(editor.SelectedRole))
                    {
                        createdStaff = _staffService.AddWarehouseStaff(editor.FullName, editor.Phone, editor.Email, address, DateTime.Now.AddYears(-28), Gender.Male, accountId, "Kho van", editor.BaseSalary, DateTime.Now, editor.SpecificInfo, editor.Shift);
                        accountRole = UserRole.WarehouseStaff;
                        serviceRole = "WarehouseStaff";
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Du lieu khong hop le", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (createdStaff != null)
                {
                    AccountProvisionResultDTO account = DependencyContainer.GetAccountManagementService()
                        .ProvisionEmployeeAccount(accountRole, createdStaff, "What is your employee code?", createdStaff.StaffID.ToLower());
                    if (account.Success)
                    {
                        _staffService.UpdateAccountId(createdStaff.StaffID, serviceRole, account.Username);
                        MessageBox.Show(
                            "Da tao nhan vien va cap tai khoan.\nTai khoan: " + account.Username +
                            "\nMat khau tam thoi: " + account.TemporaryPassword +
                            "\nCau tra loi quen mat khau mac dinh: " + createdStaff.StaffID.ToLower(),
                            "Tai khoan nhan vien",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                LoadStaffData();
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            LoadStaffData();
        }

        private void LoadStaffData()
        {
            if (_staffService == null || DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            string searchText = txtSearch.Text.ToLower();
            string roleFilter = cbRoleFilter.SelectedItem?.ToString() ?? "Tat ca vai tro";
            List<StaffDTO> filtered = new List<StaffDTO>();

            AddFilteredStaff(filtered, _staffService.GetAllDrivers(), searchText, roleFilter);
            AddFilteredStaff(filtered, _staffService.GetAllDispatchers(), searchText, roleFilter);
            AddFilteredStaff(filtered, _staffService.GetAllWarehouseStaff(), searchText, roleFilter);

            dgvStaff.DataSource = filtered;
            ConfigureGridColumns(roleFilter);
        }

        private void AddFilteredStaff(List<StaffDTO> target, IEnumerable<Driver> source, string searchText, string roleFilter)
        {
            foreach (Driver staff in source)
            {
                AddStaffIfMatched(target, staff.ToStaffDTO(), searchText, roleFilter);
            }
        }

        private void AddFilteredStaff(List<StaffDTO> target, IEnumerable<Dispatcher> source, string searchText, string roleFilter)
        {
            foreach (Dispatcher staff in source)
            {
                AddStaffIfMatched(target, staff.ToStaffDTO(), searchText, roleFilter);
            }
        }

        private void AddFilteredStaff(List<StaffDTO> target, IEnumerable<WarehouseStaff> source, string searchText, string roleFilter)
        {
            foreach (WarehouseStaff staff in source)
            {
                AddStaffIfMatched(target, staff.ToStaffDTO(), searchText, roleFilter);
            }
        }

        private void AddStaffIfMatched(List<StaffDTO> target, StaffDTO staff, string searchText, string roleFilter)
        {
            bool matchesSearch = string.IsNullOrEmpty(searchText) || staff.FullName.ToLower().Contains(searchText) || staff.StaffId.ToLower().Contains(searchText);
            bool matchesRole = IsAllRole(roleFilter) || staff.Role == roleFilter;
            if (matchesSearch && matchesRole)
            {
                target.Add(staff);
            }
        }

        private void ConfigureGridColumns(string roleFilter)
        {
            if (dgvStaff.Columns["StaffId"] != null) dgvStaff.Columns["StaffId"]!.HeaderText = "Mã NV";
            if (dgvStaff.Columns["FullName"] != null) dgvStaff.Columns["FullName"]!.HeaderText = "Họ tên";
            if (dgvStaff.Columns["Role"] != null) dgvStaff.Columns["Role"]!.HeaderText = "Vai trò";
            if (dgvStaff.Columns["WorkStatus"] != null) dgvStaff.Columns["WorkStatus"]!.HeaderText = "Trạng thái";
            if (dgvStaff.Columns["Email"] != null) dgvStaff.Columns["Email"]!.HeaderText = "Email";
            if (dgvStaff.Columns["TotalSalary"] != null)
            {
                dgvStaff.Columns["TotalSalary"]!.HeaderText = "Tổng lương";
                dgvStaff.Columns["TotalSalary"]!.DefaultCellStyle.Format = "N0";
            }

            string[] hiddenCols = { "PersonId", "Department", "AdminCode", "JoinDate", "YearsOfService", "BaseSalary", "Phone" };
            foreach (string columnName in hiddenCols)
            {
                if (dgvStaff.Columns[columnName] != null) dgvStaff.Columns[columnName]!.Visible = false;
            }

            if (dgvStaff.Columns["ManagedRegion"] != null) dgvStaff.Columns["ManagedRegion"]!.Visible = IsDispatcherRole(roleFilter);
            if (dgvStaff.Columns["WarehouseId"] != null) dgvStaff.Columns["WarehouseId"]!.Visible = IsWarehouseRole(roleFilter);
            if (dgvStaff.Columns["Shift"] != null) dgvStaff.Columns["Shift"]!.Visible = IsWarehouseRole(roleFilter);

            if (!dgvStaff.Columns.Contains("btnEdit"))
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.Name = "btnEdit";
                btnEdit.HeaderText = "";
                btnEdit.Text = "Sửa";
                btnEdit.UseColumnTextForButtonValue = true;
                btnEdit.FlatStyle = FlatStyle.Flat;
                dgvStaff.Columns.Add(btnEdit);

                DataGridViewButtonColumn btnSalary = new DataGridViewButtonColumn();
                btnSalary.Name = "btnSalary";
                btnSalary.HeaderText = "";
                btnSalary.Text = "Lương";
                btnSalary.UseColumnTextForButtonValue = true;
                btnSalary.FlatStyle = FlatStyle.Flat;
                dgvStaff.Columns.Add(btnSalary);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.Name = "btnDelete";
                btnDelete.HeaderText = "";
                btnDelete.Text = "Xóa";
                btnDelete.UseColumnTextForButtonValue = true;
                btnDelete.FlatStyle = FlatStyle.Flat;
                dgvStaff.Columns.Add(btnDelete);
            }
        }

        private void dgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_staffService == null || e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string id = dgvStaff.Rows[e.RowIndex].Cells["StaffId"].Value?.ToString() ?? string.Empty;
            string role = dgvStaff.Rows[e.RowIndex].Cells["Role"].Value?.ToString() ?? string.Empty;

            if (dgvStaff.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                EditStaff(id, role);
            }
            else if (dgvStaff.Columns[e.ColumnIndex].Name == "btnSalary")
            {
                ShowSalaryBreakdown(id, role);
            }
            else if (dgvStaff.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DeleteStaff(id, role);
            }
        }

        private void ShowSalaryBreakdown(string id, string role)
        {
            if (_staffService == null)
            {
                return;
            }

            string content = string.Empty;
            if (IsDriverRole(role))
            {
                Driver driver = _staffService.GetDriverById(id);
                if (driver != null) content = driver.GetSalaryBreakdown();
            }
            else if (IsDispatcherRole(role))
            {
                Dispatcher dispatcher = _staffService.GetDispatcherById(id);
                if (dispatcher != null) content = dispatcher.GetSalaryBreakdown();
            }
            else if (IsWarehouseRole(role))
            {
                WarehouseStaff staff = _staffService.GetWarehouseStaffById(id);
                if (staff != null) content = staff.GetSalaryBreakdown();
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Không tìm thấy bảng lương.", "Lương", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(content + "\n\nBạn có muốn in phiếu lương PDF?", "Bảng lương", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                DocumentPrintHelper.PrintText("Phiếu lương " + id, content, this);
            }
        }

        private void EditStaff(string id, string role)
        {
            if (_staffService == null)
            {
                return;
            }

            if (IsDriverRole(role))
            {
                Driver driver = _staffService.GetDriverById(id);
                if (driver != null)
                {
                    using (Forms.FrmStaffEditor editor = new Forms.FrmStaffEditor(driver))
                    {
                        if (editor.ShowDialog() == DialogResult.OK)
                        {
                            if (!_staffService.UpdateDriverInfo(id, editor.Phone, editor.Email, editor.BaseSalary))
                            {
                                MessageBox.Show("Khong the cap nhat tai xe vi du lieu khong hop le.", "Du lieu khong hop le", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            _staffService.UpdateWorkStatus(id, "Driver", editor.Status);
                            LoadStaffData();
                        }
                    }
                }
            }
            else if (IsDispatcherRole(role))
            {
                Dispatcher dispatcher = _staffService.GetDispatcherById(id);
                if (dispatcher != null)
                {
                    using (Forms.FrmStaffEditor editor = new Forms.FrmStaffEditor(dispatcher))
                    {
                        if (editor.ShowDialog() == DialogResult.OK)
                        {
                            if (!_staffService.UpdateDispatcherRegion(id, editor.SpecificInfo) ||
                                !_staffService.UpdateBaseSalary(id, "Dispatcher", editor.BaseSalary))
                            {
                                MessageBox.Show("Khong the cap nhat dieu phoi vi du lieu khong hop le.", "Du lieu khong hop le", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            LoadStaffData();
                        }
                    }
                }
            }
            else if (IsWarehouseRole(role))
            {
                WarehouseStaff staff = _staffService.GetWarehouseStaffById(id);
                if (staff != null)
                {
                    using (Forms.FrmStaffEditor editor = new Forms.FrmStaffEditor(staff))
                    {
                        if (editor.ShowDialog() == DialogResult.OK)
                        {
                            if (!_staffService.UpdateWarehouseStaffDetails(id, editor.SpecificInfo, editor.Shift) ||
                                !_staffService.UpdateBaseSalary(id, "WarehouseStaff", editor.BaseSalary))
                            {
                                MessageBox.Show("Khong the cap nhat nhan vien kho vi du lieu khong hop le.", "Du lieu khong hop le", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            LoadStaffData();
                        }
                    }
                }
            }
        }

        private void DeleteStaff(string id, string role)
        {
            if (_staffService == null)
            {
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên " + id + "?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }

            bool deleted = false;
            if (IsDriverRole(role)) deleted = _staffService.DeleteDriver(id);
            else if (IsDispatcherRole(role)) deleted = _staffService.DeleteDispatcher(id);
            else if (IsWarehouseRole(role)) deleted = _staffService.DeleteWarehouseStaff(id);

            if (deleted)
            {
                LoadStaffData();
            }
            else
            {
                MessageBox.Show("Không thể xóa nhân viên này.", "Lỗi");
            }
        }

        private bool IsAllRole(string role)
        {
            return role == "Tat ca vai tro" || role == "Tất cả vai trò" || role.Contains("Tất");
        }

        private bool IsDriverRole(string role)
        {
            return role == "Tai xe" || role == "Tài xế" || role.Contains("xế");
        }

        private bool IsDispatcherRole(string role)
        {
            return role == "Dieu phoi" || role == "Điều phối" || role.Contains("phối");
        }

        private bool IsWarehouseRole(string role)
        {
            return role == "Nhan vien kho" || role == "Nhân viên kho" || role.Contains("kho");
        }
    }
}
