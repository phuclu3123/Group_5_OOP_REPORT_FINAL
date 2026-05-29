namespace Logistics.WinFormsUI.Forms
{
    partial class FrmRegister
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelTop = new Panel();
            btnExit = new Button();
            lblTitle = new Label();
            lblUser = new Label();
            txtUsername = new TextBox();
            lblPass = new Label();
            txtPassword = new TextBox();
            lblConfirmPass = new Label();
            txtConfirmPassword = new TextBox();
            lblQuestion = new Label();
            cboSecurityQuestion = new ComboBox();
            lblAnswer = new Label();
            txtSecurityAnswer = new TextBox();
            btnRegister = new Button();
            lblBackToLogin = new LinkLabel();
            lblRequirements = new Label();
            chkShowPassword = new CheckBox();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(30, 32, 39);
            panelTop.Controls.Add(btnExit);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(450, 40);
            panelTop.TabIndex = 0;
            // 
            // btnExit
            // 
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(410, 0);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(40, 40);
            btnExit.TabIndex = 0;
            btnExit.Text = "x";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(77, 168, 218);
            lblTitle.Location = new Point(110, 50);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(229, 32);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Đăng Ký Tài Khoản";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.ForeColor = Color.White;
            lblUser.Location = new Point(40, 100);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(89, 15);
            lblUser.TabIndex = 2;
            lblUser.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(40, 125);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(370, 23);
            txtUsername.TabIndex = 3;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.ForeColor = Color.White;
            lblPass.Location = new Point(40, 165);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(60, 15);
            lblPass.TabIndex = 4;
            lblPass.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(40, 190);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(370, 23);
            txtPassword.TabIndex = 5;
            // 
            // lblConfirmPass
            // 
            lblConfirmPass.AutoSize = true;
            lblConfirmPass.ForeColor = Color.White;
            lblConfirmPass.Location = new Point(40, 230);
            lblConfirmPass.Name = "lblConfirmPass";
            lblConfirmPass.Size = new Size(112, 15);
            lblConfirmPass.TabIndex = 6;
            lblConfirmPass.Text = "Xác nhận mật khẩu:";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(40, 255);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '●';
            txtConfirmPassword.Size = new Size(370, 23);
            txtConfirmPassword.TabIndex = 7;
            // 
            // lblQuestion
            // 
            lblQuestion.AutoSize = true;
            lblQuestion.ForeColor = Color.White;
            lblQuestion.Location = new Point(40, 397);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(98, 15);
            lblQuestion.TabIndex = 10;
            lblQuestion.Text = "Câu hỏi bảo mật:";
            // 
            // cboSecurityQuestion
            // 
            cboSecurityQuestion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSecurityQuestion.FormattingEnabled = true;
            cboSecurityQuestion.Items.Add("What is your pet's name?");
            cboSecurityQuestion.Items.Add("What is your mother's maiden name?");
            cboSecurityQuestion.Items.Add("What was your first school?");
            cboSecurityQuestion.Items.Add("What is your favorite book?");
            cboSecurityQuestion.Location = new Point(40, 422);
            cboSecurityQuestion.Name = "cboSecurityQuestion";
            cboSecurityQuestion.Size = new Size(370, 23);
            cboSecurityQuestion.TabIndex = 11;
            // 
            // lblAnswer
            // 
            lblAnswer.AutoSize = true;
            lblAnswer.ForeColor = Color.White;
            lblAnswer.Location = new Point(40, 462);
            lblAnswer.Name = "lblAnswer";
            lblAnswer.Size = new Size(64, 15);
            lblAnswer.TabIndex = 12;
            lblAnswer.Text = "Câu trả lời:";
            // 
            // txtSecurityAnswer
            // 
            txtSecurityAnswer.Location = new Point(40, 487);
            txtSecurityAnswer.Name = "txtSecurityAnswer";
            txtSecurityAnswer.Size = new Size(370, 23);
            txtSecurityAnswer.TabIndex = 13;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.FromArgb(77, 168, 218);
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(40, 547);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(370, 45);
            btnRegister.TabIndex = 14;
            btnRegister.Text = "ĐĂNG KÝ";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += BtnRegister_Click;
            // 
            // lblBackToLogin
            // 
            lblBackToLogin.AutoSize = true;
            lblBackToLogin.LinkColor = Color.FromArgb(77, 168, 218);
            lblBackToLogin.Location = new Point(165, 607);
            lblBackToLogin.Name = "lblBackToLogin";
            lblBackToLogin.Size = new Size(111, 15);
            lblBackToLogin.TabIndex = 15;
            lblBackToLogin.TabStop = true;
            lblBackToLogin.Text = "Quay lại Đăng nhập";
            lblBackToLogin.LinkClicked += lblBackToLogin_LinkClicked;
            // 
            // lblRequirements
            // 
            lblRequirements.Font = new Font("Segoe UI", 8F);
            lblRequirements.ForeColor = Color.Gray;
            lblRequirements.Location = new Point(40, 315);
            lblRequirements.Name = "lblRequirements";
            lblRequirements.Size = new Size(370, 70);
            lblRequirements.TabIndex = 9;
            lblRequirements.Text = "Yêu cầu mật khẩu:\n- Tối thiểu 8 ký tự\n- Có chữ hoa, chữ thường\n- Có ít nhất 1 chữ số\n- Có ký tự đặc biệt (!@#$%...)";
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.ForeColor = Color.White;
            chkShowPassword.Location = new Point(40, 285);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(122, 19);
            chkShowPassword.TabIndex = 8;
            chkShowPassword.Text = "Hiển thị mật khẩu";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // FrmRegister
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 32, 39);
            ClientSize = new Size(450, 650);
            Controls.Add(chkShowPassword);
            Controls.Add(lblRequirements);
            Controls.Add(lblBackToLogin);
            Controls.Add(btnRegister);
            Controls.Add(txtSecurityAnswer);
            Controls.Add(lblAnswer);
            Controls.Add(cboSecurityQuestion);
            Controls.Add(lblQuestion);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmPass);
            Controls.Add(txtPassword);
            Controls.Add(lblPass);
            Controls.Add(txtUsername);
            Controls.Add(lblUser);
            Controls.Add(lblTitle);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmRegister";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Register New Account";
            panelTop.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirmPass;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.ComboBox cboSecurityQuestion;
        private System.Windows.Forms.Label lblAnswer;
        private System.Windows.Forms.TextBox txtSecurityAnswer;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.LinkLabel lblBackToLogin;
        private System.Windows.Forms.Label lblRequirements;
        private System.Windows.Forms.CheckBox chkShowPassword;
    }
}
