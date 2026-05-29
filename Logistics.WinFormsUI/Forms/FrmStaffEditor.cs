using System;
using System.Windows.Forms;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Common;
using Logistics.Core.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmStaffEditor : Form
    {
        public bool IsEditMode { get; private set; }
        public Driver? ResultDriver { get; private set; }

        public string SelectedRole
        {
            get { return cbRole.SelectedItem?.ToString() ?? "Tài xế"; }
        }

        public WorkStatus Status
        {
            get { return EnumTranslator.ParseWorkStatus(cbStatus.SelectedItem?.ToString() ?? "Đang hoạt động"); }
        }

        public string Shift
        {
            get { return txtShift.Text; }
        }

        public FrmStaffEditor(string title = "Thêm nhân viên mới")
        {
            InitializeComponent();
            lblTitle.Text = title;
            IsEditMode = false;
        }

        public FrmStaffEditor(Driver driver) : this("Sửa thông tin tài xế")
        {
            IsEditMode = true;
            txtName.Text = driver.FullName;
            txtPhone.Text = driver.PhoneNumber;
            txtEmail.Text = driver.Email;
            cbRole.SelectedItem = "Tài xế";
            cbRole.Enabled = false;
            txtSalary.Text = driver.BaseSalary.ToString();
            txtSpecific.Text = driver.LicenseNumber;
            lblSpecific.Text = "Số bằng lái:";
            cbStatus.SelectedItem = EnumTranslator.TranslateWorkStatus(driver.WorkStatus);
        }

        public FrmStaffEditor(Dispatcher dispatcher) : this("Sửa thông tin điều phối")
        {
            IsEditMode = true;
            txtName.Text = dispatcher.FullName;
            txtPhone.Text = dispatcher.PhoneNumber;
            txtEmail.Text = dispatcher.Email;
            cbRole.SelectedItem = "Điều phối";
            cbRole.Enabled = false;
            txtSalary.Text = dispatcher.BaseSalary.ToString();
            txtSpecific.Text = dispatcher.ManagedRegion;
            lblSpecific.Text = "Khu vực quản lý:";
            cbStatus.SelectedItem = EnumTranslator.TranslateWorkStatus(dispatcher.WorkStatus);
        }

        public FrmStaffEditor(WarehouseStaff staff) : this("Sửa thông tin nhân viên kho")
        {
            IsEditMode = true;
            txtName.Text = staff.FullName;
            txtPhone.Text = staff.PhoneNumber;
            txtEmail.Text = staff.Email;
            cbRole.SelectedItem = "Nhân viên kho";
            cbRole.Enabled = false;
            txtSalary.Text = staff.BaseSalary.ToString();
            txtSpecific.Text = staff.WarehouseID;
            txtShift.Text = staff.Shift;
            lblSpecific.Text = "Mã kho:";
            cbStatus.SelectedItem = EnumTranslator.TranslateWorkStatus(staff.WorkStatus);
            lblShift.Visible = txtShift.Visible = true;
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void CbRole_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string role = cbRole.SelectedItem?.ToString() ?? string.Empty;
            lblShift.Visible = txtShift.Visible = role == "Nhân viên kho";

            if (role == "Tài xế")
            {
                lblSpecific.Text = "Số bằng lái:";
            }
            else if (role == "Điều phối")
            {
                lblSpecific.Text = "Khu vực quản lý:";
            }
            else if (role == "Nhân viên kho")
            {
                lblSpecific.Text = "Mã kho:";
            }

            lblSpecific.Visible = true;
            txtSpecific.Visible = true;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ họ tên, số điện thoại và email.", "Thông báo");
                return;
            }

            decimal salary;
            if (!decimal.TryParse(txtSalary.Text, out salary) || salary < 0)
            {
                MessageBox.Show("Lương cơ bản không hợp lệ.", "Thông báo");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSpecific.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin theo vai trò.", "Thông báo");
                return;
            }

            if (SelectedRole == "Nhân viên kho" && string.IsNullOrWhiteSpace(txtShift.Text))
            {
                MessageBox.Show("Vui lòng nhập ca làm việc.", "Thông báo");
                return;
            }

            DialogResult = DialogResult.OK;
        }

        public string FullName => txtName.Text.Trim();
        public string Phone => txtPhone.Text.Trim();
        public string Email => txtEmail.Text.Trim();

        public decimal BaseSalary
        {
            get
            {
                return decimal.TryParse(txtSalary.Text, out decimal salary) ? salary : 0;
            }
        }

        public string SpecificInfo => txtSpecific.Text.Trim();
    }
}
