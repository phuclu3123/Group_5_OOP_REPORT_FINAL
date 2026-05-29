using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    partial class ucStaffManagement
    {
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnAddStaff = new Button();
            lblTitle = new Label();
            pnlSearch = new Panel();
            cbRoleFilter = new ComboBox();
            txtSearch = new TextBox();
            dgvStaff = new DataGridView();
            
            pnlHeader.SuspendLayout();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStaff).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(btnAddStaff);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(950, 70);
            pnlHeader.TabIndex = 0;
            // 
            // btnAddStaff
            // 
            btnAddStaff.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddStaff.BackColor = Color.FromArgb(52, 152, 219);
            btnAddStaff.FlatAppearance.BorderSize = 0;
            btnAddStaff.FlatStyle = FlatStyle.Flat;
            btnAddStaff.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddStaff.ForeColor = Color.White;
            btnAddStaff.Location = new Point(760, 15);
            btnAddStaff.Name = "btnAddStaff";
            btnAddStaff.Size = new Size(160, 40);
            btnAddStaff.TabIndex = 1;
            btnAddStaff.Text = "+ Thêm Nhân Viên";
            btnAddStaff.UseVisualStyleBackColor = false;
            btnAddStaff.Click += btnAddStaff_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(215, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ NHÂN SỰ";
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.WhiteSmoke;
            pnlSearch.Controls.Add(cbRoleFilter);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 70);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(20, 10, 20, 10);
            pnlSearch.Size = new Size(950, 60);
            pnlSearch.TabIndex = 1;
            // 
            // cbRoleFilter
            // 
            cbRoleFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRoleFilter.Font = new Font("Segoe UI", 10F);
            cbRoleFilter.Items.Add("Tất cả vai trò");
            cbRoleFilter.Items.Add("Tài xế");
            cbRoleFilter.Items.Add("Điều phối");
            cbRoleFilter.Items.Add("Nhân viên kho");
            cbRoleFilter.Location = new Point(350, 15);
            cbRoleFilter.Name = "cbRoleFilter";
            cbRoleFilter.Size = new Size(180, 25);
            cbRoleFilter.TabIndex = 1;
            cbRoleFilter.SelectedIndexChanged += FilterChanged;
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
            // dgvStaff
            // 
            dgvStaff.AllowUserToAddRows = false;
            dgvStaff.AllowUserToDeleteRows = false;
            dgvStaff.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStaff.BackgroundColor = Color.White;
            dgvStaff.Dock = DockStyle.Fill;
            dgvStaff.Location = new Point(0, 130);
            dgvStaff.Name = "dgvStaff";
            dgvStaff.ReadOnly = true;
            dgvStaff.RowTemplate.Height = 35;
            dgvStaff.Size = new Size(950, 470);
            dgvStaff.TabIndex = 2;
            dgvStaff.CellClick += dgvStaff_CellClick;
            // 
            // ucStaffManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(dgvStaff);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Name = "ucStaffManagement";
            Size = new Size(950, 600);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStaff).EndInit();
            ResumeLayout(false);
        }
    }
}
