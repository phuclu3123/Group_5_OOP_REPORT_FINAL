namespace Logistics.WinFormsUI.UserControls
{
    partial class ucMyProfile
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
            this.header = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblAccountStatus = new System.Windows.Forms.Label();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.profileCard = new System.Windows.Forms.Panel();
            this.summaryCard = new System.Windows.Forms.Panel();
            this.summaryPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.workCard = new System.Windows.Forms.Panel();
            this.workTable = new System.Windows.Forms.TableLayoutPanel();
            this.activityCard = new System.Windows.Forms.Panel();
            this.dgvWork = new System.Windows.Forms.DataGridView();
            this.header.SuspendLayout();
            this.layout.SuspendLayout();
            this.summaryCard.SuspendLayout();
            this.workCard.SuspendLayout();
            this.activityCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWork)).BeginInit();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.Color.White;
            this.header.Controls.Add(this.lblName);
            this.header.Controls.Add(this.lblRole);
            this.header.Controls.Add(this.lblAccountStatus);
            this.header.Controls.Add(this.btnChangePassword);
            this.header.Controls.Add(this.btnSave);
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Height = 112;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Padding = new System.Windows.Forms.Padding(28, 20, 28, 16);
            this.header.Size = new System.Drawing.Size(1000, 112);
            this.header.TabIndex = 0;
            this.header.Resize += new System.EventHandler(this.Header_Resize);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblName.Location = new System.Drawing.Point(28, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 37);
            this.lblName.TabIndex = 0;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblRole.Location = new System.Drawing.Point(31, 62);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(0, 19);
            this.lblRole.TabIndex = 1;
            // 
            // lblAccountStatus
            // 
            this.lblAccountStatus.AutoSize = true;
            this.lblAccountStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAccountStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblAccountStatus.Location = new System.Drawing.Point(31, 84);
            this.lblAccountStatus.Name = "lblAccountStatus";
            this.lblAccountStatus.Size = new System.Drawing.Size(0, 19);
            this.lblAccountStatus.TabIndex = 2;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(620, 36);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(140, 38);
            this.btnChangePassword.TabIndex = 3;
            this.btnChangePassword.Text = "Đổi mật khẩu";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.BtnChangePassword_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(780, 36);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 38);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Lưu hồ sơ";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // layout
            // 
            this.layout.ColumnCount = 2;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Controls.Add(this.profileCard, 0, 0);
            this.layout.Controls.Add(this.summaryCard, 1, 0);
            this.layout.Controls.Add(this.workCard, 0, 1);
            this.layout.Controls.Add(this.activityCard, 1, 1);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 112);
            this.layout.Name = "layout";
            this.layout.Padding = new System.Windows.Forms.Padding(28, 24, 28, 24);
            this.layout.RowCount = 2;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Size = new System.Drawing.Size(1000, 688);
            this.layout.TabIndex = 1;
            // 
            // profileCard
            // 
            this.profileCard.BackColor = System.Drawing.Color.White;
            this.profileCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profileCard.Location = new System.Drawing.Point(28, 24);
            this.profileCard.Margin = new System.Windows.Forms.Padding(0, 0, 18, 18);
            this.profileCard.Name = "profileCard";
            this.profileCard.Padding = new System.Windows.Forms.Padding(18);
            this.profileCard.Size = new System.Drawing.Size(402, 262);
            this.profileCard.TabIndex = 0;
            // 
            // summaryCard
            // 
            this.summaryCard.BackColor = System.Drawing.Color.White;
            this.summaryCard.Controls.Add(this.summaryPanel);
            this.summaryCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryCard.Location = new System.Drawing.Point(448, 24);
            this.summaryCard.Margin = new System.Windows.Forms.Padding(0, 0, 18, 18);
            this.summaryCard.Name = "summaryCard";
            this.summaryCard.Padding = new System.Windows.Forms.Padding(18);
            this.summaryCard.Size = new System.Drawing.Size(524, 262);
            this.summaryCard.TabIndex = 1;
            // 
            // summaryPanel
            // 
            this.summaryPanel.AutoScroll = true;
            this.summaryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.summaryPanel.Location = new System.Drawing.Point(18, 18);
            this.summaryPanel.Name = "summaryPanel";
            this.summaryPanel.Padding = new System.Windows.Forms.Padding(18, 56, 18, 18);
            this.summaryPanel.Size = new System.Drawing.Size(488, 226);
            this.summaryPanel.TabIndex = 0;
            this.summaryPanel.WrapContents = true;
            // 
            // workCard
            // 
            this.workCard.BackColor = System.Drawing.Color.White;
            this.workCard.Controls.Add(this.workTable);
            this.workCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workCard.Location = new System.Drawing.Point(28, 304);
            this.workCard.Margin = new System.Windows.Forms.Padding(0, 0, 18, 18);
            this.workCard.Name = "workCard";
            this.workCard.Padding = new System.Windows.Forms.Padding(18);
            this.workCard.Size = new System.Drawing.Size(402, 342);
            this.workCard.TabIndex = 2;
            // 
            // workTable
            // 
            this.workTable.AutoScroll = true;
            this.workTable.ColumnCount = 2;
            this.workTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.workTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.workTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workTable.Location = new System.Drawing.Point(18, 18);
            this.workTable.Name = "workTable";
            this.workTable.Padding = new System.Windows.Forms.Padding(18, 56, 18, 18);
            this.workTable.Size = new System.Drawing.Size(366, 306);
            this.workTable.TabIndex = 0;
            // 
            // activityCard
            // 
            this.activityCard.BackColor = System.Drawing.Color.White;
            this.activityCard.Controls.Add(this.dgvWork);
            this.activityCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityCard.Location = new System.Drawing.Point(448, 304);
            this.activityCard.Margin = new System.Windows.Forms.Padding(0, 0, 18, 18);
            this.activityCard.Name = "activityCard";
            this.activityCard.Padding = new System.Windows.Forms.Padding(18, 56, 18, 18);
            this.activityCard.Size = new System.Drawing.Size(524, 342);
            this.activityCard.TabIndex = 3;
            // 
            // dgvWork
            // 
            this.dgvWork.AllowUserToAddRows = false;
            this.dgvWork.AllowUserToDeleteRows = false;
            this.dgvWork.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWork.BackgroundColor = System.Drawing.Color.White;
            this.dgvWork.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWork.Location = new System.Drawing.Point(18, 56);
            this.dgvWork.Name = "dgvWork";
            this.dgvWork.ReadOnly = true;
            this.dgvWork.RowHeadersVisible = false;
            this.dgvWork.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWork.Size = new System.Drawing.Size(488, 268);
            this.dgvWork.TabIndex = 0;
            // 
            // ucMyProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.layout);
            this.Controls.Add(this.header);
            this.Name = "ucMyProfile";
            this.Size = new System.Drawing.Size(1000, 800);
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            this.layout.ResumeLayout(false);
            this.summaryCard.ResumeLayout(false);
            this.workCard.ResumeLayout(false);
            this.activityCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWork)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblAccountStatus;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Panel profileCard;
        private System.Windows.Forms.Panel summaryCard;
        private System.Windows.Forms.FlowLayoutPanel summaryPanel;
        private System.Windows.Forms.Panel workCard;
        private System.Windows.Forms.TableLayoutPanel workTable;
        private System.Windows.Forms.Panel activityCard;
        private System.Windows.Forms.DataGridView dgvWork;
    }
}
