using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Models.Account;
using Logistics.Core.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmLogin : Form
    {
        private PrivateFontCollection pfc = new PrivateFontCollection();
        private readonly IAuthService _authService;

        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        private System.Windows.Forms.Timer timerMinimize = new System.Windows.Forms.Timer();

        public FrmLogin()
        {
            _authService = null!;
            InitializeComponent();

            if (!Utilities.DesignerHelper.IsInDesignMode(this))
            {
                SetupCustomUI();
            }
        }

        public FrmLogin(IAuthService authService)
        {
            _authService = authService;
            InitializeComponent();

            SetupCustomUI();
        }

        private void SetupCustomUI()
        {
            LoadPoppinsFont();

            // Safe image loading for designer
            try { ptLoGo.Image = Properties.Resources.truck; } catch { }

            timerMinimize.Interval = 5;
            timerMinimize.Tick += TimerMinimize_Tick;

            this.Load += FrmLogin_Load;
            this.MouseDown += FrmLogin_MouseDown;
            this.MouseMove += FrmLogin_MouseMove;
            this.MouseUp += FrmLogin_MouseUp;

            LoadRememberedUser();
            txtPassword.PasswordChar = '*';
            btnTogglePassword.Image = null;
            btnTogglePassword.Text = "Show";
            btnTogglePassword.Width = 56;
            btnTogglePassword.Left = pnlPassword.Width - btnTogglePassword.Width - 4;
            btnTogglePassword.Top = 3;
            btnTogglePassword.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtPassword.Width = pnlPassword.Width - btnTogglePassword.Width - 18;
            btnTogglePassword.ImageAlign = ContentAlignment.MiddleCenter;
            btnTogglePassword.Visible = true;
            btnTogglePassword.BringToFront();
        }

        private void LoadPoppinsFont()
        {
            // Sử dụng font Segoe UI mặc định của Windows để hiển thị tiếng Việt có dấu chuẩn đẹp trong WinForms GDI+
            lblTen.Font = new Font("Segoe UI", 18, FontStyle.Bold);
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User? user = _authService.Login(txtUsername.Text.Trim(), txtPassword.Text);
            if (user != null)
            {
                if (chkRememberMe.Checked)
                {
                    SaveRememberedUser(txtUsername.Text.Trim());
                }
                else
                {
                    ClearRememberedUser();
                }

                MessageBox.Show("Đăng nhập thành công! Vai trò: " + user.Role.ToString(), "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SessionManager.Login(user);
                if (user.MustChangePassword)
                {
                    MessageBox.Show("Tài khoản đang dùng mật khẩu tạm thời. Vui lòng đổi mật khẩu trước khi vào hệ thống.",
                        "Đổi mật khẩu bắt buộc", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    using FrmChangePassword changePassword = new FrmChangePassword(user);
                    if (changePassword.ShowDialog(this) != DialogResult.OK)
                    {
                        SessionManager.Logout();
                        txtPassword.Clear();
                        txtPassword.Focus();
                        return;
                    }
                }

                FrmMain main = new FrmMain(user);
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void llbDangKy_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRegister reg = new FrmRegister(_authService);
            reg.Show();
            this.Hide();
        }

        private void FrmLogin_Load(object? sender, EventArgs e)
        {
        }

        private void btnExit_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object? sender, EventArgs e)
        {
            timerMinimize.Start();
        }

        private void linkLabel1_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmForgotPassword forgot = new FrmForgotPassword(_authService);
            forgot.Show();
            this.Hide();
        }

        private void TimerMinimize_Tick(object? sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.1;
            }
            else
            {
                timerMinimize.Stop();
                this.WindowState = FormWindowState.Minimized;
                this.Opacity = 1;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000;
                return cp;
            }
        }

        #region Form Dragging
        private void FrmLogin_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                startPoint = new Point(e.X, e.Y);
            }
        }

        private void FrmLogin_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }

        private void FrmLogin_MouseUp(object? sender, MouseEventArgs e)
        {
            isDragging = false;
        }
        #endregion

        private void btnTogglePassword_Click(object? sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                txtPassword.PasswordChar = '\0';
                btnTogglePassword.Text = "Hide";
            }
            else
            {
                txtPassword.PasswordChar = '*';
                btnTogglePassword.Text = "Show";
            }
        }

        private string GetRememberFilePath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appData, "TPA_Logistics");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            return Path.Combine(folder, "remembered_user.txt");
        }

        private void SaveRememberedUser(string username)
        {
            try { File.WriteAllText(GetRememberFilePath(), username, System.Text.Encoding.UTF8); } catch { }
        }

        private void ClearRememberedUser()
        {
            try { string path = GetRememberFilePath(); if (File.Exists(path)) File.Delete(path); } catch { }
        }

        private void LoadRememberedUser()
        {
            try
            {
                string path = GetRememberFilePath();
                if (File.Exists(path))
                {
                    txtUsername.Text = File.ReadAllText(path, System.Text.Encoding.UTF8);
                    chkRememberMe.Checked = true;
                    txtPassword.Focus();
                }
            }
            catch { }
        }

        private void ptLoGo_Click(object? sender, EventArgs e)
        {

        }
    }
}
