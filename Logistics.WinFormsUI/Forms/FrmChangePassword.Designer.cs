namespace Logistics.WinFormsUI.Forms
{
    partial class FrmChangePassword
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCurrentPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new System.Windows.Forms.Label();
            lblCurrentPassword = new System.Windows.Forms.Label();
            lblNewPassword = new System.Windows.Forms.Label();
            lblConfirmPassword = new System.Windows.Forms.Label();
            txtCurrentPassword = new System.Windows.Forms.TextBox();
            txtNewPassword = new System.Windows.Forms.TextBox();
            txtConfirmPassword = new System.Windows.Forms.TextBox();
            btnSave = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            SuspendLayout();
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(28, 24);
            lblTitle.Text = "Đổi mật khẩu";
            lblCurrentPassword.AutoSize = true;
            lblCurrentPassword.Location = new System.Drawing.Point(32, 86);
            lblCurrentPassword.Text = "Mật khẩu hiện tại";
            txtCurrentPassword.Location = new System.Drawing.Point(32, 110);
            txtCurrentPassword.PasswordChar = '*';
            txtCurrentPassword.Size = new System.Drawing.Size(320, 23);
            lblNewPassword.AutoSize = true;
            lblNewPassword.Location = new System.Drawing.Point(32, 152);
            lblNewPassword.Text = "Mật khẩu mới";
            txtNewPassword.Location = new System.Drawing.Point(32, 176);
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.Size = new System.Drawing.Size(320, 23);
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new System.Drawing.Point(32, 218);
            lblConfirmPassword.Text = "Xác nhận mật khẩu";
            txtConfirmPassword.Location = new System.Drawing.Point(32, 242);
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Size = new System.Drawing.Size(320, 23);
            btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(172, 298);
            btnSave.Size = new System.Drawing.Size(86, 34);
            btnSave.Text = "Lưu";
            btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            btnCancel.BackColor = System.Drawing.Color.FromArgb(189, 195, 199);
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.Location = new System.Drawing.Point(266, 298);
            btnCancel.Size = new System.Drawing.Size(86, 34);
            btnCancel.Text = "Hủy";
            btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(388, 360);
            Controls.Add(lblTitle);
            Controls.Add(lblCurrentPassword);
            Controls.Add(txtCurrentPassword);
            Controls.Add(lblNewPassword);
            Controls.Add(txtNewPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "FrmChangePassword";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Đổi mật khẩu";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
