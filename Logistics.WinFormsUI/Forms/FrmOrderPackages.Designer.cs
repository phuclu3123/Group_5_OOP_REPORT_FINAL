using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.Forms
{
    partial class FrmOrderPackages
    {
        private Label lblTitle = null!;
        private Button btnAdd = null!;
        private DataGridView dgvPackages = null!;
        private Label lblSummary = null!;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            btnAdd = new Button();
            dgvPackages = new DataGridView();
            lblSummary = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPackages).BeginInit();
            SuspendLayout();
            BackColor = Color.White;
            ClientSize = new Size(980, 600);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chi tiết kiện hàng";
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 18);
            lblTitle.Size = new Size(650, 34);
            lblTitle.Text = "Danh sách kiện hàng của đơn";
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(830, 18);
            btnAdd.Size = new Size(130, 36);
            btnAdd.Text = "Thêm kiện hàng";
            btnAdd.Click += BtnAdd_Click;
            dgvPackages.AllowUserToAddRows = false;
            dgvPackages.AllowUserToDeleteRows = false;
            dgvPackages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPackages.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPackages.BackgroundColor = Color.White;
            dgvPackages.Location = new Point(20, 70);
            dgvPackages.MultiSelect = false;
            dgvPackages.Name = "dgvPackages";
            dgvPackages.ReadOnly = true;
            dgvPackages.RowHeadersVisible = false;
            dgvPackages.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPackages.Size = new Size(940, 455);
            dgvPackages.CellClick += DgvPackages_CellClick;
            lblSummary.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblSummary.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSummary.Location = new Point(20, 540);
            lblSummary.Size = new Size(940, 32);
            lblSummary.Text = "Số kiện: 0";
            Controls.Add(lblTitle);
            Controls.Add(btnAdd);
            Controls.Add(dgvPackages);
            Controls.Add(lblSummary);
            ((System.ComponentModel.ISupportInitialize)dgvPackages).EndInit();
            ResumeLayout(false);
        }
    }
}
