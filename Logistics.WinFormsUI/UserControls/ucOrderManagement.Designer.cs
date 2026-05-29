using System.Drawing;
using System.Windows.Forms;

namespace Logistics.WinFormsUI.UserControls
{
    partial class ucOrderManagement
    {
        private Label lblLegend;

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnCreateOrder = new Button();
            lblLegend = new Label();
            lblTitle = new Label();
            pnlSearch = new Panel();
            cbStatusFilter = new ComboBox();
            txtSearch = new TextBox();
            dgvOrders = new DataGridView();

            pnlHeader.SuspendLayout();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();

            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(btnCreateOrder);
            pnlHeader.Controls.Add(lblLegend);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(950, 70);

            btnCreateOrder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCreateOrder.BackColor = Color.FromArgb(46, 204, 113);
            btnCreateOrder.FlatAppearance.BorderSize = 0;
            btnCreateOrder.FlatStyle = FlatStyle.Flat;
            btnCreateOrder.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCreateOrder.ForeColor = Color.White;
            btnCreateOrder.Location = new Point(740, 15);
            btnCreateOrder.Name = "btnCreateOrder";
            btnCreateOrder.Size = new Size(180, 40);
            btnCreateOrder.Text = "+ Tạo đơn hàng mới";
            btnCreateOrder.UseVisualStyleBackColor = false;
            btnCreateOrder.Click += btnCreateOrder_Click;

            lblLegend.AutoSize = true;
            lblLegend.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            lblLegend.ForeColor = Color.FromArgb(90, 98, 104);
            lblLegend.Location = new Point(20, 48);
            lblLegend.Name = "lblLegend";
            lblLegend.Text = "Màu trạng thái: Chờ xử lý=vàng, Đang vận chuyển=xanh dương, Đã giao=xanh lá, Sự cố/Hủy=đỏ. Dùng nút Sửa TT để sửa.";

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "QUẢN LÝ ĐƠN HÀNG";

            pnlSearch.BackColor = Color.WhiteSmoke;
            pnlSearch.Controls.Add(cbStatusFilter);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 70);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Padding = new Padding(20, 10, 20, 10);
            pnlSearch.Size = new Size(950, 60);

            cbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatusFilter.Font = new Font("Segoe UI", 10F);
            cbStatusFilter.Items.Add("Tất cả trạng thái");
            cbStatusFilter.Items.Add("Chờ xử lý");
            cbStatusFilter.Items.Add("Đang vận chuyển");
            cbStatusFilter.Items.Add("Đã giao");
            cbStatusFilter.Items.Add("Đã hủy");
            cbStatusFilter.Items.Add("Giao thất bại");
            cbStatusFilter.Location = new Point(350, 15);
            cbStatusFilter.Name = "cbStatusFilter";
            cbStatusFilter.Size = new Size(190, 25);
            cbStatusFilter.SelectedIndex = 0;
            cbStatusFilter.SelectedIndexChanged += FilterChanged;

            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(25, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm mã vận đơn...";
            txtSearch.Size = new Size(300, 27);
            txtSearch.TextChanged += FilterChanged;

            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.Dock = DockStyle.Fill;
            dgvOrders.Location = new Point(0, 130);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowTemplate.Height = 35;
            dgvOrders.CellClick += dgvOrders_CellClick;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(dgvOrders);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
            Name = "ucOrderManagement";
            Size = new Size(950, 600);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);
        }
    }
}
