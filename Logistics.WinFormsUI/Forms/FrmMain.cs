﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Logistics.Core.Models.Account;
using Logistics.Core.Security;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;
using Logistics.WinFormsUI.UserControls;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmMain : Form
    {
        private readonly User _currentUser;

        private bool _isLoggingOut = false;

        public FrmMain()
        {
            InitializeComponent();
            InitializeThemeToggle();
            ConfigureSidebarMenu();
            RegisterThemeEvents();
            _currentUser = new User
            {
                Username = "Designer",
                Role = UserRole.Admin
            };
        }

        public FrmMain(User user)
        {
            InitializeComponent();
            InitializeThemeToggle();
            ConfigureSidebarMenu();
            RegisterThemeEvents();
            _currentUser = user;
            
            // Thiet lap noi dung theo vai tro cua tai khoan dang nhap.
            SetupUIByRole();
            SetupProfileButton();
            
            // Khi dong form chinh, can thoat ung dung neu khong phai dang dang xuat.
            this.FormClosed += FrmMain_FormClosed;

            // Nap trang Tong quan mac dinh sau khi quyen da duoc thiet lap.
            btnDashboard_Click(this, EventArgs.Empty);

            // Nhan thong bao van hanh tu service dung chung.
            DependencyContainer.GetNotificationService().OnNotificationReceived += HandleGlobalNotification;

        }

        private void RegisterThemeEvents()
        {
            ThemeManager.ThemeChanged += FrmMain_ThemeChanged;
            ApplyCurrentTheme();
        }

        private void FrmMain_ThemeChanged(object? sender, EventArgs e)
        {
            ApplyCurrentTheme();
        }

        private void ApplyCurrentTheme()
        {
            UpdateThemeToggleButton();
            ThemeManager.ApplyTheme(this);

            if (panelContent.Controls.Count > 0)
            {
                ThemeManager.ApplyTheme(panelContent.Controls[0]);
            }
        }

        private void HandleGlobalNotification(string title, string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string, string>(HandleGlobalNotification), title, message);
                return;
            }

            // Danh dau nut thong bao va hien thi toast khi co su kien van hanh.
            // Mau vang giup nguoi dung nhan biet thong bao moi tren header.
            btnNotifications.BackColor = Color.FromArgb(255, 193, 7);
            
            // NotifyIcon duoc dung lam toast nhe, khong tao them form phu.
            ShowToast(title, message);
        }

        private void ShowToast(string title, string message)
        {
            // Toast he thong ngan gon, tu huy sau khi het thoi gian hien thi.

            NotifyIcon toast = new NotifyIcon();
            toast.Icon = SystemIcons.Information;
            toast.Visible = true;
            toast.BalloonTipTitle = title;
            toast.BalloonTipText = message;
            toast.ShowBalloonTip(3000);
            
            // Giai phong NotifyIcon de khong giu icon an trong system tray.
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer { Interval = 5000 };
            t.Tag = toast;
            t.Tick += ToastTimer_Tick;
            t.Start();
        }

        private void ToastTimer_Tick(object? sender, EventArgs e)
        {
            System.Windows.Forms.Timer? timer = sender as System.Windows.Forms.Timer;
            if (timer == null)
            {
                return;
            }

            NotifyIcon? toast = timer.Tag as NotifyIcon;
            if (toast != null)
            {
                toast.Dispose();
            }

            timer.Stop();
            timer.Dispose();
        }

        // An/hien menu theo quyen cua tai khoan hien tai.
        private void SetupUIByRole()
        {
            // Uu tien ten ho so neu tai khoan da lien ket voi Person.
            string displayName = (_currentUser.Person != null) ? _currentUser.Person.FullName : _currentUser.Username;
            lblWelcome.Text = "Xin chào, " + displayName;
            this.Text = "Logistics System - " + displayName;

            btnOrders.Visible = RoleGuard.CanViewOrders();
            btnDispatch.Visible = RoleGuard.CanDispatch();
            btnCustomers.Visible = RoleGuard.CanManageCustomers();
            btnDrivers.Visible = RoleGuard.CanManageStaff();
            btnVehicles.Visible = RoleGuard.CanManageVehicles();
            btnMaintenance.Visible = RoleGuard.CanManageMaintenance();
            btnWarehouses.Visible = RoleGuard.CanManageWarehouse();
            btnDocuments.Visible = RoleGuard.CanViewDocuments();
            btnAdmin.Visible = RoleGuard.CanOpenAdminArea();
            btnReports.Visible = RoleGuard.CanViewReports();
        }

        private void ConfigureSidebarMenu()
        {
            ConfigureMenuButton(btnDashboard, "      Tổng quan", DockStyle.Top);
            ConfigureMenuButton(btnOrders, "      Đơn hàng", DockStyle.Top);
            ConfigureMenuButton(btnDispatch, "      Điều phối", DockStyle.Top);
            ConfigureMenuButton(btnCustomers, "      Khách hàng", DockStyle.Top);
            ConfigureMenuButton(btnDrivers, "      Nhân sự", DockStyle.Top);
            ConfigureMenuButton(btnVehicles, "      Phương tiện", DockStyle.Top);
            ConfigureMenuButton(btnMaintenance, "      Bảo trì xe", DockStyle.Top);
            ConfigureMenuButton(btnWarehouses, "      Kho bãi", DockStyle.Top);
            ConfigureMenuButton(btnDocuments, "      Chứng từ", DockStyle.Top);
            ConfigureMenuButton(btnAdmin, "      Quản trị", DockStyle.Top);
            ConfigureMenuButton(btnReports, "      Báo cáo", DockStyle.Top);
            ConfigureMenuButton(btnSettings, "      Cài đặt", DockStyle.Bottom);
            ConfigureMenuButton(btnMyProfile, "      Hồ sơ của tôi", DockStyle.Bottom);

            panel1.Width = 280;
            pictureBox1.Height = 112;
            lblStatus.Text = "● Đang hoạt động";
            lblWelcome.Text = "Xin chào, ...";
            Text = "Hệ thống quản lý vận chuyển";

            panel1.Tag = "Sidebar";
            panel2.Tag = "Header";
            lblWelcome.Tag = "HeaderText";
            lblStatus.Tag = "Success";
            ConfigureHeaderButton(btnProfile);
            ConfigureHeaderButton(btnNotifications);
            ConfigureHeaderButton(btnLogout);
            ConfigureHeaderButton(btnThemeToggle);
        }

        private void InitializeThemeToggle()
        {
            btnThemeToggle = new Button();
            btnThemeToggle.Dock = DockStyle.Right;
            btnThemeToggle.Width = 60;
            btnThemeToggle.FlatStyle = FlatStyle.Flat;
            btnThemeToggle.FlatAppearance.BorderSize = 0;
            btnThemeToggle.Cursor = Cursors.Hand;
            btnThemeToggle.Font = new Font("Segoe UI", 14F);
            btnThemeToggle.Tag = "HeaderButton";
            btnThemeToggle.TextAlign = ContentAlignment.MiddleCenter;
            btnThemeToggle.UseVisualStyleBackColor = false;
            
            UpdateThemeToggleButton();

            btnThemeToggle.Click += (sender, e) =>
            {
                AppTheme nextTheme = ThemeManager.CurrentTheme == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
                ThemeManager.CurrentTheme = nextTheme;
                SaveThemePreference(nextTheme);
            };

            btnThemeToggle.MouseEnter += btnHeader_MouseEnter;
            btnThemeToggle.MouseLeave += btnHeader_MouseLeave;

            panel2.Controls.Add(btnThemeToggle);
        }

        private static void SaveThemePreference(AppTheme theme)
        {
            AppPersonalizationSettings personalization = AppPersonalizationSettings.Load();
            personalization.ThemeName = theme == AppTheme.Dark ? "Dark" : "Light";
            personalization.Save();
        }

        private void UpdateThemeToggleButton()
        {
            if (ThemeManager.CurrentTheme == AppTheme.Light)
            {
                btnThemeToggle.Text = "☾";
                btnThemeToggle.ForeColor = ThemeManager.HeaderTextColor;
            }
            else
            {
                btnThemeToggle.Text = "☀";
                btnThemeToggle.ForeColor = ThemeManager.HeaderTextColor;
            }
        }

        private static void ConfigureMenuButton(Button button, string text, DockStyle dockStyle)
        {
            button.Dock = dockStyle;
            button.Height = 50;
            button.Text = text;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Font = new Font("Segoe UI", 11F);
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
        }

        private static void ConfigureHeaderButton(Button button)
        {
            button.Tag = "HeaderButton";
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
            button.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void addUserControl(UserControl uc, Button activeBtn)
        {
            uc.Dock = DockStyle.Fill;
            panelContent.Controls.Clear();
            panelContent.Controls.Add(uc);
            uc.BringToFront();

            // Reset highlight tren sidebar truoc khi danh dau nut dang chon.
            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.Transparent;
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Segoe UI", 11F);
                }
            }
            if (activeBtn != null)
            {
                activeBtn.BackColor = Color.FromArgb(65, 50, 100);
                activeBtn.ForeColor = Color.White;
                activeBtn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            }

            ThemeManager.ApplyTheme(uc);
        }

        private void btnDashboard_Click(object? sender, EventArgs e)
        {
            addUserControl(new ucDashboard(
                DependencyContainer.GetReportService(),
                DependencyContainer.GetOrderService()
            ), btnDashboard);
        }

        private void btnOrders_Click(object? sender, EventArgs e)
        {
            if (!EnsureAllowed(RoleGuard.CanViewOrders(), "Don hang")) return;
            addUserControl(new ucOrderManagement(), btnOrders);
        }

        private void btnDispatch_Click(object? sender, EventArgs e)
        {
            if (!EnsureAllowed(RoleGuard.CanDispatch(), "Dieu phoi")) return;
            using (FrmDispatch dispatchForm = new FrmDispatch())
            {
                dispatchForm.ShowDialog(this);
            }
        }

        private void btnDrivers_Click(object? sender, EventArgs e)
        {
            if (!EnsureAllowed(RoleGuard.CanManageStaff(), "Nhan su")) return;
            addUserControl(new ucStaffManagement(), btnDrivers);
        }

        private void btnCustomers_Click(object? sender, EventArgs e)
        {
            if (!EnsureAllowed(RoleGuard.CanManageCustomers(), "Khach hang")) return;
            addUserControl(new ucCustomerManagement(), btnCustomers);
        }

        private void btnVehicles_Click(object? sender, EventArgs e)
        {
            if (!EnsureAllowed(RoleGuard.CanManageVehicles(), "Phuong tien")) return;
            addUserControl(new ucVehicleManagement(), btnVehicles);
        }

        private void btnWarehouses_Click(object? sender, EventArgs e)
        {
            if (!EnsureAllowed(RoleGuard.CanManageWarehouse(), "Kho bai")) return;
            addUserControl(new ucWarehouseManagement(), btnWarehouses);
        }

        private void btnMaintenance_Click(object? sender, EventArgs e)
        {
            if (!EnsureAllowed(RoleGuard.CanManageMaintenance(), "Bao tri xe")) return;
            addUserControl(new ucMaintenanceManagement(), btnMaintenance);
        }

        private void btnReports_Click(object? sender, EventArgs e)
        {
            if (!EnsureAllowed(RoleGuard.CanViewReports(), "Bao cao")) return;
            using (FrmReport reportForm = new FrmReport(DependencyContainer.GetReportService()))
            {
                reportForm.ShowDialog(this);
            }
        }

        private void btnAdmin_Click(object? sender, EventArgs e)
        {
            if (!EnsureAllowed(RoleGuard.CanOpenAdminArea(), "Quan tri")) return;
            addUserControl(new ucAdminDashboard(), btnAdmin);
        }

        private void btnDocuments_Click(object? sender, EventArgs e)
        {
            if (!EnsureAllowed(RoleGuard.CanViewDocuments(), "Chung tu")) return;
            addUserControl(new ucDocuments(), btnDocuments);
        }

        private bool EnsureAllowed(bool allowed, string featureName)
        {
            if (allowed)
            {
                return true;
            }

            MessageBox.Show(
                "Tai khoan " + _currentUser.Username + " (" + _currentUser.Role + ") khong co quyen truy cap chuc nang: " + featureName + ".",
                "Khong du quyen",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        private void btnMyProfile_Click(object? sender, EventArgs e)
        {
            addUserControl(new ucMyProfile(_currentUser), btnMyProfile);
        }

        private void btnSettings_Click(object? sender, EventArgs e)
        {
            using (FrmSettings settingsForm = new FrmSettings())
            {
                if (settingsForm.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (Control control in panelContent.Controls)
                    {
                        if (control is ucOrderManagement)
                        {
                            addUserControl(new ucOrderManagement(), btnOrders);
                            break;
                        }
                    }
                }
            }
        }

        // Chuan hoa icon header sau khi form da load day du resource.
        private void FrmMain_Load(object? sender, EventArgs e)
        {
            // Avatar co the thay doi theo tai khoan, nen cap nhat lai khi form load.
            SetupProfileButton();

            if (btnLogout.Image != null)
            {
                // Ep icon lon ve kich thuoc phu hop voi nut header.
                btnLogout.Image = new Bitmap(btnLogout.Image, new Size(24, 24));
            }

            if (btnNotifications.Image != null)
            {
                // Ep icon lon ve kich thuoc phu hop voi nut header.
                btnNotifications.Image = new Bitmap(btnNotifications.Image, new Size(24, 24));
            }
        }

        private void SetupProfileButton()
        {
            if (!string.IsNullOrEmpty(_currentUser.AvatarPath) && File.Exists(_currentUser.AvatarPath))
            {
                try
                {
                    using (Image original = Image.FromFile(_currentUser.AvatarPath))
                    {
                        // Tao avatar tron 32x32 tu anh nguoi dung.
                        btnProfile.Image = GetCircularImage(original, 32);
                    }
                }
                catch { /* Bo qua neu khong doc duoc anh avatar. */ }
            }
            btnProfile.Text = btnProfile.Image == null ? "Hồ sơ" : "";
        }

        private Image GetCircularImage(Image img, int size)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, size, size);
                    g.SetClip(path);
                    g.DrawImage(img, 0, 0, size, size);
                }
            }
            return bmp;
        }

        private void btnHeader_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = ThemeManager.HeaderHoverColor;
            }
        }

        private void btnHeader_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = ThemeManager.HeaderColor;
                btn.ForeColor = ThemeManager.HeaderTextColor;
            }
        }

        private void ShowProfileModal()
        {
            FrmUserProfileModal modal = new FrmUserProfileModal(_currentUser);
            Form backgroundWrapper = new Form();
            using (modal)
            {
                backgroundWrapper.StartPosition = FormStartPosition.Manual;
                backgroundWrapper.FormBorderStyle = FormBorderStyle.None;
                backgroundWrapper.Opacity = 0.5d;
                backgroundWrapper.BackColor = Color.Black;
                backgroundWrapper.Size = this.Size;
                backgroundWrapper.Location = this.Location;
                backgroundWrapper.ShowInTaskbar = false;
                backgroundWrapper.Show(this);

                modal.Owner = backgroundWrapper;
                modal.ShowDialog();

                backgroundWrapper.Dispose();
            }
        }

        private void btnProfile_Click(object? sender, EventArgs e)
        {
            ShowProfileModal();
        }

        private void btnNotifications_Click(object? sender, EventArgs e)
        {
            int openIssues = 0;
            foreach (var report in DependencyContainer.GetProblemReportService().GetAllReports())
            {
                if (report.ResolutionStatus != Logistics.Core.Models.Common.ResolutionStatus.Resolved)
                {
                    openIssues++;
                }
            }

            int pendingOrders = DependencyContainer.GetOrderService().GetOrdersByStatus(Logistics.Core.Models.Common.OrderStatus.Pending).Count;
            string message = "Đơn hàng chờ điều phối: " + pendingOrders + "\n" +
                             "Sự cố đang xử lý: " + openIssues;
            MessageBox.Show(message, "Thông báo vận hành", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnLogout_Click(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?", "Xác nhận đăng xuất", 
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _isLoggingOut = true;
                SessionManager.Logout();
                this.Close(); // Dong form hien tai, sau do mo lai man hinh dang nhap.
                
                FrmLogin loginForm = new FrmLogin(DependencyContainer.GetAuthService());
                loginForm.Show();
            }
        }

        private void FrmMain_FormClosed(object? sender, FormClosedEventArgs e)
        {
            // Neu nguoi dung bam nut dong cua so, thoat han ung dung.
            // Neu dang dang xuat, FrmLogin moi se duoc mo tiep nen khong Application.Exit.
            ThemeManager.ThemeChanged -= FrmMain_ThemeChanged;
            DependencyContainer.GetNotificationService().OnNotificationReceived -= HandleGlobalNotification;

            if (!_isLoggingOut)
            {
                Application.Exit();
            }
        }
    }
}
