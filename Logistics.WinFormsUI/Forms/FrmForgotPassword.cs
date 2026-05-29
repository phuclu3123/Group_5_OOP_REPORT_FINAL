using System;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmForgotPassword : Form
    {
        private readonly IAuthService _authService;
        private string _targetUsername;
        private bool _isDragging;
        private Point _dragStartPoint;

        public FrmForgotPassword()
        {
            _authService = null!;
            _targetUsername = string.Empty;
            InitializeComponent();

            if (!Utilities.DesignerHelper.IsInDesignMode(this))
            {
                SetupDragging();
            }
        }

        public FrmForgotPassword(IAuthService authService)
        {
            _authService = authService;
            _targetUsername = string.Empty;
            InitializeComponent();
            SetupDragging();
        }

        private void SetupDragging()
        {
            this.MouseDown += FrmForgotPassword_MouseDown;
            this.MouseMove += FrmForgotPassword_MouseMove;
            this.MouseUp += FrmForgotPassword_MouseUp;
        }

        private void FrmForgotPassword_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void FrmForgotPassword_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point point = PointToScreen(e.Location);
                Location = new Point(point.X - _dragStartPoint.X, point.Y - _dragStartPoint.Y);
            }
        }

        private void FrmForgotPassword_MouseUp(object? sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void BtnCheckUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo");
                return;
            }

            string? question = _authService.GetSecurityQuestion(username);
            if (string.IsNullOrEmpty(question))
            {
                MessageBox.Show("Không tìm thấy tên đăng nhập này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _targetUsername = username;
            lblQuestion.Text = question;
            
            // Transition to Step 2
            pnlUser.Visible = false;
            lblUser.Visible = false;
            btnCheckUser.Visible = false;
            lblInstructions.Text = "Xác thực danh tính của bạn";
            pnlVerification.Visible = true;
        }

        private void BtnVerifyAnswer_Click(object sender, EventArgs e)
        {
            string answer = txtAnswer.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(answer))
            {
                MessageBox.Show("Vui lòng nhập câu trả lời.", "Thông báo");
                return;
            }

            if (_authService.ValidateSecurityAnswer(_targetUsername, answer))
            {
                // Transition to Step 3
                pnlVerification.Visible = false;
                lblInstructions.Text = "Thiết lập mật khẩu mới";
                pnlReset.Visible = true;
            }
            else
            {
                MessageBox.Show("Câu trả lời không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAnswer.Clear();
                txtAnswer.Focus();
            }
        }

        private void BtnResetPassword_Click(object sender, EventArgs e)
        {
            string newPass = txtNewPassword.Text;
            string confirmPass = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới.", "Thông báo");
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!PasswordValidator.IsValid(newPass, out string error))
            {
                MessageBox.Show(error, "Mật khẩu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_authService.ResetPassword(_targetUsername, newPass))
            {
                MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GoBackToLogin();
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExit_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void BtnToggleNewPass_Click(object? sender, EventArgs e)
        {
            TogglePassword(txtNewPassword, btnToggleNewPass);
        }

        private void BtnToggleConfirmPass_Click(object? sender, EventArgs e)
        {
            TogglePassword(txtConfirmPassword, btnToggleConfirmPass);
        }

        private void LblBack_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            GoBackToLogin();
        }

        private void GoBackToLogin()
        {
            FrmLogin login = new FrmLogin(_authService);
            login.Show();
            this.Close();
        }

        private void TogglePassword(TextBox txt, Button btn)
        {
            if (txt.PasswordChar == '●')
            {
                txt.PasswordChar = '\0';
                btn.Image = Properties.Resources.eye_hide;
            }
            else
            {
                txt.PasswordChar = '●';
                btn.Image = Properties.Resources.eye_show;
            }
        }
    }
}
