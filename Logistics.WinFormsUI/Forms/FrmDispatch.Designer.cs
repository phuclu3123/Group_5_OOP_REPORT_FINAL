namespace Logistics.WinFormsUI.Forms
{
    partial class FrmDispatch
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.ComboBox cbVehicle;
        private System.Windows.Forms.ComboBox cbDriver;
        private System.Windows.Forms.Button btnSuggest;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.Button btnCompleteTrip;
        private System.Windows.Forms.Button btnCancelTrip;
        private System.Windows.Forms.DataGridView dgvDispatch;
        private System.Windows.Forms.Label lblOrders;
        private System.Windows.Forms.Label lblTrips;
        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.DataGridView dgvTrips;
        private Logistics.WinFormsUI.UserControls.RouteMapControl routeMap;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            pnlFilters = new System.Windows.Forms.Panel();
            cbVehicle = new System.Windows.Forms.ComboBox();
            cbDriver = new System.Windows.Forms.ComboBox();
            btnSuggest = new System.Windows.Forms.Button();
            btnAssign = new System.Windows.Forms.Button();
            btnCompleteTrip = new System.Windows.Forms.Button();
            btnCancelTrip = new System.Windows.Forms.Button();
            lblOrders = new System.Windows.Forms.Label();
            lblTrips = new System.Windows.Forms.Label();
            lblMap = new System.Windows.Forms.Label();
            dgvDispatch = new System.Windows.Forms.DataGridView();
            dgvTrips = new System.Windows.Forms.DataGridView();
            routeMap = new Logistics.WinFormsUI.UserControls.RouteMapControl();
            pnlHeader.SuspendLayout();
            pnlFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDispatch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTrips).BeginInit();
            SuspendLayout();
            
            // pnlHeader
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Size = new System.Drawing.Size(1180, 78);
            
            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(24, 22);
            lblTitle.Text = "Điều phối vận chuyển";
            
            // pnlFilters
            pnlFilters.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlFilters.Controls.Add(cbVehicle);
            pnlFilters.Controls.Add(cbDriver);
            pnlFilters.Controls.Add(btnSuggest);
            pnlFilters.Controls.Add(btnAssign);
            pnlFilters.Controls.Add(btnCompleteTrip);
            pnlFilters.Controls.Add(btnCancelTrip);
            pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            pnlFilters.Size = new System.Drawing.Size(1180, 70);
            
            // cbVehicle
            cbVehicle.Location = new System.Drawing.Point(15, 22);
            cbVehicle.Size = new System.Drawing.Size(200, 23);
            cbVehicle.Text = "Chọn xe";
            
            // cbDriver
            cbDriver.Location = new System.Drawing.Point(225, 22);
            cbDriver.Size = new System.Drawing.Size(200, 23);
            cbDriver.Text = "Chọn tài xế";
            
            // btnSuggest
            btnSuggest.BackColor = System.Drawing.Color.FromArgb(108, 92, 231);
            btnSuggest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSuggest.ForeColor = System.Drawing.Color.White;
            btnSuggest.Location = new System.Drawing.Point(435, 18);
            btnSuggest.Size = new System.Drawing.Size(125, 32);
            btnSuggest.Text = "Gợi ý tối ưu ✨";
            btnSuggest.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSuggest.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnSuggest.Click += new System.EventHandler(this.BtnSuggest_Click);
            
            // btnAssign
            btnAssign.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnAssign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAssign.ForeColor = System.Drawing.Color.White;
            btnAssign.Location = new System.Drawing.Point(570, 18);
            btnAssign.Size = new System.Drawing.Size(125, 32);
            btnAssign.Text = "Gán điều phối";
            btnAssign.Click += new System.EventHandler(this.BtnAssign_Click);
            
            // btnCompleteTrip
            btnCompleteTrip.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnCompleteTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCompleteTrip.ForeColor = System.Drawing.Color.White;
            btnCompleteTrip.Location = new System.Drawing.Point(705, 18);
            btnCompleteTrip.Size = new System.Drawing.Size(125, 32);
            btnCompleteTrip.Text = "Hoàn tất chuyến";
            btnCompleteTrip.Click += new System.EventHandler(this.BtnCompleteTrip_Click);
            
            // btnCancelTrip
            btnCancelTrip.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            btnCancelTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancelTrip.ForeColor = System.Drawing.Color.White;
            btnCancelTrip.Location = new System.Drawing.Point(840, 18);
            btnCancelTrip.Size = new System.Drawing.Size(125, 32);
            btnCancelTrip.Text = "Hủy chuyến";
            btnCancelTrip.Click += new System.EventHandler(this.BtnCancelTrip_Click);
            
            // lblOrders
            lblOrders.AutoSize = true;
            lblOrders.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblOrders.Location = new System.Drawing.Point(24, 160);
            lblOrders.Text = "Đơn chờ điều phối";
            
            // lblTrips
            lblTrips.AutoSize = true;
            lblTrips.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblTrips.Location = new System.Drawing.Point(24, 420);
            lblTrips.Text = "Chuyến xe đang hoạt động";
            
            // lblMap
            lblMap.AutoSize = true;
            lblMap.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            lblMap.Location = new System.Drawing.Point(780, 160);
            lblMap.Text = "So do tuyen duong";
            
            // dgvDispatch
            dgvDispatch.AllowUserToAddRows = false;
            dgvDispatch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvDispatch.BackgroundColor = System.Drawing.Color.White;
            dgvDispatch.Location = new System.Drawing.Point(24, 190);
            dgvDispatch.MultiSelect = true;
            dgvDispatch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvDispatch.Size = new System.Drawing.Size(730, 210);
            dgvDispatch.SelectionChanged += new System.EventHandler(this.DgvDispatch_SelectionChanged);
            
            // dgvTrips
            dgvTrips.AllowUserToAddRows = false;
            dgvTrips.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvTrips.BackgroundColor = System.Drawing.Color.White;
            dgvTrips.Location = new System.Drawing.Point(24, 450);
            dgvTrips.ReadOnly = true;
            dgvTrips.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvTrips.Size = new System.Drawing.Size(730, 200);
            
            // routeMap
            routeMap.Location = new System.Drawing.Point(780, 190);
            routeMap.Name = "routeMap";
            routeMap.Size = new System.Drawing.Size(376, 460);
            routeMap.TabIndex = 9;
            
            // FrmDispatch
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1180, 680);
            Controls.Add(routeMap);
            Controls.Add(lblMap);
            Controls.Add(dgvTrips);
            Controls.Add(lblTrips);
            Controls.Add(lblOrders);
            Controls.Add(dgvDispatch);
            Controls.Add(pnlFilters);
            Controls.Add(pnlHeader);
            Name = "FrmDispatch";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Điều phối phương tiện";
            Load += new System.EventHandler(this.FrmDispatch_Load);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDispatch).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTrips).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
