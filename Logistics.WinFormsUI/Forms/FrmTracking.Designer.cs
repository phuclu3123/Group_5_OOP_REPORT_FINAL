namespace Logistics.WinFormsUI.Forms
{
    partial class FrmTracking
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTrackingNumber;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblRoute;
        private System.Windows.Forms.SplitContainer splitContent;
        private Logistics.WinFormsUI.UserControls.RouteMapControl routeMap;
        private System.Windows.Forms.DataGridView dgvTimeline;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            txtTrackingNumber = new System.Windows.Forms.TextBox();
            btnSearch = new System.Windows.Forms.Button();
            pnlSummary = new System.Windows.Forms.Panel();
            lblStatus = new System.Windows.Forms.Label();
            lblRoute = new System.Windows.Forms.Label();
            splitContent = new System.Windows.Forms.SplitContainer();
            routeMap = new Logistics.WinFormsUI.UserControls.RouteMapControl();
            dgvTimeline = new System.Windows.Forms.DataGridView();
            pnlHeader.SuspendLayout();
            pnlSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContent).BeginInit();
            splitContent.Panel1.SuspendLayout();
            splitContent.Panel2.SuspendLayout();
            splitContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTimeline).BeginInit();
            SuspendLayout();
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(txtTrackingNumber);
            pnlHeader.Controls.Add(btnSearch);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Size = new System.Drawing.Size(900, 96);
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(24, 18);
            lblTitle.Text = "Theo doi don hang";
            txtTrackingNumber.Location = new System.Drawing.Point(24, 58);
            txtTrackingNumber.PlaceholderText = "Nhap ma van don";
            txtTrackingNumber.Size = new System.Drawing.Size(300, 23);
            btnSearch.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSearch.ForeColor = System.Drawing.Color.White;
            btnSearch.Location = new System.Drawing.Point(344, 54);
            btnSearch.Size = new System.Drawing.Size(100, 30);
            btnSearch.Text = "Tim";
            btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            pnlSummary.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlSummary.Controls.Add(lblStatus);
            pnlSummary.Controls.Add(lblRoute);
            pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            pnlSummary.Size = new System.Drawing.Size(900, 96);
            lblStatus.AutoSize = true;
            lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblStatus.Location = new System.Drawing.Point(24, 22);
            lblStatus.Text = "Trang thai: --";
            lblRoute.AutoSize = true;
            lblRoute.Location = new System.Drawing.Point(24, 56);
            lblRoute.Text = "Tuyen duong: --";
            splitContent.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContent.Location = new System.Drawing.Point(0, 192);
            splitContent.Name = "splitContent";
            splitContent.Orientation = System.Windows.Forms.Orientation.Horizontal;
            splitContent.Panel1.Controls.Add(routeMap);
            splitContent.Panel2.Controls.Add(dgvTimeline);
            splitContent.Size = new System.Drawing.Size(900, 458);
            splitContent.SplitterDistance = 250;
            splitContent.TabIndex = 2;
            routeMap.Dock = System.Windows.Forms.DockStyle.Fill;
            routeMap.Location = new System.Drawing.Point(0, 0);
            routeMap.Name = "routeMap";
            routeMap.Size = new System.Drawing.Size(900, 250);
            routeMap.TabIndex = 0;
            dgvTimeline.AllowUserToAddRows = false;
            dgvTimeline.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvTimeline.BackgroundColor = System.Drawing.Color.White;
            dgvTimeline.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvTimeline.Location = new System.Drawing.Point(0, 0);
            dgvTimeline.Name = "dgvTimeline";
            dgvTimeline.Size = new System.Drawing.Size(900, 204);
            dgvTimeline.TabIndex = 0;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 650);
            Controls.Add(splitContent);
            Controls.Add(pnlSummary);
            Controls.Add(pnlHeader);
            Name = "FrmTracking";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Order Tracking";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSummary.ResumeLayout(false);
            pnlSummary.PerformLayout();
            splitContent.Panel1.ResumeLayout(false);
            splitContent.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContent).EndInit();
            splitContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTimeline).EndInit();
            ResumeLayout(false);
        }
    }
}
