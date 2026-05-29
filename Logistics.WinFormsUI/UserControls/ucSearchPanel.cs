using System;
using System.Drawing;
using System.Windows.Forms;
using Logistics.WinFormsUI.Extensions;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucSearchPanel : UserControl
    {
        private TextBox txtSearch = null!;
        private Button btnFilter = null!;
        private Panel pnlContainer = null!;

        public ucSearchPanel()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            pnlContainer.Dock = DockStyle.Fill;
            pnlContainer.BackColor = Color.White;
            pnlContainer.SetRoundedBorder(25);
            this.Controls.Add(pnlContainer);

            // TextBox tìm kiếm
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(20, 15);
            txtSearch.Size = new Size(380, 25);
            txtSearch.SetPlaceholder("Tìm kiếm đơn hàng, khách hàng...");
            pnlContainer.Controls.Add(txtSearch);

            // Nút Filter
            btnFilter.Text = "Lọc 🔍";
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.FlatAppearance.BorderSize = 0;
            btnFilter.BackColor = Color.FromArgb(77, 168, 218);
            btnFilter.ForeColor = Color.White;
            btnFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFilter.Size = new Size(80, 34);
            btnFilter.Location = new Point(410, 8);
            btnFilter.SetRoundedBorder(17);
            pnlContainer.Controls.Add(btnFilter);
        }
    }
}
