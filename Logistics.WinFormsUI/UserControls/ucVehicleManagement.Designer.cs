using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    partial class ucVehicleManagement
    {
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnAddVehicle = new Button();
            lblTitle = new Label();
            pnlSearch = new Panel();
            cbTypeFilter = new ComboBox();
            txtSearch = new TextBox();
            dgvVehicles = new DataGridView();
            
            pnlHeader.SuspendLayout();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(btnAddVehicle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(950, 70);
            pnlHeader.TabIndex = 0;
            // 
            // btnAddVehicle
            // 
            btnAddVehicle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddVehicle.BackColor = Color.FromArgb(241, 196, 15);
            btnAddVehicle.FlatAppearance.BorderSize = 0;
            btnAddVehicle.FlatStyle = FlatStyle.Flat;
            btnAddVehicle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddVehicle.ForeColor = Color.White;
            btnAddVehicle.Location = new Point(740, 15);
            btnAddVehicle.Name = "btnAddVehicle";
            btnAddVehicle.Size = new Size(180, 40);
            btnAddVehicle.TabIndex = 1;
            btnAddVehicle.Text = "+ Thêm Phương Tiện";
            btnAddVehicle.UseVisualStyleBackColor = false;
            btnAddVehicle.Click += btnAddVehicle_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(262, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ PHƯƠNG TIỆN";
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.WhiteSmoke;
            pnlSearch.Controls.Add(cbTypeFilter);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 70);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(20, 10, 20, 10);
            pnlSearch.Size = new Size(950, 60);
            pnlSearch.TabIndex = 1;
            // 
            // cbTypeFilter
            // 
            cbTypeFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTypeFilter.Font = new Font("Segoe UI", 10F);
            cbTypeFilter.Items.Add("Tất cả loại xe");
            cbTypeFilter.Items.Add("Motorbike");
            cbTypeFilter.Items.Add("Van");
            cbTypeFilter.Items.Add("Truck_1Ton");
            cbTypeFilter.Items.Add("ColdStorageTruck");
            cbTypeFilter.Location = new Point(350, 15);
            cbTypeFilter.Name = "cbTypeFilter";
            cbTypeFilter.Size = new Size(180, 25);
            cbTypeFilter.TabIndex = 1;
            cbTypeFilter.SelectedIndexChanged += FilterChanged;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(25, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(300, 27);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += FilterChanged;
            // 
            // dgvVehicles
            // 
            dgvVehicles.AllowUserToAddRows = false;
            dgvVehicles.AllowUserToDeleteRows = false;
            dgvVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVehicles.BackgroundColor = Color.White;
            dgvVehicles.Dock = DockStyle.Fill;
            dgvVehicles.Location = new Point(0, 130);
            dgvVehicles.Name = "dgvVehicles";
            dgvVehicles.ReadOnly = true;
            dgvVehicles.RowTemplate.Height = 35;
            dgvVehicles.Size = new Size(950, 470);
            dgvVehicles.TabIndex = 2;
            dgvVehicles.CellClick += dgvVehicles_CellClick;
            // 
            // ucVehicleManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(dgvVehicles);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Name = "ucVehicleManagement";
            Size = new Size(950, 600);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).EndInit();
            ResumeLayout(false);
        }
    }
}
