#nullable disable
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Forms
{
    partial class FrmMain
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmMain));
            panel1 = new Panel();
            btnReports = new Button();
            btnAdmin = new Button();
            btnMyProfile = new Button();
            btnSettings = new Button();
            btnDocuments = new Button();
            btnWarehouses = new Button();
            btnMaintenance = new Button();
            btnVehicles = new Button();
            btnDrivers = new Button();
            btnCustomers = new Button();
            btnDispatch = new Button();
            btnOrders = new Button();
            btnDashboard = new Button();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            lblStatus = new Label();
            lblWelcome = new Label();
            btnProfile = new Button();
            btnNotifications = new Button();
            btnLogout = new Button();
            panelContent = new Panel();
            panel1.SuspendLayout();
            ((ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(45, 35, 70);
            panel1.Controls.Add(btnMyProfile);
            panel1.Controls.Add(btnSettings);
            panel1.Controls.Add(btnReports);
            panel1.Controls.Add(btnAdmin);
            panel1.Controls.Add(btnDocuments);
            panel1.Controls.Add(btnWarehouses);
            panel1.Controls.Add(btnMaintenance);
            panel1.Controls.Add(btnVehicles);
            panel1.Controls.Add(btnDrivers);
            panel1.Controls.Add(btnCustomers);
            panel1.Controls.Add(btnDispatch);
            panel1.Controls.Add(btnOrders);
            panel1.Controls.Add(btnDashboard);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(280, 517);
            panel1.TabIndex = 0;
            // 
            // btnReports
            // 
            btnReports.Name = "btnReports";
            btnReports.TabIndex = 12;
            btnReports.UseVisualStyleBackColor = true;
            btnReports.Click += btnReports_Click;
            // 
            // btnAdmin
            // 
            btnAdmin.Name = "btnAdmin";
            btnAdmin.TabIndex = 11;
            btnAdmin.UseVisualStyleBackColor = true;
            btnAdmin.Click += btnAdmin_Click;
            // 
            // btnMyProfile
            // 
            btnMyProfile.Name = "btnMyProfile";
            btnMyProfile.TabIndex = 14;
            btnMyProfile.UseVisualStyleBackColor = true;
            btnMyProfile.Click += btnMyProfile_Click;
            // 
            // btnSettings
            // 
            btnSettings.Name = "btnSettings";
            btnSettings.TabIndex = 13;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnDocuments
            // 
            btnDocuments.Name = "btnDocuments";
            btnDocuments.TabIndex = 10;
            btnDocuments.UseVisualStyleBackColor = true;
            btnDocuments.Click += btnDocuments_Click;
            // 
            // btnWarehouses
            // 
            btnWarehouses.Name = "btnWarehouses";
            btnWarehouses.TabIndex = 9;
            btnWarehouses.UseVisualStyleBackColor = true;
            btnWarehouses.Click += btnWarehouses_Click;
            // 
            // btnMaintenance
            // 
            btnMaintenance.Name = "btnMaintenance";
            btnMaintenance.TabIndex = 8;
            btnMaintenance.UseVisualStyleBackColor = true;
            btnMaintenance.Click += btnMaintenance_Click;
            // 
            // btnVehicles
            // 
            btnVehicles.Name = "btnVehicles";
            btnVehicles.TabIndex = 7;
            btnVehicles.UseVisualStyleBackColor = true;
            btnVehicles.Click += btnVehicles_Click;
            // 
            // btnDrivers
            // 
            btnDrivers.Name = "btnDrivers";
            btnDrivers.TabIndex = 6;
            btnDrivers.UseVisualStyleBackColor = true;
            btnDrivers.Click += btnDrivers_Click;
            // 
            // btnCustomers
            // 
            btnCustomers.Name = "btnCustomers";
            btnCustomers.TabIndex = 5;
            btnCustomers.UseVisualStyleBackColor = true;
            btnCustomers.Click += btnCustomers_Click;
            // 
            // btnDispatch
            // 
            btnDispatch.Name = "btnDispatch";
            btnDispatch.TabIndex = 4;
            btnDispatch.UseVisualStyleBackColor = true;
            btnDispatch.Click += btnDispatch_Click;
            // 
            // btnOrders
            // 
            btnOrders.Name = "btnOrders";
            btnOrders.TabIndex = 3;
            btnOrders.UseVisualStyleBackColor = true;
            btnOrders.Click += btnOrders_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.Name = "btnDashboard";
            btnDashboard.TabIndex = 2;
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = Properties.Resources.truck;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(280, 112);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(lblStatus);
            panel2.Controls.Add(lblWelcome);
            panel2.Controls.Add(btnProfile);
            panel2.Controls.Add(btnNotifications);
            panel2.Controls.Add(btnLogout);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(280, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(569, 77);
            panel2.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.Green;
            lblStatus.Location = new Point(22, 42);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(92, 20);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Dang hoat dong";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblWelcome.Location = new Point(20, 15);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(99, 25);
            lblWelcome.TabIndex = 3;
            lblWelcome.Text = "Xin chao, ...";
            // 
            // btnProfile
            // 
            btnProfile.Cursor = Cursors.Hand;
            btnProfile.Dock = DockStyle.Right;
            btnProfile.FlatAppearance.BorderSize = 0;
            btnProfile.FlatStyle = FlatStyle.Flat;
            btnProfile.Location = new Point(306, 0);
            btnProfile.Name = "btnProfile";
            btnProfile.Size = new Size(95, 77);
            btnProfile.TabIndex = 0;
            btnProfile.UseVisualStyleBackColor = true;
            btnProfile.Click += btnProfile_Click;
            btnProfile.MouseEnter += btnHeader_MouseEnter;
            btnProfile.MouseLeave += btnHeader_MouseLeave;
            // 
            // btnNotifications
            // 
            btnNotifications.BackgroundImageLayout = ImageLayout.Zoom;
            btnNotifications.Cursor = Cursors.Hand;
            btnNotifications.Dock = DockStyle.Right;
            btnNotifications.FlatAppearance.BorderSize = 0;
            btnNotifications.FlatStyle = FlatStyle.Flat;
            btnNotifications.Image = (Image)resources.GetObject("btnNotifications.Image");
            btnNotifications.Location = new Point(401, 0);
            btnNotifications.Name = "btnNotifications";
            btnNotifications.Size = new Size(95, 77);
            btnNotifications.TabIndex = 1;
            btnNotifications.UseVisualStyleBackColor = true;
            btnNotifications.Click += btnNotifications_Click;
            btnNotifications.MouseEnter += btnHeader_MouseEnter;
            btnNotifications.MouseLeave += btnHeader_MouseLeave;
            // 
            // btnLogout
            // 
            btnLogout.BackgroundImageLayout = ImageLayout.Zoom;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Right;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Image = (Image)resources.GetObject("btnLogout.Image");
            btnLogout.Location = new Point(496, 0);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(73, 77);
            btnLogout.TabIndex = 2;
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            btnLogout.MouseEnter += btnHeader_MouseEnter;
            btnLogout.MouseLeave += btnHeader_MouseLeave;
            // 
            // panelContent
            // 
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(280, 77);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(569, 440);
            panelContent.TabIndex = 2;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(849, 517);
            Controls.Add(panelContent);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "He thong quan ly van chuyen";
            WindowState = FormWindowState.Maximized;
            Load += FrmMain_Load;
            panel1.ResumeLayout(false);
            ((ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panel1;
        private Button btnReports;
        private Button btnAdmin;
        private Button btnMyProfile;
        private Button btnSettings;
        private Button btnDocuments;
        private Button btnWarehouses;
        private Button btnMaintenance;
        private Button btnVehicles;
        private Button btnDrivers;
        private Button btnCustomers;
        private Button btnDispatch;
        private Button btnOrders;
        private Button btnDashboard;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Label lblStatus;
        private Label lblWelcome;
        private Button btnProfile;
        private Button btnNotifications;
        private Button btnLogout;
        private Panel panelContent;
        private Button btnThemeToggle;
    }
}
