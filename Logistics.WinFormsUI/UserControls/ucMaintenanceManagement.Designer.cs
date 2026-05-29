using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    partial class ucMaintenanceManagement
    {
        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private ComboBox cbVehicles = null!;
        private TextBox txtVehicleId = null!;
        private Button btnAdd = null!;
        private Button btnSendMaintenance = null!;
        private Button btnComplete = null!;
        private CheckBox chkDueOnly = null!;
        private DataGridView dgvMaintenance = null!;

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();
            cbVehicles = new ComboBox();
            txtVehicleId = new TextBox();
            btnAdd = new Button();
            btnSendMaintenance = new Button();
            btnComplete = new Button();
            chkDueOnly = new CheckBox();
            dgvMaintenance = new DataGridView();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMaintenance).BeginInit();
            SuspendLayout();
            BackColor = Color.WhiteSmoke;
            Dock = DockStyle.Fill;
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(cbVehicles);
            pnlHeader.Controls.Add(txtVehicleId);
            pnlHeader.Controls.Add(btnAdd);
            pnlHeader.Controls.Add(btnSendMaintenance);
            pnlHeader.Controls.Add(btnComplete);
            pnlHeader.Controls.Add(chkDueOnly);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 92;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.Location = new Point(18, 14);
            lblTitle.Size = new Size(260, 32);
            lblTitle.Text = "Bảo trì phương tiện";
            cbVehicles.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVehicles.Location = new Point(20, 52);
            cbVehicles.Size = new Size(230, 27);
            cbVehicles.SelectedIndexChanged += CbVehicles_SelectedIndexChanged;
            txtVehicleId.Location = new Point(260, 52);
            txtVehicleId.PlaceholderText = "Mã xe";
            txtVehicleId.Size = new Size(120, 27);
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(390, 50);
            btnAdd.Size = new Size(120, 30);
            btnAdd.Text = "Thêm lịch sử";
            btnAdd.Click += BtnAdd_Click;
            btnSendMaintenance.BackColor = Color.FromArgb(241, 196, 15);
            btnSendMaintenance.FlatStyle = FlatStyle.Flat;
            btnSendMaintenance.ForeColor = Color.White;
            btnSendMaintenance.Location = new Point(520, 50);
            btnSendMaintenance.Size = new Size(120, 30);
            btnSendMaintenance.Text = "Đưa bảo trì";
            btnSendMaintenance.Click += BtnSendMaintenance_Click;
            btnComplete.BackColor = Color.FromArgb(52, 152, 219);
            btnComplete.FlatStyle = FlatStyle.Flat;
            btnComplete.ForeColor = Color.White;
            btnComplete.Location = new Point(650, 50);
            btnComplete.Size = new Size(130, 30);
            btnComplete.Text = "Hoàn tất";
            btnComplete.Click += BtnComplete_Click;
            chkDueOnly.Location = new Point(800, 52);
            chkDueOnly.Size = new Size(180, 28);
            chkDueOnly.Text = "Chỉ xe đến hạn";
            chkDueOnly.CheckedChanged += ChkDueOnly_CheckedChanged;
            dgvMaintenance.AllowUserToAddRows = false;
            dgvMaintenance.AllowUserToDeleteRows = false;
            dgvMaintenance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMaintenance.BackgroundColor = Color.White;
            dgvMaintenance.Dock = DockStyle.Fill;
            dgvMaintenance.MultiSelect = false;
            dgvMaintenance.ReadOnly = true;
            dgvMaintenance.RowHeadersVisible = false;
            dgvMaintenance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Controls.Add(dgvMaintenance);
            Controls.Add(pnlHeader);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMaintenance).EndInit();
            ResumeLayout(false);
        }
    }
}
