using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    partial class ucCustomerManagement
    {
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnAddCustomer = new Button();
            lblTitle = new Label();
            pnlSearch = new Panel();
            txtSearch = new TextBox();
            dgvCustomers = new DataGridView();
            pnlHeader.SuspendLayout();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            SuspendLayout();
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(btnAddCustomer);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(950, 70);
            pnlHeader.TabIndex = 0;
            btnAddCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddCustomer.BackColor = Color.FromArgb(52, 152, 219);
            btnAddCustomer.FlatAppearance.BorderSize = 0;
            btnAddCustomer.FlatStyle = FlatStyle.Flat;
            btnAddCustomer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddCustomer.ForeColor = Color.White;
            btnAddCustomer.Location = new Point(750, 15);
            btnAddCustomer.Name = "btnAddCustomer";
            btnAddCustomer.Size = new Size(170, 40);
            btnAddCustomer.TabIndex = 1;
            btnAddCustomer.Text = "+ Them khach hang";
            btnAddCustomer.UseVisualStyleBackColor = false;
            btnAddCustomer.Click += BtnAddCustomer_Click;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(230, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUAN LY KHACH HANG";
            pnlSearch.BackColor = Color.WhiteSmoke;
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 70);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(20, 10, 20, 10);
            pnlSearch.Size = new Size(950, 60);
            pnlSearch.TabIndex = 1;
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(25, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tim theo ma, ten, dien thoai";
            txtSearch.Size = new Size(320, 27);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.BackgroundColor = Color.White;
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Location = new Point(0, 130);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowTemplate.Height = 35;
            dgvCustomers.Size = new Size(950, 470);
            dgvCustomers.TabIndex = 2;
            dgvCustomers.CellClick += DgvCustomers_CellClick;
            Controls.Add(dgvCustomers);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Name = "ucCustomerManagement";
            Size = new Size(950, 600);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ResumeLayout(false);
        }
    }
}
