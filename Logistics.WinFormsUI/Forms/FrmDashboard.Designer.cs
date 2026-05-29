namespace Logistics.WinFormsUI.Forms
{
    partial class FrmDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel pnlStats;
        private System.Windows.Forms.Panel pnlOrders;
        private System.Windows.Forms.Panel pnlRevenue;
        private System.Windows.Forms.Panel pnlVehicles;
        private System.Windows.Forms.Panel pnlWarehouses;
        private System.Windows.Forms.Label lblOrders;
        private System.Windows.Forms.Label lblRevenue;
        private System.Windows.Forms.Label lblVehicles;
        private System.Windows.Forms.Label lblWarehouses;
        private System.Windows.Forms.DataGridView dgvRecentActivity;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            pnlStats = new System.Windows.Forms.FlowLayoutPanel();
            pnlOrders = new System.Windows.Forms.Panel();
            pnlRevenue = new System.Windows.Forms.Panel();
            pnlVehicles = new System.Windows.Forms.Panel();
            pnlWarehouses = new System.Windows.Forms.Panel();
            lblOrders = new System.Windows.Forms.Label();
            lblRevenue = new System.Windows.Forms.Label();
            lblVehicles = new System.Windows.Forms.Label();
            lblWarehouses = new System.Windows.Forms.Label();
            dgvRecentActivity = new System.Windows.Forms.DataGridView();
            pnlHeader.SuspendLayout();
            pnlStats.SuspendLayout();
            pnlOrders.SuspendLayout();
            pnlRevenue.SuspendLayout();
            pnlVehicles.SuspendLayout();
            pnlWarehouses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentActivity).BeginInit();
            SuspendLayout();
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Size = new System.Drawing.Size(1200, 78);
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(24, 20);
            lblTitle.Text = "Dashboard";
            pnlStats.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlStats.Controls.Add(pnlOrders);
            pnlStats.Controls.Add(pnlRevenue);
            pnlStats.Controls.Add(pnlVehicles);
            pnlStats.Controls.Add(pnlWarehouses);
            pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            pnlStats.Padding = new System.Windows.Forms.Padding(20);
            pnlStats.Size = new System.Drawing.Size(1200, 160);
            pnlOrders.BackColor = System.Drawing.Color.White;
            pnlOrders.Controls.Add(lblOrders);
            pnlOrders.Margin = new System.Windows.Forms.Padding(10);
            pnlOrders.Size = new System.Drawing.Size(250, 100);
            lblOrders.AutoSize = true;
            lblOrders.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblOrders.Location = new System.Drawing.Point(18, 34);
            lblOrders.Text = "Tong don hang";
            pnlRevenue.BackColor = System.Drawing.Color.White;
            pnlRevenue.Controls.Add(lblRevenue);
            pnlRevenue.Margin = new System.Windows.Forms.Padding(10);
            pnlRevenue.Size = new System.Drawing.Size(250, 100);
            lblRevenue.AutoSize = true;
            lblRevenue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblRevenue.Location = new System.Drawing.Point(18, 34);
            lblRevenue.Text = "Doanh thu";
            pnlVehicles.BackColor = System.Drawing.Color.White;
            pnlVehicles.Controls.Add(lblVehicles);
            pnlVehicles.Margin = new System.Windows.Forms.Padding(10);
            pnlVehicles.Size = new System.Drawing.Size(250, 100);
            lblVehicles.AutoSize = true;
            lblVehicles.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblVehicles.Location = new System.Drawing.Point(18, 34);
            lblVehicles.Text = "Phuong tien";
            pnlWarehouses.BackColor = System.Drawing.Color.White;
            pnlWarehouses.Controls.Add(lblWarehouses);
            pnlWarehouses.Margin = new System.Windows.Forms.Padding(10);
            pnlWarehouses.Size = new System.Drawing.Size(250, 100);
            lblWarehouses.AutoSize = true;
            lblWarehouses.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblWarehouses.Location = new System.Drawing.Point(18, 34);
            lblWarehouses.Text = "Kho bai";
            dgvRecentActivity.AllowUserToAddRows = false;
            dgvRecentActivity.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecentActivity.BackgroundColor = System.Drawing.Color.White;
            dgvRecentActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvRecentActivity.Location = new System.Drawing.Point(0, 238);
            dgvRecentActivity.Name = "dgvRecentActivity";
            dgvRecentActivity.Size = new System.Drawing.Size(1200, 562);
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.WhiteSmoke;
            ClientSize = new System.Drawing.Size(1200, 800);
            Controls.Add(dgvRecentActivity);
            Controls.Add(pnlStats);
            Controls.Add(pnlHeader);
            Name = "FrmDashboard";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Dashboard - Statistics & Analytics";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlStats.ResumeLayout(false);
            pnlOrders.ResumeLayout(false);
            pnlOrders.PerformLayout();
            pnlRevenue.ResumeLayout(false);
            pnlRevenue.PerformLayout();
            pnlVehicles.ResumeLayout(false);
            pnlVehicles.PerformLayout();
            pnlWarehouses.ResumeLayout(false);
            pnlWarehouses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentActivity).EndInit();
            ResumeLayout(false);
        }
    }
}
