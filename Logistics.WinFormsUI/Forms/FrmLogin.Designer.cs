namespace Logistics.WinFormsUI.Forms
{
    partial class FrmLogin
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
            panel2 = new Panel();
            btnExit = new Button();
            btnMinimize = new Button();
            txtUsername = new TextBox();
            lblPass = new Label();
            lblUser = new Label();
            txtPassword = new TextBox();
            ptLoGo = new PictureBox();
            btnLogin = new Button();
            lblTen = new Label();
            lblSlogan = new Label();
            lblChuaCoTaiKhoan = new Label();
            chkRememberMe = new CheckBox();
            llbDangKy = new LinkLabel();
            linkLabel1 = new LinkLabel();
            btnTogglePassword = new Button();
            pnlPassword = new Panel();
            pnlUsername = new Panel();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ptLoGo).BeginInit();
            pnlPassword.SuspendLayout();
            pnlUsername.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(30, 32, 39);
            panel2.Controls.Add(btnExit);
            panel2.Controls.Add(btnMinimize);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(476, 40);
            panel2.TabIndex = 10;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(30, 32, 39);
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExit.ForeColor = SystemColors.Window;
            btnExit.Location = new Point(413, 0);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(63, 40);
            btnExit.TabIndex = 0;
            btnExit.Text = "x";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // btnMinimize
            // 
            btnMinimize.BackColor = Color.FromArgb(30, 32, 39);
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnMinimize.ForeColor = SystemColors.Window;
            btnMinimize.Location = new Point(341, 0);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(66, 40);
            btnMinimize.TabIndex = 1;
            btnMinimize.Text = "-";
            btnMinimize.UseVisualStyleBackColor = false;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Location = new Point(10, 8);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(350, 16);
            txtUsername.TabIndex = 1;
            // 
            // lblPass
            // 
            lblPass.Font = new Font("Segoe UI", 13.8F);
            lblPass.ForeColor = SystemColors.Window;
            lblPass.Location = new Point(48, 282);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(101, 34);
            lblPass.TabIndex = 2;
            lblPass.Text = "Mật khẩu:";
            // 
            // lblUser
            // 
            lblUser.Font = new Font("Segoe UI", 13.8F);
            lblUser.ForeColor = SystemColors.Window;
            lblUser.Location = new Point(48, 204);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(101, 32);
            lblUser.TabIndex = 0;
            lblUser.Text = "Tài khoản:";
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Location = new Point(10, 8);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(320, 16);
            txtPassword.TabIndex = 3;
            // 
            // ptLoGo
            // 
            ptLoGo.ErrorImage = Properties.Resources.hospital;
            ptLoGo.Image = Properties.Resources.hospital;
            ptLoGo.InitialImage = Properties.Resources.hospital;
            ptLoGo.Location = new Point(195, 58);
            ptLoGo.Name = "ptLoGo";
            ptLoGo.Size = new Size(82, 64);
            ptLoGo.SizeMode = PictureBoxSizeMode.Zoom;
            ptLoGo.TabIndex = 11;
            ptLoGo.TabStop = false;
            ptLoGo.Click += ptLoGo_Click;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(77, 168, 218);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(50, 396);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(373, 45);
            btnLogin.TabIndex = 7;
            btnLogin.TabStop = false;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTen.ForeColor = Color.FromArgb(77, 168, 218);
            lblTen.Location = new Point(90, 125);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(211, 32);
            lblTen.TabIndex = 12;
            lblTen.Text = "HỆ THỐNG LOGISTICS\r\n";
            // 
            // lblSlogan
            // 
            lblSlogan.Font = new Font("Segoe UI", 9.8F);
            lblSlogan.ForeColor = Color.White;
            lblSlogan.Location = new Point(138, 167);
            lblSlogan.Name = "lblSlogan";
            lblSlogan.Size = new Size(200, 33);
            lblSlogan.TabIndex = 13;
            lblSlogan.Text = "Smart Logistics Solution";
            // 
            // lblChuaCoTaiKhoan
            // 
            lblChuaCoTaiKhoan.AutoSize = true;
            lblChuaCoTaiKhoan.Font = new Font("Segoe UI", 12F);
            lblChuaCoTaiKhoan.ForeColor = Color.White;
            lblChuaCoTaiKhoan.Location = new Point(80, 470);
            lblChuaCoTaiKhoan.Name = "lblChuaCoTaiKhoan";
            lblChuaCoTaiKhoan.Size = new Size(145, 21);
            lblChuaCoTaiKhoan.TabIndex = 8;
            lblChuaCoTaiKhoan.Text = "Chưa có tài khoản ?";
            // 
            // chkRememberMe
            // 
            chkRememberMe.AutoSize = true;
            chkRememberMe.ForeColor = Color.White;
            chkRememberMe.Location = new Point(50, 360);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(106, 19);
            chkRememberMe.TabIndex = 5;
            chkRememberMe.Text = "Lưu đăng nhập";
            chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // llbDangKy
            // 
            llbDangKy.AutoSize = true;
            llbDangKy.Font = new Font("Segoe UI", 12F);
            llbDangKy.LinkColor = Color.White;
            llbDangKy.Location = new Point(256, 470);
            llbDangKy.Name = "llbDangKy";
            llbDangKy.Size = new Size(117, 21);
            llbDangKy.TabIndex = 9;
            llbDangKy.TabStop = true;
            llbDangKy.Text = "Đăng ký tại đây";
            llbDangKy.LinkClicked += llbDangKy_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.LinkColor = Color.White;
            linkLabel1.Location = new Point(326, 360);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(97, 15);
            linkLabel1.TabIndex = 6;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Quên mật khẩu ?";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // btnTogglePassword
            // 
            btnTogglePassword.BackColor = Color.White;
            btnTogglePassword.Cursor = Cursors.Hand;
            btnTogglePassword.FlatAppearance.BorderSize = 0;
            btnTogglePassword.FlatStyle = FlatStyle.Flat;
            btnTogglePassword.Location = new Point(340, 3);
            btnTogglePassword.Name = "btnTogglePassword";
            btnTogglePassword.Size = new Size(30, 26);
            btnTogglePassword.TabIndex = 4;
            btnTogglePassword.UseVisualStyleBackColor = false;
            btnTogglePassword.Click += btnTogglePassword_Click;
            // 
            // pnlPassword
            // 
            pnlPassword.BackColor = Color.White;
            pnlPassword.Controls.Add(btnTogglePassword);
            pnlPassword.Controls.Add(txtPassword);
            pnlPassword.Location = new Point(50, 319);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Size = new Size(373, 32);
            pnlPassword.TabIndex = 3;
            // 
            // pnlUsername
            // 
            pnlUsername.BackColor = Color.White;
            pnlUsername.Controls.Add(txtUsername);
            pnlUsername.Location = new Point(50, 239);
            pnlUsername.Name = "pnlUsername";
            pnlUsername.Size = new Size(373, 32);
            pnlUsername.TabIndex = 1;
            // 
            // FrmLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(30, 32, 39);
            ClientSize = new Size(476, 550);
            Controls.Add(pnlUsername);
            Controls.Add(pnlPassword);
            Controls.Add(linkLabel1);
            Controls.Add(llbDangKy);
            Controls.Add(chkRememberMe);
            Controls.Add(lblChuaCoTaiKhoan);
            Controls.Add(ptLoGo);
            Controls.Add(lblTen);
            Controls.Add(lblSlogan);
            Controls.Add(panel2);
            Controls.Add(btnLogin);
            Controls.Add(lblUser);
            Controls.Add(lblPass);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Logistics System - Đăng nhập";
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ptLoGo).EndInit();
            pnlPassword.ResumeLayout(false);
            pnlPassword.PerformLayout();
            pnlUsername.ResumeLayout(false);
            pnlUsername.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox ptLoGo;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label lblSlogan;
        private System.Windows.Forms.Label lblChuaCoTaiKhoan;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.LinkLabel llbDangKy;
        private System.Windows.Forms.Button btnMinimize;
        private LinkLabel linkLabel1;
        private Button btnTogglePassword;
        private Panel pnlPassword;
        private Panel pnlUsername;
    }
}
