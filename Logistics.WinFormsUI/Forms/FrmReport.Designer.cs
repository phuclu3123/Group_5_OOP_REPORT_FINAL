namespace Logistics.WinFormsUI.Forms
{
    partial class FrmReport
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.FlowLayoutPanel pnlSummary;
        private System.Windows.Forms.Panel pnlOrdersCard;
        private System.Windows.Forms.Label lblTotalOrdersTitle;
        private System.Windows.Forms.Label lblTotalOrdersValue;
        private System.Windows.Forms.Panel pnlRevenueCard;
        private System.Windows.Forms.Label lblRevenueTitle;
        private System.Windows.Forms.Label lblRevenueValue;
        private System.Windows.Forms.Panel pnlSuccessCard;
        private System.Windows.Forms.Label lblSuccessRateTitle;
        private System.Windows.Forms.Label lblSuccessRateValue;
        private System.Windows.Forms.Panel pnlResourceCard;
        private System.Windows.Forms.Label lblResourcesTitle;
        private System.Windows.Forms.Label lblResourcesValue;
        private System.Windows.Forms.DataGridView dgvReport;

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
            pnlHeader = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            btnRefresh = new System.Windows.Forms.Button();
            btnExport = new System.Windows.Forms.Button();
            pnlSummary = new System.Windows.Forms.FlowLayoutPanel();
            pnlOrdersCard = new System.Windows.Forms.Panel();
            lblTotalOrdersTitle = new System.Windows.Forms.Label();
            lblTotalOrdersValue = new System.Windows.Forms.Label();
            pnlRevenueCard = new System.Windows.Forms.Panel();
            lblRevenueTitle = new System.Windows.Forms.Label();
            lblRevenueValue = new System.Windows.Forms.Label();
            pnlSuccessCard = new System.Windows.Forms.Panel();
            lblSuccessRateTitle = new System.Windows.Forms.Label();
            lblSuccessRateValue = new System.Windows.Forms.Label();
            pnlResourceCard = new System.Windows.Forms.Panel();
            lblResourcesTitle = new System.Windows.Forms.Label();
            lblResourcesValue = new System.Windows.Forms.Label();
            dgvReport = new System.Windows.Forms.DataGridView();
            pnlHeader.SuspendLayout();
            pnlSummary.SuspendLayout();
            pnlOrdersCard.SuspendLayout();
            pnlRevenueCard.SuspendLayout();
            pnlSuccessCard.SuspendLayout();
            pnlResourceCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Controls.Add(btnExport);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(1050, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            lblTitle.Location = new System.Drawing.Point(24, 23);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(235, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BAO CAO QUAN TRI";
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.Location = new System.Drawing.Point(778, 22);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(116, 38);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Lam moi";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnExport
            // 
            btnExport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnExport.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnExport.ForeColor = System.Drawing.Color.White;
            btnExport.Location = new System.Drawing.Point(910, 22);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(116, 38);
            btnExport.TabIndex = 2;
            btnExport.Text = "Xuat nhanh";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += btnExport_Click;
            // 
            // pnlSummary
            // 
            pnlSummary.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlSummary.Controls.Add(pnlOrdersCard);
            pnlSummary.Controls.Add(pnlRevenueCard);
            pnlSummary.Controls.Add(pnlSuccessCard);
            pnlSummary.Controls.Add(pnlResourceCard);
            pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            pnlSummary.Location = new System.Drawing.Point(0, 80);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Padding = new System.Windows.Forms.Padding(20);
            pnlSummary.Size = new System.Drawing.Size(1050, 160);
            pnlSummary.TabIndex = 1;
            // 
            // pnlOrdersCard
            // 
            pnlOrdersCard.BackColor = System.Drawing.Color.White;
            pnlOrdersCard.Controls.Add(lblTotalOrdersTitle);
            pnlOrdersCard.Controls.Add(lblTotalOrdersValue);
            pnlOrdersCard.Location = new System.Drawing.Point(30, 30);
            pnlOrdersCard.Margin = new System.Windows.Forms.Padding(10);
            pnlOrdersCard.Name = "pnlOrdersCard";
            pnlOrdersCard.Size = new System.Drawing.Size(230, 100);
            pnlOrdersCard.TabIndex = 0;
            // 
            // lblTotalOrdersTitle
            // 
            lblTotalOrdersTitle.AutoSize = true;
            lblTotalOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblTotalOrdersTitle.ForeColor = System.Drawing.Color.Gray;
            lblTotalOrdersTitle.Location = new System.Drawing.Point(18, 18);
            lblTotalOrdersTitle.Name = "lblTotalOrdersTitle";
            lblTotalOrdersTitle.Size = new System.Drawing.Size(95, 15);
            lblTotalOrdersTitle.TabIndex = 0;
            lblTotalOrdersTitle.Text = "TONG DON HANG";
            // 
            // lblTotalOrdersValue
            // 
            lblTotalOrdersValue.AutoSize = true;
            lblTotalOrdersValue.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            lblTotalOrdersValue.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
            lblTotalOrdersValue.Location = new System.Drawing.Point(16, 42);
            lblTotalOrdersValue.Name = "lblTotalOrdersValue";
            lblTotalOrdersValue.Size = new System.Drawing.Size(35, 41);
            lblTotalOrdersValue.TabIndex = 1;
            lblTotalOrdersValue.Text = "0";
            // 
            // pnlRevenueCard
            // 
            pnlRevenueCard.BackColor = System.Drawing.Color.White;
            pnlRevenueCard.Controls.Add(lblRevenueTitle);
            pnlRevenueCard.Controls.Add(lblRevenueValue);
            pnlRevenueCard.Location = new System.Drawing.Point(280, 30);
            pnlRevenueCard.Margin = new System.Windows.Forms.Padding(10);
            pnlRevenueCard.Name = "pnlRevenueCard";
            pnlRevenueCard.Size = new System.Drawing.Size(230, 100);
            pnlRevenueCard.TabIndex = 1;
            // 
            // lblRevenueTitle
            // 
            lblRevenueTitle.AutoSize = true;
            lblRevenueTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblRevenueTitle.ForeColor = System.Drawing.Color.Gray;
            lblRevenueTitle.Location = new System.Drawing.Point(18, 18);
            lblRevenueTitle.Name = "lblRevenueTitle";
            lblRevenueTitle.Size = new System.Drawing.Size(70, 15);
            lblRevenueTitle.TabIndex = 0;
            lblRevenueTitle.Text = "DOANH THU";
            // 
            // lblRevenueValue
            // 
            lblRevenueValue.AutoSize = true;
            lblRevenueValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblRevenueValue.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            lblRevenueValue.Location = new System.Drawing.Point(16, 48);
            lblRevenueValue.Name = "lblRevenueValue";
            lblRevenueValue.Size = new System.Drawing.Size(83, 32);
            lblRevenueValue.TabIndex = 1;
            lblRevenueValue.Text = "0 VND";
            // 
            // pnlSuccessCard
            // 
            pnlSuccessCard.BackColor = System.Drawing.Color.White;
            pnlSuccessCard.Controls.Add(lblSuccessRateTitle);
            pnlSuccessCard.Controls.Add(lblSuccessRateValue);
            pnlSuccessCard.Location = new System.Drawing.Point(530, 30);
            pnlSuccessCard.Margin = new System.Windows.Forms.Padding(10);
            pnlSuccessCard.Name = "pnlSuccessCard";
            pnlSuccessCard.Size = new System.Drawing.Size(230, 100);
            pnlSuccessCard.TabIndex = 2;
            // 
            // lblSuccessRateTitle
            // 
            lblSuccessRateTitle.AutoSize = true;
            lblSuccessRateTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblSuccessRateTitle.ForeColor = System.Drawing.Color.Gray;
            lblSuccessRateTitle.Location = new System.Drawing.Point(18, 18);
            lblSuccessRateTitle.Name = "lblSuccessRateTitle";
            lblSuccessRateTitle.Size = new System.Drawing.Size(102, 15);
            lblSuccessRateTitle.TabIndex = 0;
            lblSuccessRateTitle.Text = "TY LE THANH CONG";
            // 
            // lblSuccessRateValue
            // 
            lblSuccessRateValue.AutoSize = true;
            lblSuccessRateValue.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            lblSuccessRateValue.ForeColor = System.Drawing.Color.FromArgb(155, 89, 182);
            lblSuccessRateValue.Location = new System.Drawing.Point(16, 42);
            lblSuccessRateValue.Name = "lblSuccessRateValue";
            lblSuccessRateValue.Size = new System.Drawing.Size(61, 41);
            lblSuccessRateValue.TabIndex = 1;
            lblSuccessRateValue.Text = "0%";
            // 
            // pnlResourceCard
            // 
            pnlResourceCard.BackColor = System.Drawing.Color.White;
            pnlResourceCard.Controls.Add(lblResourcesTitle);
            pnlResourceCard.Controls.Add(lblResourcesValue);
            pnlResourceCard.Location = new System.Drawing.Point(780, 30);
            pnlResourceCard.Margin = new System.Windows.Forms.Padding(10);
            pnlResourceCard.Name = "pnlResourceCard";
            pnlResourceCard.Size = new System.Drawing.Size(230, 100);
            pnlResourceCard.TabIndex = 3;
            // 
            // lblResourcesTitle
            // 
            lblResourcesTitle.AutoSize = true;
            lblResourcesTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblResourcesTitle.ForeColor = System.Drawing.Color.Gray;
            lblResourcesTitle.Location = new System.Drawing.Point(18, 18);
            lblResourcesTitle.Name = "lblResourcesTitle";
            lblResourcesTitle.Size = new System.Drawing.Size(127, 15);
            lblResourcesTitle.TabIndex = 0;
            lblResourcesTitle.Text = "XE SAN SANG / TONG";
            // 
            // lblResourcesValue
            // 
            lblResourcesValue.AutoSize = true;
            lblResourcesValue.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            lblResourcesValue.ForeColor = System.Drawing.Color.FromArgb(241, 196, 15);
            lblResourcesValue.Location = new System.Drawing.Point(16, 42);
            lblResourcesValue.Name = "lblResourcesValue";
            lblResourcesValue.Size = new System.Drawing.Size(64, 41);
            lblResourcesValue.TabIndex = 1;
            lblResourcesValue.Text = "0/0";
            // 
            // dgvReport
            // 
            dgvReport.AllowUserToAddRows = false;
            dgvReport.AllowUserToDeleteRows = false;
            dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.BackgroundColor = System.Drawing.Color.White;
            dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvReport.Location = new System.Drawing.Point(0, 240);
            dgvReport.Name = "dgvReport";
            dgvReport.ReadOnly = true;
            dgvReport.RowTemplate.Height = 35;
            dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvReport.Size = new System.Drawing.Size(1050, 460);
            dgvReport.TabIndex = 2;
            // 
            // FrmReport
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(1050, 700);
            Controls.Add(dgvReport);
            Controls.Add(pnlSummary);
            Controls.Add(pnlHeader);
            MinimumSize = new System.Drawing.Size(980, 620);
            Name = "FrmReport";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Bao cao quan tri";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSummary.ResumeLayout(false);
            pnlOrdersCard.ResumeLayout(false);
            pnlOrdersCard.PerformLayout();
            pnlRevenueCard.ResumeLayout(false);
            pnlRevenueCard.PerformLayout();
            pnlSuccessCard.ResumeLayout(false);
            pnlSuccessCard.PerformLayout();
            pnlResourceCard.ResumeLayout(false);
            pnlResourceCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
            ResumeLayout(false);
        }
    }
}
