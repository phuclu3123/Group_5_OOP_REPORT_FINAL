using System;
using System.Drawing;
using System.Windows.Forms;
using Logistics.WinFormsUI.Extensions;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucOrderCard : UserControl
    {
        private Logistics.Core.DTOs.OrderDTO _orderData = null!;
        private Label lblTitle = null!;
        private Label lblValue = null!;
        private Panel pnlAccent = null!;

        public ucOrderCard()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.SetRoundedBorder(20);

            // Accent Bar
            pnlAccent.BackColor = Color.FromArgb(77, 168, 218);
            pnlAccent.Dock = DockStyle.Left;
            pnlAccent.Width = 5;
            this.Controls.Add(pnlAccent);

            // Title
            lblTitle.Text = "MÃ ĐƠN HÀNG";
            lblTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DimGray;
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            // Value
            lblValue.Text = "#ORD-000";
            lblValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblValue.ForeColor = Color.Black;
            lblValue.Location = new Point(18, 50);
            lblValue.AutoSize = true;
            this.Controls.Add(lblValue);
        }

        public void BindData(Logistics.Core.DTOs.OrderDTO order)
        {
            if (order == null) return;
            _orderData = order;

            lblValue.Text = order.OrderId;
            
            // Đổi màu thanh accent theo trạng thái
            switch (order.Status.ToLower())
            {
                case "pending":
                case "đang chờ":
                    pnlAccent.BackColor = Color.Orange;
                    break;
                case "delivered":
                case "đã giao":
                    pnlAccent.BackColor = Color.Green;
                    break;
                case "cancelled":
                case "đã hủy":
                    pnlAccent.BackColor = Color.Red;
                    break;
                default:
                    pnlAccent.BackColor = Color.FromArgb(77, 168, 218);
                    break;
            }
        }
    }
}
