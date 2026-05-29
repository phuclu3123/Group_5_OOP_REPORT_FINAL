using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Account;
using Logistics.Core.Utilities;
using Logistics.Core.Services.Interfaces;
using Logistics.WinFormsUI.Extensions;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucOrderManagement : UserControl
    {
        private IOrderService? _orderService;

        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Button btnCreateOrder = null!;
        private Panel pnlSearch = null!;
        private TextBox txtSearch = null!;
        private ComboBox cbStatusFilter = null!;
        private ComboBox cbCustomerScope = null!;
        private DataGridView dgvOrders = null!;
        private Button btnExportCsv = null!;
        private AppPersonalizationSettings _personalization = new AppPersonalizationSettings();

        public ucOrderManagement()
        {
            InitializeComponent();
            if (DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            _orderService = DependencyContainer.GetOrderService();
            _personalization = AppPersonalizationSettings.Load();
            UIStyleHelper.ApplyGridViewStyle(dgvOrders);
            dgvOrders.ReadOnly = false;
            dgvOrders.CellPainting += dgvOrders_CellPainting;
            btnCreateOrder.SetRoundedBorder(10);
            SetupCustomerScopeFilter();
            InitializeExportButton();
            LoadOrderData();
        }

        private void InitializeExportButton()
        {
            btnExportCsv = new Button();
            btnExportCsv.Location = new Point(560, 14);
            btnExportCsv.Size = new Size(130, 28);
            btnExportCsv.Text = "Xuất CSV 📄";
            btnExportCsv.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExportCsv.BackColor = Color.FromArgb(52, 152, 219);
            btnExportCsv.ForeColor = Color.White;
            btnExportCsv.FlatStyle = FlatStyle.Flat;
            btnExportCsv.FlatAppearance.BorderSize = 0;
            btnExportCsv.Cursor = Cursors.Hand;
            btnExportCsv.Tag = "KeepStyle"; // Prevent overriding its specific visual color

            btnExportCsv.Click += (sender, e) =>
            {
                CsvExporter.ExportGrid(dgvOrders, "DanhSachDonHang.csv");
            };

            pnlSearch.Controls.Add(btnExportCsv);
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            if (_orderService == null)
            {
                return;
            }

            using (Forms.FrmOrderEditor editor = new Forms.FrmOrderEditor())
            {
                if (IsCustomer())
                {
                    List<string> customerIds = ResolveCurrentCustomerIds(SessionManager.CurrentUser);
                    if (customerIds.Count > 0)
                    {
                        editor.SetSenderId(customerIds[0], true);
                    }
                }

                if (editor.ShowDialog() == DialogResult.OK)
                {
                    Order order = _orderService.CreateOrder(
                        editor.SenderId,
                        editor.ReceiverId,
                        editor.PickupAddress,
                        editor.DeliveryAddress,
                        editor.SelectedServiceType);
                    Package package = _orderService.CreatePackage(
                        order.TrackingNumber,
                        editor.PackageDescription,
                        editor.PackageWeight,
                        editor.Dimensions,
                        editor.IsFragile,
                        editor.PackageValue,
                        editor.PackageCategory,
                        editor.IsFragile ? "Hàng dễ vỡ, cần xử lý nhẹ" : string.Empty);
                    _orderService.AddPackageToOrder(order.TrackingNumber, package);
                    _orderService.CalculateOrderCost(order.TrackingNumber, editor.CostPerKg);
                    LoadOrderData();
                }
            }
        }

        private void FilterChanged(object? sender, EventArgs e)
        {
            LoadOrderData();
        }

        private void LoadOrderData()
        {
            if (_orderService == null || DesignerHelper.IsInDesignMode(this))
            {
                return;
            }

            string searchText = txtSearch.Text.ToLower();
            string statusFilter = cbStatusFilter.SelectedItem?.ToString() ?? "Tất cả trạng thái";
            List<Order> allOrders = GetOrdersForCurrentScope();
            List<OrderGridRowDTO> filtered = new List<OrderGridRowDTO>();

            foreach (Order order in allOrders)
            {
                bool matchesSearch = string.IsNullOrEmpty(searchText) || order.TrackingNumber.ToLower().Contains(searchText);
                bool matchesStatus = statusFilter == "Tất cả trạng thái" ||
                                     EnumTranslator.TranslateOrderStatus(order.CurrentStatus) == statusFilter ||
                                     order.CurrentStatus.ToString() == statusFilter;
                if (matchesSearch && matchesStatus)
                {
                    filtered.Add(new OrderGridRowDTO
                    {
                        TrackingNumber = order.TrackingNumber,
                        SenderId = order.SenderID,
                        ReceiverId = order.ReceiverID,
                        TotalWeight = order.TotalWeight,
                        TotalCost = order.TotalCost,
                        ServiceType = TranslateServiceType(order.ServiceType),
                        Status = EnumTranslator.TranslateOrderStatus(order.CurrentStatus),
                        StatusCode = order.CurrentStatus.ToString(),
                        AssignedDriver = string.IsNullOrWhiteSpace(order.AssignedDriverID) ? "Chưa gán" : order.AssignedDriverID,
                        AssignedVehicle = string.IsNullOrWhiteSpace(order.AssignedVehicleID) ? "Chưa gán" : order.AssignedVehicleID,
                        CreatedAt = order.CreatedDate.ToString("dd/MM/yyyy HH:mm")
                    });
                }
            }

            dgvOrders.DataSource = filtered;
            ConfigureGridColumns();
            ApplyPersonalizedGridStyle();
        }

        private void SetupCustomerScopeFilter()
        {
            cbCustomerScope = new ComboBox();
            cbCustomerScope.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCustomerScope.Font = cbStatusFilter.Font;
            cbCustomerScope.Items.Add("Đơn gửi");
            cbCustomerScope.Items.Add("Đơn nhận");
            cbCustomerScope.Items.Add("Tất cả đơn liên quan");
            cbCustomerScope.SelectedIndex = 0;
            cbCustomerScope.Location = new Point(cbStatusFilter.Right + 16, cbStatusFilter.Top);
            cbCustomerScope.Size = new Size(190, cbStatusFilter.Height);
            cbCustomerScope.Visible = IsCustomer();
            cbCustomerScope.SelectedIndexChanged += FilterChanged;
            pnlSearch.Controls.Add(cbCustomerScope);
        }

        private List<Order> GetOrdersForCurrentScope()
        {
            if (!IsCustomer() || cbCustomerScope == null)
            {
                return _orderService!.GetAllOrders();
            }

            string scope = cbCustomerScope.SelectedItem?.ToString() ?? "Đơn gửi";
            if (scope == "Đơn nhận")
            {
                return _orderService!.GetReceivedOrdersForCurrentCustomer();
            }

            if (scope == "Tất cả đơn liên quan")
            {
                return _orderService!.GetAllOrders();
            }

            return _orderService!.GetSentOrdersForCurrentCustomer();
        }

        private void ConfigureGridColumns()
        {
            if (dgvOrders.Columns["TrackingNumber"] != null) dgvOrders.Columns["TrackingNumber"]!.HeaderText = "Mã vận đơn";
            if (dgvOrders.Columns["SenderId"] != null) dgvOrders.Columns["SenderId"]!.HeaderText = "Người gửi";
            if (dgvOrders.Columns["ReceiverId"] != null) dgvOrders.Columns["ReceiverId"]!.HeaderText = "Người nhận";
            if (dgvOrders.Columns["TotalWeight"] != null) dgvOrders.Columns["TotalWeight"]!.HeaderText = "Khối lượng (kg)";
            if (dgvOrders.Columns["TotalCost"] != null)
            {
                dgvOrders.Columns["TotalCost"]!.HeaderText = "Cước phí";
                dgvOrders.Columns["TotalCost"]!.DefaultCellStyle.Format = "N0";
            }
            if (dgvOrders.Columns["ServiceType"] != null) dgvOrders.Columns["ServiceType"]!.HeaderText = "Dịch vụ";
            if (dgvOrders.Columns["Status"] != null) dgvOrders.Columns["Status"]!.HeaderText = "Trạng thái";
            if (dgvOrders.Columns["StatusCode"] != null) dgvOrders.Columns["StatusCode"]!.Visible = false;
            if (dgvOrders.Columns["AssignedDriver"] != null) dgvOrders.Columns["AssignedDriver"]!.HeaderText = "Tài xế";
            if (dgvOrders.Columns["AssignedVehicle"] != null) dgvOrders.Columns["AssignedVehicle"]!.HeaderText = "Xe";
            if (dgvOrders.Columns["CreatedAt"] != null) dgvOrders.Columns["CreatedAt"]!.HeaderText = "Ngày tạo";
            if (dgvOrders.Columns["Status"] != null)
            {
                dgvOrders.Columns["Status"]!.FillWeight = 90;
            }

            foreach (DataGridViewColumn column in dgvOrders.Columns)
            {
                column.ReadOnly = true;
            }

            if (!dgvOrders.Columns.Contains("btnDetails"))
            {
                DataGridViewButtonColumn btnDetails = new DataGridViewButtonColumn();
                btnDetails.Name = "btnDetails";
                btnDetails.HeaderText = "";
                btnDetails.Text = "Chi tiết";
                btnDetails.UseColumnTextForButtonValue = true;
                btnDetails.FlatStyle = FlatStyle.Flat;
                dgvOrders.Columns.Add(btnDetails);

                DataGridViewButtonColumn btnPackages = new DataGridViewButtonColumn();
                btnPackages.Name = "btnPackages";
                btnPackages.HeaderText = "";
                btnPackages.Text = "Kiện hàng";
                btnPackages.UseColumnTextForButtonValue = true;
                btnPackages.FlatStyle = FlatStyle.Flat;
                dgvOrders.Columns.Add(btnPackages);

                DataGridViewButtonColumn btnInvoice = new DataGridViewButtonColumn();
                btnInvoice.Name = "btnInvoice";
                btnInvoice.HeaderText = "";
                btnInvoice.Text = "Hóa đơn";
                btnInvoice.UseColumnTextForButtonValue = true;
                btnInvoice.FlatStyle = FlatStyle.Flat;
                dgvOrders.Columns.Add(btnInvoice);

                DataGridViewButtonColumn btnPayment = new DataGridViewButtonColumn();
                btnPayment.Name = "btnPayment";
                btnPayment.HeaderText = "";
                btnPayment.Text = "Thu tiền";
                btnPayment.UseColumnTextForButtonValue = true;
                btnPayment.FlatStyle = FlatStyle.Flat;
                dgvOrders.Columns.Add(btnPayment);

                DataGridViewButtonColumn btnIssue = new DataGridViewButtonColumn();
                btnIssue.Name = "btnIssue";
                btnIssue.HeaderText = "";
                btnIssue.Text = "Sự cố";
                btnIssue.UseColumnTextForButtonValue = true;
                btnIssue.FlatStyle = FlatStyle.Flat;
                dgvOrders.Columns.Add(btnIssue);

                DataGridViewButtonColumn btnChangeStatus = new DataGridViewButtonColumn();
                btnChangeStatus.Name = "btnChangeStatus";
                btnChangeStatus.HeaderText = "";
                btnChangeStatus.Text = "Sửa TT";
                btnChangeStatus.UseColumnTextForButtonValue = true;
                btnChangeStatus.FlatStyle = FlatStyle.Flat;
                dgvOrders.Columns.Add(btnChangeStatus);

                DataGridViewButtonColumn btnComplete = new DataGridViewButtonColumn();
                btnComplete.Name = "btnComplete";
                btnComplete.HeaderText = "";
                btnComplete.Text = "Đã giao";
                btnComplete.UseColumnTextForButtonValue = true;
                btnComplete.FlatStyle = FlatStyle.Flat;
                dgvOrders.Columns.Add(btnComplete);

                DataGridViewButtonColumn btnCancel = new DataGridViewButtonColumn();
                btnCancel.Name = "btnCancel";
                btnCancel.HeaderText = "";
                btnCancel.Text = "Hủy";
                btnCancel.UseColumnTextForButtonValue = true;
                btnCancel.FlatStyle = FlatStyle.Flat;
                dgvOrders.Columns.Add(btnCancel);
            }

            SetActionColumnWidth("btnDetails", 78);
            SetActionColumnWidth("btnPackages", 86);
            SetActionColumnWidth("btnInvoice", 80);
            SetActionColumnWidth("btnPayment", 80);
            SetActionColumnWidth("btnIssue", 70);
            SetActionColumnWidth("btnChangeStatus", 76);
            SetActionColumnWidth("btnComplete", 74);
            SetActionColumnWidth("btnCancel", 58);
            ApplyRoleActions();
        }

        private void ApplyRoleActions()
        {
            UserRole role = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.Role : UserRole.Customer;
            bool isAdminOrDispatcher = role == UserRole.Admin || role == UserRole.Dispatcher;
            bool isCustomer = role == UserRole.Customer;
            bool isDriver = role == UserRole.Driver;

            btnCreateOrder.Visible = isAdminOrDispatcher || isCustomer;
            SetColumnVisible("btnPayment", isAdminOrDispatcher);
            SetColumnVisible("btnChangeStatus", isAdminOrDispatcher);
            SetColumnVisible("btnComplete", isDriver || isAdminOrDispatcher);
            SetColumnVisible("btnCancel", isAdminOrDispatcher || (isCustomer && !IsReceivedScopeSelected()));
            SetColumnVisible("btnIssue", isDriver || isAdminOrDispatcher);
            SetColumnVisible("btnPackages", !isDriver);
        }

        private void SetColumnVisible(string columnName, bool visible)
        {
            if (dgvOrders.Columns[columnName] != null)
            {
                dgvOrders.Columns[columnName]!.Visible = visible;
            }
        }

        private void SetActionColumnWidth(string columnName, int width)
        {
            if (dgvOrders.Columns[columnName] == null)
            {
                return;
            }

            dgvOrders.Columns[columnName]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOrders.Columns[columnName]!.Width = width;
        }

        private void ApplyPersonalizedGridStyle()
        {
            int rowHeight = _personalization.OrderGridDensity == "Compact" ? 30 : 42;
            dgvOrders.RowTemplate.Height = rowHeight;
            dgvOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvOrders.DefaultCellStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);

            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                row.Height = rowHeight;
                string status = row.Cells["Status"].Value?.ToString() ?? string.Empty;
                string statusCode = row.Cells["StatusCode"].Value?.ToString() ?? status;
                Color statusColor = GetStatusColor(statusCode);

                row.DefaultCellStyle.BackColor = Color.White;
                row.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);

                if (_personalization.HighlightProblemOrders && IsProblemStatus(statusCode))
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 242);
                }

                row.Cells["Status"].Style.BackColor = statusColor;
                row.Cells["Status"].Style.SelectionBackColor = statusColor;
                row.Cells["Status"].Style.ForeColor = Color.White;
                row.Cells["Status"].Style.SelectionForeColor = Color.White;
                row.Cells["Status"].Style.Font = new Font(dgvOrders.Font, FontStyle.Bold);
            }
        }

        private void dgvOrders_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Graphics == null || dgvOrders.Columns[e.ColumnIndex].Name != "Status")
            {
                return;
            }

            string status = dgvOrders.Rows[e.RowIndex].Cells["Status"].Value?.ToString() ?? string.Empty;
            string statusCode = dgvOrders.Rows[e.RowIndex].Cells["StatusCode"].Value?.ToString() ?? status;
            Color statusColor = GetStatusColor(statusCode);
            Rectangle bounds = e.CellBounds;
            bounds.Inflate(-6, -8);

            e.PaintBackground(e.CellBounds, true);
            using (SolidBrush brush = new SolidBrush(statusColor))
            {
                e.Graphics.FillRectangle(brush, bounds);
            }

            TextRenderer.DrawText(
                e.Graphics,
                status,
                dgvOrders.Font,
                bounds,
                Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            e.Handled = true;
        }

        private Color GetStatusColor(string status)
        {
            if (status == "Failed" || status == "Cancelled" || status.Contains("thất") || status.Contains("hủy"))
            {
                return _personalization.HighContrastStatusColors
                    ? Color.FromArgb(198, 40, 40)
                    : Color.FromArgb(229, 57, 53);
            }

            if (status == "Delivered")
            {
                return Color.FromArgb(46, 125, 50);
            }

            if (status == "InTransit")
            {
                return Color.FromArgb(21, 101, 192);
            }

            return Color.FromArgb(245, 124, 0);
        }

        private static bool IsProblemStatus(string status)
        {
            return status == "Failed" ||
                   status == "Cancelled" ||
                   status.Contains("thất") ||
                   status.Contains("Sự cố");
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_orderService == null || e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string id = dgvOrders.Rows[e.RowIndex].Cells["TrackingNumber"].Value?.ToString() ?? string.Empty;
            if (dgvOrders.Columns[e.ColumnIndex].Name == "btnDetails")
            {
                using (Forms.FrmTracking tracking = new Forms.FrmTracking(id))
                {
                    tracking.ShowDialog();
                }
            }
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "btnPackages")
            {
                if (IsDriver()) return;
                using (Forms.FrmOrderPackages packages = new Forms.FrmOrderPackages(id))
                {
                    packages.ShowDialog();
                }
                LoadOrderData();
            }
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "btnInvoice")
            {
                using (Forms.FrmInvoice invoice = new Forms.FrmInvoice(id))
                {
                    invoice.ShowDialog();
                }
            }
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "btnPayment")
            {
                if (!IsAdminOrDispatcher()) return;
                Order order = _orderService.GetOrderById(id);
                decimal suggestedAmount = order != null ? order.TotalCost : 0;
                if (suggestedAmount <= 0)
                {
                    suggestedAmount = _orderService.CalculateOrderCost(id, 15000m);
                }

                using (Forms.FrmPayment payment = new Forms.FrmPayment(id, suggestedAmount))
                {
                    payment.ShowDialog();
                }
                LoadOrderData();
            }
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "btnIssue")
            {
                if (!IsAdminOrDispatcher() && !IsDriver()) return;
                using (Forms.FrmProblemReport report = new Forms.FrmProblemReport(id))
                {
                    report.ShowDialog();
                }
                LoadOrderData();
            }
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "btnChangeStatus")
            {
                if (!IsAdminOrDispatcher()) return;
                ShowStatusEditor(id);
            }
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "btnComplete")
            {
                if (!IsAdminOrDispatcher() && !IsDriver()) return;
                if (_personalization.ConfirmBeforeDeliveryComplete)
                {
                    DialogResult confirm = MessageBox.Show("Xác nhận đánh dấu đơn hàng " + id + " là đã giao?", "Xác nhận giao hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes)
                    {
                        return;
                    }
                }

                bool completed = DependencyContainer.GetDeliveryService().CompleteDelivery(id);
                if (!completed)
                {
                    MessageBox.Show("Không thể hoàn tất giao hàng.", "Giao hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                LoadOrderData();
            }
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "btnCancel")
            {
                if (!IsAdminOrDispatcher() && !IsCustomer()) return;
                DialogResult result = MessageBox.Show("Xác nhận hủy đơn hàng " + id + "?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    bool cancelled = _orderService.CancelOrder(id);
                    if (!cancelled)
                    {
                        MessageBox.Show("Không thể hủy đơn hàng này.", "Hủy đơn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    LoadOrderData();
                }
            }
        }

        private bool IsReceivedScopeSelected()
        {
            return cbCustomerScope != null &&
                   cbCustomerScope.Visible &&
                   cbCustomerScope.SelectedItem?.ToString() == "Đơn nhận";
        }

        private static bool IsAdminOrDispatcher()
        {
            return SessionManager.CurrentUser != null &&
                   (SessionManager.CurrentUser.Role == UserRole.Admin ||
                    SessionManager.CurrentUser.Role == UserRole.Dispatcher);
        }

        private static bool IsCustomer()
        {
            return SessionManager.CurrentUser != null && SessionManager.CurrentUser.Role == UserRole.Customer;
        }

        private static bool IsDriver()
        {
            return SessionManager.CurrentUser != null && SessionManager.CurrentUser.Role == UserRole.Driver;
        }

        private List<string> ResolveCurrentCustomerIds(User? user)
        {
            List<string> ids = new List<string>();
            if (user == null) return ids;

            if (user.Person is Customer customer)
            {
                ids.Add(customer.Id);
            }

            var customerRepo = DependencyContainer.GetCustomerRepository();
            if (customerRepo != null)
            {
                var customers = customerRepo.GetAll();
                for (int i = 0; i < customers.Count; i++)
                {
                    if (customers[i].AccountID == user.Username || customers[i].AccountID == user.UserId)
                    {
                        ids.Add(customers[i].Id);
                    }
                }
            }

            if (ids.Count == 0 && user.Username.StartsWith("customer", StringComparison.OrdinalIgnoreCase))
            {
                ids.Add("CUST001");
            }

            return ids;
        }

        private void ShowStatusEditor(string trackingNumber)
        {
            Order order = _orderService!.GetOrderById(trackingNumber);
            if (order == null)
            {
                return;
            }

            using (Forms.FrmOrderStatusEditor editor = new Forms.FrmOrderStatusEditor(trackingNumber, order.CurrentStatus))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    if (_orderService.UpdateOrderStatus(trackingNumber, editor.SelectedStatus))
                    {
                        LoadOrderData();
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật trạng thái đơn hàng.", "Trạng thái", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private static string TranslateServiceType(Logistics.Core.Models.Common.ServiceType serviceType)
        {
            switch (serviceType)
            {
                case Logistics.Core.Models.Common.ServiceType.Standard:
                    return "Tiêu chuẩn";
                case Logistics.Core.Models.Common.ServiceType.Express:
                    return "Nhanh";
                case Logistics.Core.Models.Common.ServiceType.Instant:
                    return "Hỏa tốc";
                default:
                    return serviceType.ToString();
            }
        }

    }
}
