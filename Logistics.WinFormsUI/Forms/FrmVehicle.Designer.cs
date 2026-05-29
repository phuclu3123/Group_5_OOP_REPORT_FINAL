namespace Logistics.WinFormsUI.Forms
{
    partial class FrmVehicle
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.DataGridView dgvVehicles;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            btnAdd = new System.Windows.Forms.Button();
            pnlSearch = new System.Windows.Forms.Panel();
            txtSearch = new System.Windows.Forms.TextBox();
            cbType = new System.Windows.Forms.ComboBox();
            dgvVehicles = new System.Windows.Forms.DataGridView();
            pnlHeader.SuspendLayout();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).BeginInit();
            SuspendLayout();
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnAdd);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Size = new System.Drawing.Size(1000, 76);
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(24, 22);
            lblTitle.Text = "Quan ly phuong tien";
            btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAdd.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.Location = new System.Drawing.Point(815, 22);
            btnAdd.Size = new System.Drawing.Size(155, 34);
            btnAdd.Text = "Them phuong tien";
            pnlSearch.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(cbType);
            pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            pnlSearch.Size = new System.Drawing.Size(1000, 64);
            txtSearch.Location = new System.Drawing.Point(24, 20);
            txtSearch.PlaceholderText = "Tim bien so xe...";
            txtSearch.Size = new System.Drawing.Size(300, 23);
            cbType.Location = new System.Drawing.Point(344, 20);
            cbType.Size = new System.Drawing.Size(180, 23);
            cbType.Text = "Loai xe";
            dgvVehicles.AllowUserToAddRows = false;
            dgvVehicles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvVehicles.BackgroundColor = System.Drawing.Color.White;
            dgvVehicles.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvVehicles.Location = new System.Drawing.Point(0, 140);
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1000, 650);
            Controls.Add(dgvVehicles);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Name = "FrmVehicle";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Vehicle Management";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).EndInit();
            ResumeLayout(false);
        }
    }
}
