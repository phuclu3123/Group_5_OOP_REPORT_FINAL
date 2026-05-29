namespace Logistics.WinFormsUI.UserControls
{
    partial class ucWarehouseManagement
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvWarehouses;
        private System.Windows.Forms.Panel pnlSearch;

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnlHeader = new Panel();
            btnAdd = new Button();
            lblTitle = new Label();
            pnlSearch = new Panel();
            txtSearch = new TextBox();
            dgvWarehouses = new DataGridView();
            pnlHeader.SuspendLayout();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWarehouses).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(btnAdd);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(900, 80);
            pnlHeader.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Right;
            btnAdd.BackColor = Color.FromArgb(52, 152, 219);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(730, 20);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(150, 40);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "+ Thêm Kho Bãi";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAdd_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(224, 32);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "QUẢN LÝ KHO BÃI";
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 80);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(20, 10, 20, 10);
            pnlSearch.Size = new Size(900, 60);
            pnlSearch.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.Dock = DockStyle.Left;
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(20, 10);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = " Tìm kiếm kho bãi (Tên, Mã, Địa chỉ)...";
            txtSearch.Size = new Size(350, 27);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // dgvWarehouses
            // 
            dgvWarehouses.AllowUserToAddRows = false;
            dgvWarehouses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvWarehouses.BackgroundColor = Color.White;
            dgvWarehouses.BorderStyle = BorderStyle.None;
            dgvWarehouses.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvWarehouses.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(242, 245, 250);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(242, 245, 250);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dgvWarehouses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvWarehouses.ColumnHeadersHeight = 45;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvWarehouses.DefaultCellStyle = dataGridViewCellStyle2;
            dgvWarehouses.Dock = DockStyle.Fill;
            dgvWarehouses.EnableHeadersVisualStyles = false;
            dgvWarehouses.GridColor = Color.FromArgb(239, 241, 243);
            dgvWarehouses.Location = new Point(0, 140);
            dgvWarehouses.Name = "dgvWarehouses";
            dgvWarehouses.RowHeadersVisible = false;
            dgvWarehouses.RowTemplate.Height = 40;
            dgvWarehouses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWarehouses.Size = new Size(900, 460);
            dgvWarehouses.TabIndex = 0;
            dgvWarehouses.CellContentClick += DgvWarehouses_CellContentClick;
            // 
            // ucWarehouseManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(dgvWarehouses);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Name = "ucWarehouseManagement";
            Size = new Size(900, 600);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWarehouses).EndInit();
            ResumeLayout(false);
        }
    }
}
