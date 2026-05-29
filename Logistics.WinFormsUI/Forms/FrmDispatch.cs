using System.Windows.Forms;
using System.Collections.Generic;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Models.Common;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.Forms
{
    public partial class FrmDispatch : Form
    {
        public FrmDispatch()
        {
            InitializeComponent();
        }

        private void FrmDispatch_Load(object? sender, System.EventArgs e)
        {
            if (!DesignerHelper.IsInDesignMode(this))
            {
                LoadDispatchData();
            }
        }

        private void BtnAssign_Click(object? sender, System.EventArgs e)
        {
            if (dgvDispatch.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui long chon it nhat mot don hang can dieu phoi.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string vehicleId = cbVehicle.SelectedItem?.ToString() ?? string.Empty;
            string driverId = cbDriver.SelectedItem?.ToString() ?? string.Empty;
            List<string> orderIds = new List<string>();
            foreach (DataGridViewRow row in dgvDispatch.SelectedRows)
            {
                string orderId = System.Convert.ToString(row.Cells["TrackingNumber"].Value) ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(orderId))
                {
                    orderIds.Add(orderId);
                }
            }

            if (orderIds.Count == 0 || string.IsNullOrWhiteSpace(vehicleId) || string.IsNullOrWhiteSpace(driverId))
            {
                MessageBox.Show("Vui long chon day du don hang, xe va tai xe.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DeliveryTripDTO trip = DependencyContainer.GetDispatchService().CreateTripDispatch(vehicleId, driverId, orderIds);

                if (string.IsNullOrWhiteSpace(trip.TripID))
                {
                    MessageBox.Show("Dieu phoi khong thanh cong. Hay kiem tra trang thai xe/tai xe/don hang.", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDispatchData();
                    return;
                }

                MessageBox.Show("Da tao chuyen xe " + trip.TripID + " cho " + trip.OrderCount + " don hang.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDispatchData();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Loi Han Che Tai Trong / The Tich", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadDispatchData();
            }
        }

        private void BtnCompleteTrip_Click(object? sender, System.EventArgs e)
        {
            string tripId = GetSelectedTripId();
            if (string.IsNullOrWhiteSpace(tripId))
            {
                MessageBox.Show("Vui long chon mot chuyen xe.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool completed = DependencyContainer.GetDispatchService().CompleteTrip(tripId);
            if (!completed)
            {
                MessageBox.Show("Chua the hoan tat chuyen. Tat ca don trong chuyen phai o trang thai da giao, da huy, hoan tra hoac giao that bai.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadDispatchData();
                return;
            }

            MessageBox.Show("Da hoan tat chuyen xe " + tripId + ".", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDispatchData();
        }

        private void BtnCancelTrip_Click(object? sender, System.EventArgs e)
        {
            string tripId = GetSelectedTripId();
            if (string.IsNullOrWhiteSpace(tripId))
            {
                MessageBox.Show("Vui long chon mot chuyen xe.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Huy chuyen " + tripId + " va dua cac don dang van chuyen ve cho xu ly?", "Xac nhan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            bool cancelled = DependencyContainer.GetDispatchService().CancelTrip(tripId);
            if (!cancelled)
            {
                MessageBox.Show("Khong the huy chuyen xe nay.", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDispatchData();
                return;
            }

            MessageBox.Show("Da huy chuyen xe " + tripId + ".", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDispatchData();
        }

        private string GetSelectedTripId()
        {
            if (dgvTrips.SelectedRows.Count == 0)
            {
                return string.Empty;
            }

            return System.Convert.ToString(dgvTrips.SelectedRows[0].Cells["TripID"].Value) ?? string.Empty;
        }

        private void LoadDispatchData()
        {
            List<Vehicle> vehicles = DependencyContainer.GetDispatchService().GetAvailableVehicles();
            cbVehicle.Items.Clear();
            foreach (Vehicle vehicle in vehicles)
            {
                cbVehicle.Items.Add(vehicle.VehicleID);
            }
            if (cbVehicle.Items.Count > 0) cbVehicle.SelectedIndex = 0;

            List<Driver> drivers = DependencyContainer.GetDeliveryService().GetAvailableDrivers();
            cbDriver.Items.Clear();
            foreach (Driver driver in drivers)
            {
                cbDriver.Items.Add(driver.StaffID);
            }
            if (cbDriver.Items.Count > 0) cbDriver.SelectedIndex = 0;

            List<Order> allOrders = DependencyContainer.GetOrderService().GetAllOrders();
            List<Order> orders = new List<Order>();
            foreach (Order item in allOrders)
            {
                if (IsDispatchableStatus(item.CurrentStatus))
                {
                    orders.Add(item);
                }
            }

            List<DispatchOrderDTO> rows = new List<DispatchOrderDTO>();
            foreach (Order order in orders)
            {
                rows.Add(new DispatchOrderDTO
                {
                    TrackingNumber = order.TrackingNumber,
                    SenderID = order.SenderID,
                    ReceiverID = order.ReceiverID,
                    TotalWeight = order.TotalWeight,
                    Status = order.CurrentStatus.ToString(),
                    AssignedDriverID = order.AssignedDriverID,
                    AssignedVehicleID = order.AssignedVehicleID
                });
            }

            dgvDispatch.DataSource = rows;
            dgvTrips.DataSource = DependencyContainer.GetDispatchService().GetActiveTrips();
            UpdateDispatchMap();
        }

        private static bool IsDispatchableStatus(OrderStatus status)
        {
            return status == OrderStatus.Pending ||
                   status == OrderStatus.PickedUp ||
                   status == OrderStatus.ArrivedAtWarehouse ||
                   status == OrderStatus.Sorting ||
                   status == OrderStatus.ReadyForDispatch;
        }

        private void DgvDispatch_SelectionChanged(object? sender, System.EventArgs e)
        {
            UpdateDispatchMap();
        }

        private void UpdateDispatchMap()
        {
            if (DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            if (dgvDispatch.SelectedRows.Count == 0)
            {
                routeMap.ClearRoute("Chon don hang de xem so do tuyen");
                return;
            }

            string orderId = System.Convert.ToString(dgvDispatch.SelectedRows[0].Cells["TrackingNumber"].Value) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(orderId))
            {
                routeMap.ClearRoute("Chon don hang de xem so do tuyen");
                return;
            }

            Order order = DependencyContainer.GetOrderService().GetOrderById(orderId);
            if (order == null)
            {
                routeMap.ClearRoute("Khong tim thay don hang");
                return;
            }

            routeMap.SetRoute(RouteMapBuilder.Build(order), RouteMapBuilder.BuildStatusText(order), RouteMapBuilder.BuildProgressText(order));
        }

        private void BtnSuggest_Click(object? sender, System.EventArgs e)
        {
            if (dgvDispatch.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để gợi ý điều phối tối ưu.", "Gợi ý tối ưu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string orderId = System.Convert.ToString(dgvDispatch.SelectedRows[0].Cells["TrackingNumber"].Value) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(orderId))
            {
                return;
            }

            Order order = DependencyContainer.GetOrderService().GetOrderById(orderId);
            if (order == null)
            {
                MessageBox.Show("Không tìm thấy thông tin đơn hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string suggestion = DependencyContainer.GetRouteOptimizationService().SuggestDriverAndVehicle(order);
            MessageBox.Show(suggestion, "Gợi ý điều phối tối ưu ✨", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (suggestion.Contains("De xuat dieu phoi"))
            {
                // Tự động tìm xe trong combobox
                for (int i = 0; i < cbVehicle.Items.Count; i++)
                {
                    string? vId = cbVehicle.Items[i]?.ToString();
                    if (vId != null && suggestion.Contains(vId))
                    {
                        cbVehicle.SelectedIndex = i;
                        break;
                    }
                }

                // Tự động tìm tài xế trong combobox
                for (int i = 0; i < cbDriver.Items.Count; i++)
                {
                    string? dId = cbDriver.Items[i]?.ToString();
                    if (dId != null && suggestion.Contains(dId))
                    {
                        cbDriver.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}
