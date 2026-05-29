namespace Logistics.WinFormsUI.Forms
{
    partial class FrmOrder
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.DataGridView dgvOrders;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            btnCreate = new System.Windows.Forms.Button();
            pnlSearch = new System.Windows.Forms.Panel();
            txtSearch = new System.Windows.Forms.TextBox();
            cbStatus = new System.Windows.Forms.ComboBox();
            dgvOrders = new System.Windows.Forms.DataGridView();
            pnlHeader.SuspendLayout();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnCreate);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Size = new System.Drawing.Size(1100, 76);
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(24, 22);
            lblTitle.Text = "Quản lý đơn hàng";
            btnCreate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnCreate.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCreate.ForeColor = System.Drawing.Color.White;
            btnCreate.Location = new System.Drawing.Point(920, 22);
            btnCreate.Size = new System.Drawing.Size(150, 34);
            btnCreate.Text = "Tạo đơn hàng";
            pnlSearch.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(cbStatus);
            pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            pnlSearch.Size = new System.Drawing.Size(1100, 64);
            txtSearch.Location = new System.Drawing.Point(24, 20);
            txtSearch.PlaceholderText = "Tìm mã vận đơn...";
            txtSearch.Size = new System.Drawing.Size(320, 23);
            cbStatus.Location = new System.Drawing.Point(364, 20);
            cbStatus.Size = new System.Drawing.Size(180, 23);
            cbStatus.Text = "Trạng thái";
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.BackgroundColor = System.Drawing.Color.White;
            dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvOrders.Location = new System.Drawing.Point(0, 140);
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1100, 700);
            Controls.Add(dgvOrders);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Name = "FrmOrder";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Quản lý đơn hàng";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);
        }
    }
}
