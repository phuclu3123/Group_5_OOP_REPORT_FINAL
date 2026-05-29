using System;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Models.Common;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmOrderEditor : Form
    {
        private TextBox txtPackageDescription = null!;
        private TextBox txtPackageWeight = null!;
        private TextBox txtPackageValue = null!;
        private TextBox txtPackageCategory = null!;
        private TextBox txtDimensions = null!;
        private TextBox txtCostPerKg = null!;
        private CheckBox chkFragile = null!;

        public string SenderId
        {
            get { return txtSenderId.Text; }
        }

        public string ReceiverId
        {
            get { return txtReceiverId.Text; }
        }

        public string PickupAddress
        {
            get { return txtPickup.Text; }
        }

        public string DeliveryAddress
        {
            get { return txtDelivery.Text; }
        }

        public ServiceType SelectedServiceType
        {
            get
            {
                ServiceType value;
                if (Enum.TryParse(cbServiceType.SelectedItem?.ToString(), out value))
                {
                    return value;
                }

                return ServiceType.Standard;
            }
        }

        public string PackageDescription
        {
            get { return txtPackageDescription.Text.Trim(); }
        }

        public double PackageWeight
        {
            get
            {
                double value;
                return double.TryParse(txtPackageWeight.Text.Trim(), out value) ? value : 0;
            }
        }

        public decimal PackageValue
        {
            get
            {
                decimal value;
                return decimal.TryParse(txtPackageValue.Text.Trim(), out value) ? value : 0;
            }
        }

        public string PackageCategory
        {
            get { return txtPackageCategory.Text.Trim(); }
        }

        public string Dimensions
        {
            get { return txtDimensions.Text.Trim(); }
        }

        public decimal CostPerKg
        {
            get
            {
                decimal value;
                return decimal.TryParse(txtCostPerKg.Text.Trim(), out value) ? value : 15000;
            }
        }

        public bool IsFragile
        {
            get { return chkFragile.Checked; }
        }

        public FrmOrderEditor()
        {
            InitializeComponent();
            ConfigureVietnameseUi();
            AddPackageFields();
        }

        private void ConfigureVietnameseUi()
        {
            ClientSize = new Size(560, 720);
            lblTitle.Text = "Tạo đơn hàng mới";
            lblSender.Text = "Mã khách hàng gửi:";
            lblReceiver.Text = "Mã khách hàng nhận:";
            lblPickup.Text = "Địa chỉ lấy hàng:";
            lblDelivery.Text = "Địa chỉ giao hàng:";
            lblService.Text = "Loại dịch vụ:";
            btnSave.Text = "Tạo đơn";
            btnCancel.Text = "Hủy";
            btnSave.Location = new Point(335, 660);
            btnCancel.Location = new Point(445, 660);
        }

        private void AddPackageFields()
        {
            Label section = CreateLabel("Thông tin kiện hàng đầu tiên", 405);
            section.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            Label lblDescription = CreateLabel("Mô tả hàng hóa:", 435);
            txtPackageDescription = CreateTextBox(460, "VD: Linh kiện điện tử, hồ sơ, thực phẩm...");

            Label lblWeight = CreateLabel("Khối lượng (kg):", 500);
            txtPackageWeight = CreateTextBox(525, "VD: 12.5");
            txtPackageWeight.Width = 150;

            Label lblValue = CreateLabel("Giá trị hàng (VNĐ):", 500);
            lblValue.Location = new Point(210, 500);
            txtPackageValue = CreateTextBox(525, "VD: 2500000");
            txtPackageValue.Location = new Point(210, 525);
            txtPackageValue.Width = 150;

            Label lblCost = CreateLabel("Đơn giá/kg:", 500);
            lblCost.Location = new Point(390, 500);
            txtCostPerKg = CreateTextBox(525, "15000");
            txtCostPerKg.Location = new Point(390, 525);
            txtCostPerKg.Width = 140;

            Label lblCategory = CreateLabel("Nhóm hàng:", 565);
            txtPackageCategory = CreateTextBox(590, "Hàng thường");
            txtPackageCategory.Width = 240;

            Label lblDimensions = CreateLabel("Kích thước:", 565);
            lblDimensions.Location = new Point(300, 565);
            txtDimensions = CreateTextBox(590, "40x30x20 cm");
            txtDimensions.Location = new Point(300, 590);
            txtDimensions.Width = 230;

            chkFragile = new CheckBox
            {
                Text = "Hàng dễ vỡ/cần xử lý nhẹ",
                Location = new Point(30, 625),
                Size = new Size(260, 24),
                Font = new Font("Segoe UI", 9.5F)
            };

            Controls.Add(section);
            Controls.Add(lblDescription);
            Controls.Add(txtPackageDescription);
            Controls.Add(lblWeight);
            Controls.Add(txtPackageWeight);
            Controls.Add(lblValue);
            Controls.Add(txtPackageValue);
            Controls.Add(lblCost);
            Controls.Add(txtCostPerKg);
            Controls.Add(lblCategory);
            Controls.Add(txtPackageCategory);
            Controls.Add(lblDimensions);
            Controls.Add(txtDimensions);
            Controls.Add(chkFragile);
        }

        private static Label CreateLabel(string text, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(30, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F)
            };
        }

        private static TextBox CreateTextBox(int y, string placeholder)
        {
            return new TextBox
            {
                Location = new Point(30, y),
                Size = new Size(500, 27),
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = placeholder
            };
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSenderId.Text) ||
                string.IsNullOrWhiteSpace(txtReceiverId.Text) ||
                string.IsNullOrWhiteSpace(txtPickup.Text) ||
                string.IsNullOrWhiteSpace(txtDelivery.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin người gửi, người nhận và địa chỉ.", "Tạo đơn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(PackageDescription) || PackageWeight <= 0)
            {
                MessageBox.Show("Vui lòng nhập mô tả và khối lượng kiện hàng hợp lệ.", "Tạo đơn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            Close();
        }

        public void SetSenderId(string senderId, bool readOnly = true)
        {
            txtSenderId.Text = senderId;
            txtSenderId.ReadOnly = readOnly;
        }
    }
}
