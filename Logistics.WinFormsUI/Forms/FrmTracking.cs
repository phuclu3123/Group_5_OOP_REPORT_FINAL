using System.Windows.Forms;
using System.Collections.Generic;
using Logistics.Core.Models.Business;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmTracking : Form
    {
        public FrmTracking()
        {
            InitializeComponent();
        }

        public FrmTracking(string trackingNumber) : this()
        {
            txtTrackingNumber.Text = trackingNumber;
            LoadTracking();
        }

        private void BtnSearch_Click(object? sender, System.EventArgs e)
        {
            LoadTracking();
        }

        private void LoadTracking()
        {
            string trackingNumber = txtTrackingNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(trackingNumber))
            {
                MessageBox.Show("Vui long nhap ma van don.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Order order = DependencyContainer.GetOrderService().GetOrderById(trackingNumber);
            if (order == null)
            {
                lblStatus.Text = "Trang thai: khong tim thay";
                lblRoute.Text = "Tuyen duong: --";
                dgvTimeline.DataSource = null;
                routeMap.ClearRoute("Khong tim thay don hang");
                MessageBox.Show("Khong tim thay don hang.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lblStatus.Text = "Trang thai: " + order.CurrentStatus;
            lblRoute.Text = "Tuyen duong: " + (order.Route != null ? order.Route.ToString() : "--");
            routeMap.SetRoute(RouteMapBuilder.Build(order), RouteMapBuilder.BuildStatusText(order), RouteMapBuilder.BuildProgressText(order));

            List<TimelineRow> rows = new List<TimelineRow>();
            if (order.StatusHistories != null && order.StatusHistories.Count > 0)
            {
                foreach (OrderStatusHistory item in order.StatusHistories)
                {
                    rows.Add(new TimelineRow
                    {
                        Time = item.ChangedAt.ToString("dd/MM/yyyy HH:mm"),
                        Status = item.NewStatus.ToString(),
                        Location = item.Location,
                        Description = item.Description,
                        ChangedBy = item.ChangedBy
                    });
                }
            }
            else
            {
                rows.Add(new TimelineRow
                {
                    Time = order.CreatedDate.ToString("dd/MM/yyyy HH:mm"),
                    Status = order.CurrentStatus.ToString(),
                    Location = order.Route != null ? order.Route.ToString() : "",
                    Description = "Don hang duoc tao",
                    ChangedBy = "System"
                });
            }

            dgvTimeline.DataSource = rows;
        }

        private class TimelineRow
        {
            public string Time { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string Location { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string ChangedBy { get; set; } = string.Empty;
        }
    }
}
