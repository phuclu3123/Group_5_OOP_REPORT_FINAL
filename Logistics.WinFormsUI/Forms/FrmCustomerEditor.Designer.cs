using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Forms
{
    partial class FrmCustomerEditor
    {
        private Label lblTitle;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblCustomerType;
        private ComboBox cbCustomerType;
        private Label lblCreditLimit;
        private TextBox txtCreditLimit;
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblCustomerType = new Label();
            cbCustomerType = new ComboBox();
            lblCreditLimit = new Label();
            txtCreditLimit = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            ClientSize = new Size(430, 520);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;
            Text = "Khach hang";
            lblTitle.Text = "Khach hang";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(25, 20);
            lblTitle.Size = new Size(360, 35);
            lblFullName.Text = "Ho ten:";
            lblFullName.Location = new Point(30, 75);
            lblFullName.AutoSize = true;
            txtFullName.Location = new Point(30, 100);
            txtFullName.Size = new Size(360, 27);
            lblPhone.Text = "So dien thoai:";
            lblPhone.Location = new Point(30, 140);
            lblPhone.AutoSize = true;
            txtPhone.Location = new Point(30, 165);
            txtPhone.Size = new Size(360, 27);
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(30, 205);
            lblEmail.AutoSize = true;
            txtEmail.Location = new Point(30, 230);
            txtEmail.Size = new Size(360, 27);
            lblAddress.Text = "Dia chi:";
            lblAddress.Location = new Point(30, 270);
            lblAddress.AutoSize = true;
            txtAddress.Location = new Point(30, 295);
            txtAddress.Size = new Size(360, 27);
            lblCustomerType.Text = "Loai khach:";
            lblCustomerType.Location = new Point(30, 335);
            lblCustomerType.AutoSize = true;
            cbCustomerType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCustomerType.Location = new Point(30, 360);
            cbCustomerType.Size = new Size(360, 27);
            cbCustomerType.Items.Add("Standard");
            cbCustomerType.Items.Add("VIP");
            cbCustomerType.Items.Add("Enterprise");
            lblCreditLimit.Text = "Han muc tin dung:";
            lblCreditLimit.Location = new Point(30, 400);
            lblCreditLimit.AutoSize = true;
            txtCreditLimit.Location = new Point(30, 425);
            txtCreditLimit.Size = new Size(360, 27);
            txtCreditLimit.Text = "0";
            btnSave.Text = "Luu";
            btnSave.Location = new Point(205, 470);
            btnSave.Size = new Size(85, 32);
            btnSave.BackColor = Color.FromArgb(46, 204, 113);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += BtnSave_Click;
            btnCancel.Text = "Huy";
            btnCancel.Location = new Point(305, 470);
            btnCancel.Size = new Size(85, 32);
            btnCancel.BackColor = Color.FromArgb(189, 195, 199);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Click += BtnCancel_Click;
            Controls.Add(lblTitle);
            Controls.Add(lblFullName);
            Controls.Add(txtFullName);
            Controls.Add(lblPhone);
            Controls.Add(txtPhone);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblAddress);
            Controls.Add(txtAddress);
            Controls.Add(lblCustomerType);
            Controls.Add(cbCustomerType);
            Controls.Add(lblCreditLimit);
            Controls.Add(txtCreditLimit);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
