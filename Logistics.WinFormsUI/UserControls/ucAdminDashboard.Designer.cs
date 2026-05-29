namespace Logistics.WinFormsUI.UserControls
{
    partial class ucAdminDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.title = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnTransactions = new System.Windows.Forms.Button();
            this.btnProblemReports = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnAccounts = new System.Windows.Forms.Button();
            this.btnAuditLog = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.title.Location = new System.Drawing.Point(28, 28);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(262, 32);
            this.title.TabIndex = 0;
            this.title.Text = "QUẢN TRỊ HỆ THỐNG";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.btnSettings);
            this.panel.Controls.Add(this.btnTransactions);
            this.panel.Controls.Add(this.btnProblemReports);
            this.panel.Controls.Add(this.btnChangePassword);
            this.panel.Controls.Add(this.btnAccounts);
            this.panel.Controls.Add(this.btnAuditLog);
            this.panel.Location = new System.Drawing.Point(28, 90);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(900, 220);
            this.panel.TabIndex = 1;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(0, 0);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(0, 0, 18, 18);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(190, 72);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.Text = "Cài đặt dữ liệu";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // btnTransactions
            // 
            this.btnTransactions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnTransactions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransactions.FlatAppearance.BorderSize = 0;
            this.btnTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransactions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTransactions.ForeColor = System.Drawing.Color.White;
            this.btnTransactions.Location = new System.Drawing.Point(208, 0);
            this.btnTransactions.Margin = new System.Windows.Forms.Padding(0, 0, 18, 18);
            this.btnTransactions.Name = "btnTransactions";
            this.btnTransactions.Size = new System.Drawing.Size(190, 72);
            this.btnTransactions.TabIndex = 1;
            this.btnTransactions.Text = "Sổ giao dịch";
            this.btnTransactions.UseVisualStyleBackColor = false;
            this.btnTransactions.Click += new System.EventHandler(this.BtnTransactions_Click);
            // 
            // btnProblemReports
            // 
            this.btnProblemReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnProblemReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProblemReports.FlatAppearance.BorderSize = 0;
            this.btnProblemReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProblemReports.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnProblemReports.ForeColor = System.Drawing.Color.White;
            this.btnProblemReports.Location = new System.Drawing.Point(416, 0);
            this.btnProblemReports.Margin = new System.Windows.Forms.Padding(0, 0, 18, 18);
            this.btnProblemReports.Name = "btnProblemReports";
            this.btnProblemReports.Size = new System.Drawing.Size(190, 72);
            this.btnProblemReports.TabIndex = 2;
            this.btnProblemReports.Text = "Sổ sự cố";
            this.btnProblemReports.UseVisualStyleBackColor = false;
            this.btnProblemReports.Click += new System.EventHandler(this.BtnProblemReports_Click);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(624, 0);
            this.btnChangePassword.Margin = new System.Windows.Forms.Padding(0, 0, 18, 18);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(190, 72);
            this.btnChangePassword.TabIndex = 3;
            this.btnChangePassword.Text = "Đổi mật khẩu";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.BtnChangePassword_Click);
            // 
            // btnAccounts
            // 
            this.btnAccounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAccounts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccounts.FlatAppearance.BorderSize = 0;
            this.btnAccounts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccounts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAccounts.ForeColor = System.Drawing.Color.White;
            this.btnAccounts.Location = new System.Drawing.Point(0, 90);
            this.btnAccounts.Margin = new System.Windows.Forms.Padding(0, 0, 18, 18);
            this.btnAccounts.Name = "btnAccounts";
            this.btnAccounts.Size = new System.Drawing.Size(190, 72);
            this.btnAccounts.TabIndex = 4;
            this.btnAccounts.Text = "Tài khoản";
            this.btnAccounts.UseVisualStyleBackColor = false;
            this.btnAccounts.Click += new System.EventHandler(this.BtnAccounts_Click);
            // 
            // btnAuditLog
            // 
            this.btnAuditLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAuditLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAuditLog.FlatAppearance.BorderSize = 0;
            this.btnAuditLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuditLog.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAuditLog.ForeColor = System.Drawing.Color.White;
            this.btnAuditLog.Location = new System.Drawing.Point(208, 90);
            this.btnAuditLog.Margin = new System.Windows.Forms.Padding(0, 0, 18, 18);
            this.btnAuditLog.Name = "btnAuditLog";
            this.btnAuditLog.Size = new System.Drawing.Size(190, 72);
            this.btnAuditLog.TabIndex = 5;
            this.btnAuditLog.Text = "Nhật ký";
            this.btnAuditLog.UseVisualStyleBackColor = false;
            this.btnAuditLog.Click += new System.EventHandler(this.BtnAuditLog_Click);
            // 
            // ucAdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.title);
            this.Name = "ucAdminDashboard";
            this.Size = new System.Drawing.Size(960, 400);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.FlowLayoutPanel panel;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnTransactions;
        private System.Windows.Forms.Button btnProblemReports;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnAccounts;
        private System.Windows.Forms.Button btnAuditLog;
    }
}
