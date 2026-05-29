using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using Logistics.Core.Models.Account;
using Logistics.Core.Models.Common;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmRegister : Form
    {
        private readonly IAuthService _authService;
        private PrivateFontCollection pfc = new PrivateFontCollection();
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);

        public FrmRegister()
        {
            _authService = null!;
            InitializeComponent();

            if (!Utilities.DesignerHelper.IsInDesignMode(this))
            {
                SetupCustomUI();
            }
        }

        public FrmRegister(IAuthService authService)
        {
            _authService = authService;
            InitializeComponent();
            
            // Custom font and drag support
            SetupCustomUI();
        }

        private void SetupCustomUI()
        {
            LoadPoppinsFont();
            
            // Re-apply requirements message from code to stay dynamic
            if (lblRequirements != null) 
                lblRequirements.Text = PasswordValidator.GetRequirementsMessage();

            this.MouseDown += FrmRegister_MouseDown;
            this.MouseMove += FrmRegister_MouseMove;
            this.MouseUp += FrmRegister_MouseUp;
        }

        private void LoadPoppinsFont()
        {
            // Sử dụng font Segoe UI mặc định của Windows để hiển thị tiếng Việt có dấu chuẩn đẹp trong WinForms GDI+
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || 
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtSecurityAnswer.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!PasswordValidator.IsValid(txtPassword.Text, out string error))
            {
                MessageBox.Show(error, "Mật khẩu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User newUser = new User
            {
                Username = txtUsername.Text.Trim(),
                PasswordHash = txtPassword.Text,
                Role = UserRole.Customer,
                SecurityQuestion = cboSecurityQuestion.Text,
                SecurityAnswerHash = PasswordHasher.HashPassword(txtSecurityAnswer.Text.Trim().ToLower()),
                MustChangePassword = false
            };

            if (_authService.Register(newUser))
            {
                DependencyContainer.GetCustomerService().AddCustomer(
                    txtUsername.Text.Trim(),
                    "",
                    "",
                    new Address("N/A", "N/A", "N/A", "N/A", "000000", "Vietnam"),
                    CustomerType.Standard,
                    5_000_000m,
                    txtUsername.Text.Trim());
                MessageBox.Show("Đăng ký thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GoBackToLogin();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblBackToLogin_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            GoBackToLogin();
        }

        private void GoBackToLogin()
        {
            FrmLogin login = new FrmLogin(_authService);
            login.Show();
            this.Close();
        }

        private void btnExit_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkShowPassword_CheckedChanged(object? sender, EventArgs e)
        {
            char passwordChar = chkShowPassword.Checked ? '\0' : '*';
            txtPassword.PasswordChar = passwordChar;
            txtConfirmPassword.PasswordChar = passwordChar;
        }

        #region Form Dragging
        private void FrmRegister_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { isDragging = true; startPoint = new Point(e.X, e.Y); }
        }
        private void FrmRegister_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDragging) { Point p = PointToScreen(e.Location); this.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y); }
        }
        private void FrmRegister_MouseUp(object? sender, MouseEventArgs e) { isDragging = false; }
        #endregion
    }
}
