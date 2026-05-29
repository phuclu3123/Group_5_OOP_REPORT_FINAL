namespace Logistics.WinFormsUI.Forms
{
    partial class FrmWarehouse
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.DataGridView dgvWarehouses;

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
            dgvWarehouses = new System.Windows.Forms.DataGridView();
            pnlHeader.SuspendLayout();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWarehouses).BeginInit();
            SuspendLayout();
            pnlHeader.BackColor = System.Drawing.Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnAdd);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Size = new System.Drawing.Size(1050, 76);
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(24, 22);
            lblTitle.Text = "Quan ly kho bai";
            btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAdd.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.Location = new System.Drawing.Point(880, 22);
            btnAdd.Size = new System.Drawing.Size(140, 34);
            btnAdd.Text = "Them kho";
            pnlSearch.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(cbType);
            pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            pnlSearch.Size = new System.Drawing.Size(1050, 64);
            txtSearch.Location = new System.Drawing.Point(24, 20);
            txtSearch.PlaceholderText = "Tim kho bai...";
            txtSearch.Size = new System.Drawing.Size(320, 23);
            cbType.Location = new System.Drawing.Point(364, 20);
            cbType.Size = new System.Drawing.Size(180, 23);
            cbType.Text = "Loai kho";
            dgvWarehouses.AllowUserToAddRows = false;
            dgvWarehouses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvWarehouses.BackgroundColor = System.Drawing.Color.White;
            dgvWarehouses.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvWarehouses.Location = new System.Drawing.Point(0, 140);
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1050, 680);
            Controls.Add(dgvWarehouses);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Name = "FrmWarehouse";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Warehouse Management";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWarehouses).EndInit();
            ResumeLayout(false);
        }
    }
}
