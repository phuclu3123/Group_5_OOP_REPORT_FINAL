using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Forms
{
    partial class FrmStaffEditor
    {
        private Label lblTitle;
        private Label lblName;
        private TextBox txtName;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblRole;
        private ComboBox cbRole;
        private Label lblSalary;
        private TextBox txtSalary;
        private Label lblSpecific;
        private TextBox txtSpecific;
        private Label lblShift;
        private TextBox txtShift;
        private Label lblStatus;
        private ComboBox cbStatus;
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblName = new Label();
            txtName = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblRole = new Label();
            cbRole = new ComboBox();
            lblSalary = new Label();
            txtSalary = new TextBox();
            lblSpecific = new Label();
            txtSpecific = new TextBox();
            lblShift = new Label();
            txtShift = new TextBox();
            lblStatus = new Label();
            cbStatus = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();

            ClientSize = new Size(430, 700);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;

            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(24, 22);
            lblTitle.Size = new Size(380, 42);

            lblName.Text = "Họ và tên:";
            lblName.Location = new Point(34, 86);
            lblName.AutoSize = true;
            txtName.Location = new Point(34, 111);
            txtName.Size = new Size(360, 30);

            lblPhone.Text = "Số điện thoại:";
            lblPhone.Location = new Point(34, 151);
            lblPhone.AutoSize = true;
            txtPhone.Location = new Point(34, 176);
            txtPhone.Size = new Size(360, 30);

            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(34, 216);
            lblEmail.AutoSize = true;
            txtEmail.Location = new Point(34, 241);
            txtEmail.Size = new Size(360, 30);

            lblRole.Text = "Vai trò:";
            lblRole.Location = new Point(34, 281);
            lblRole.AutoSize = true;
            cbRole.Location = new Point(34, 306);
            cbRole.Size = new Size(360, 30);
            cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRole.Items.Add("Tài xế");
            cbRole.Items.Add("Điều phối");
            cbRole.Items.Add("Nhân viên kho");
            cbRole.SelectedIndex = 0;
            cbRole.SelectedIndexChanged += CbRole_SelectedIndexChanged;

            lblSalary.Text = "Lương cơ bản:";
            lblSalary.Location = new Point(34, 346);
            lblSalary.AutoSize = true;
            txtSalary.Location = new Point(34, 371);
            txtSalary.Size = new Size(360, 30);

            lblStatus.Text = "Trạng thái làm việc:";
            lblStatus.Location = new Point(34, 411);
            lblStatus.AutoSize = true;
            cbStatus.Location = new Point(34, 436);
            cbStatus.Size = new Size(360, 30);
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.Items.Add("Đang hoạt động");
            cbStatus.Items.Add("Đã thôi việc");
            cbStatus.Items.Add("Nghỉ phép");
            cbStatus.SelectedIndex = 0;

            lblSpecific.Text = "Số bằng lái:";
            lblSpecific.Location = new Point(34, 476);
            lblSpecific.AutoSize = true;
            txtSpecific.Location = new Point(34, 501);
            txtSpecific.Size = new Size(360, 30);

            lblShift.Text = "Ca làm việc:";
            lblShift.Location = new Point(34, 541);
            lblShift.AutoSize = true;
            lblShift.Visible = false;
            txtShift.Location = new Point(34, 566);
            txtShift.Size = new Size(360, 30);
            txtShift.Visible = false;

            btnSave.Text = "Lưu";
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(204, 630);
            btnSave.Size = new Size(90, 36);
            btnSave.Click += BtnSave_Click;

            btnCancel.Text = "Hủy";
            btnCancel.BackColor = Color.FromArgb(189, 195, 199);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(304, 630);
            btnCancel.Size = new Size(90, 36);
            btnCancel.Click += BtnCancel_Click;

            Controls.Add(lblTitle);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblPhone);
            Controls.Add(txtPhone);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblRole);
            Controls.Add(cbRole);
            Controls.Add(lblSalary);
            Controls.Add(txtSalary);
            Controls.Add(lblStatus);
            Controls.Add(cbStatus);
            Controls.Add(lblSpecific);
            Controls.Add(txtSpecific);
            Controls.Add(lblShift);
            Controls.Add(txtShift);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
