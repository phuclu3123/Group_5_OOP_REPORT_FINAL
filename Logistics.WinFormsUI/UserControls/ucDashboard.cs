using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Account;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;
using Logistics.WinFormsUI.Utilities;

namespace Logistics.WinFormsUI.UserControls
{
    public partial class ucDashboard : UserControl
    {
        private readonly IReportService? _reportService;
        private readonly IOrderService? _orderService;
        private readonly Dictionary<string, int> _statusCounts = new Dictionary<string, int>();
        private Panel pnlChart = null!;
        private DataGridView dgvRecentOrders = null!;
        private Label lblRevenue = null!;
        private Label lblPending = null!;
        private Label lblDelivered = null!;
        private Label lblFleet = null!;
        private ucVisualDashboardChart visualChart = null!;

        public ucDashboard()
        {
            InitializeComponent();
            if (DesignerHelper.IsInDesignMode(this))
            {
                return;
            }
        }

        public ucDashboard(IReportService reportService, IOrderService orderService) : this()
        {
            _reportService = reportService;
            _orderService = orderService;
            BuildDashboard();
            LoadRealData();
        }

        private void BuildDashboard()
        {
            Controls.Clear();
            BackColor = Color.WhiteSmoke;
            Padding = new Padding(24);

            UserRole role = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.Role : UserRole.Customer;

            string titleText = "TỔNG QUAN VẬN HÀNH";
            string subtitleText = "Theo dõi đơn hàng, doanh thu, đội xe và việc cần xử lý trong ngày";

            if (role == UserRole.Driver)
            {
                titleText = "TỔNG QUAN TÀI XẾ";
                subtitleText = "Xem thông tin đơn hàng được phân công, lương thưởng và xe đang vận hành";
            }
            else if (role == UserRole.Customer)
            {
                titleText = "TỔNG QUAN KHÁCH HÀNG";
                subtitleText = "Theo dõi trạng thái đơn hàng gửi/nhận, chi phí và tích lũy điểm thưởng";
            }
            else if (role == UserRole.WarehouseStaff)
            {
                titleText = "TỔNG QUAN NHÂN VIÊN KHO";
                subtitleText = "Theo dõi các kiện hàng cần đóng gói, xuất nhập kho và ca làm việc";
            }

            Label title = new Label
            {
                Text = titleText,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72),
                Location = new Point(24, 22),
                AutoSize = true
            };

            Label subtitle = new Label
            {
                Text = subtitleText,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(95, 105, 120),
                Location = new Point(26, 58),
                AutoSize = true
            };

            FlowLayoutPanel cards = new FlowLayoutPanel
            {
                Location = new Point(24, 96),
                Size = new Size(1120, 126),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                WrapContents = false
            };

            if (role == UserRole.Admin || role == UserRole.Dispatcher)
            {
                lblRevenue = AddMetricCard(cards, "Doanh thu ghi nhận", "0 đ", Color.FromArgb(46, 204, 113));
                lblPending = AddMetricCard(cards, "Đơn chờ xử lý", "0", Color.FromArgb(245, 158, 11));
                lblDelivered = AddMetricCard(cards, "Đã giao", "0", Color.FromArgb(52, 152, 219));
                lblFleet = AddMetricCard(cards, "Xe sẵn sàng", "0/0", Color.FromArgb(108, 92, 231));
            }
            else if (role == UserRole.Driver)
            {
                lblRevenue = AddMetricCard(cards, "Đơn được giao", "0", Color.FromArgb(245, 158, 11));
                lblPending = AddMetricCard(cards, "Chuyến hoàn thành", "0", Color.FromArgb(46, 204, 113));
                lblDelivered = AddMetricCard(cards, "Lương ước tính", "0 đ", Color.FromArgb(52, 152, 219));
                lblFleet = AddMetricCard(cards, "Xe đang lái", "Chưa gán", Color.FromArgb(108, 92, 231));
            }
            else if (role == UserRole.Customer)
            {
                lblRevenue = AddMetricCard(cards, "Đơn đã gửi", "0", Color.FromArgb(52, 152, 219));
                lblPending = AddMetricCard(cards, "Đơn đã nhận", "0", Color.FromArgb(46, 204, 113));
                lblDelivered = AddMetricCard(cards, "Chi tiêu tích lũy", "0 đ", Color.FromArgb(245, 158, 11));
                lblFleet = AddMetricCard(cards, "Điểm tích lũy", "0 điểm", Color.FromArgb(108, 92, 231));
            }
            else if (role == UserRole.WarehouseStaff)
            {
                lblRevenue = AddMetricCard(cards, "Mã kho phụ trách", "WAREHOUSE01", Color.FromArgb(108, 92, 231));
                lblPending = AddMetricCard(cards, "Ca làm việc", "Ca ngày", Color.FromArgb(52, 152, 219));
                lblDelivered = AddMetricCard(cards, "Kiện hàng trong kho", "0", Color.FromArgb(245, 158, 11));
                lblFleet = AddMetricCard(cards, "Đơn chờ xử lý", "0", Color.FromArgb(239, 68, 68));
            }

            bool showChart = (role == UserRole.Admin || role == UserRole.Dispatcher);

            if (showChart)
            {
                pnlChart = new Panel
                {
                    BackColor = Color.White,
                    Location = new Point(24, 246),
                    Size = new Size(560, 310),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left
                };

                Label chartTitle = new Label
                {
                    Text = "Cơ cấu trạng thái đơn hàng",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Location = new Point(18, 14),
                    AutoSize = true
                };
                pnlChart.Controls.Add(chartTitle);

                visualChart = new ucVisualDashboardChart
                {
                    Location = new Point(10, 50),
                    Size = new Size(540, 250),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
                };
                pnlChart.Controls.Add(visualChart);
                Controls.Add(pnlChart);
            }

            Panel actionPanel = new Panel
            {
                BackColor = Color.White,
                Location = showChart ? new Point(610, 246) : new Point(24, 246),
                Size = showChart ? new Size(534, 310) : new Size(1120, 310),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            actionPanel.Controls.Add(new Label
            {
                Text = "Việc cần chú ý",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(18, 14),
                AutoSize = true
            });

            if (role == UserRole.Admin || role == UserRole.Dispatcher)
            {
                actionPanel.Controls.Add(CreateBullet("Điều phối các đơn đang chờ xử lý.", 58));
                actionPanel.Controls.Add(CreateBullet("Kiểm tra xe đang bảo trì hoặc xe hỏng.", 92));
                actionPanel.Controls.Add(CreateBullet("In hóa đơn/phiếu thu từ màn Chứng từ.", 126));
                actionPanel.Controls.Add(CreateBullet("Cập nhật điểm khách hàng sau giao dịch.", 160));
            }
            else if (role == UserRole.Driver)
            {
                actionPanel.Controls.Add(CreateBullet("Luôn luôn tuân thủ luật an toàn giao thông đường bộ.", 58));
                actionPanel.Controls.Add(CreateBullet("Cập nhật trạng thái 'Đã giao' ngay khi khách nhận hàng.", 92));
                actionPanel.Controls.Add(CreateBullet("Báo cáo sự cố (hỏng hóc, tai nạn) kịp thời qua nút 'Sự cố'.", 126));
                actionPanel.Controls.Add(CreateBullet("Kiểm tra kỹ lưỡng nhiên liệu và phanh trước khi khởi hành.", 160));
            }
            else if (role == UserRole.Customer)
            {
                actionPanel.Controls.Add(CreateBullet("Bấm nút 'Tạo đơn hàng' ở thanh menu để gửi kiện hàng mới.", 58));
                actionPanel.Controls.Add(CreateBullet("Theo dõi lộ trình thời gian thực trong danh sách đơn hàng bên dưới.", 92));
                actionPanel.Controls.Add(CreateBullet("Tích lũy điểm thưởng loyalty sau mỗi đơn giao thành công.", 126));
                actionPanel.Controls.Add(CreateBullet("Liên hệ hotline CSKH nếu có bất kỳ thắc mắc nào về cước phí.", 160));
            }
            else if (role == UserRole.WarehouseStaff)
            {
                actionPanel.Controls.Add(CreateBullet("Quét mã vạch chính xác khi xuất/nhập kho hàng.", 58));
                actionPanel.Controls.Add(CreateBullet("Phân loại và bảo quản kỹ các kiện hàng dễ vỡ (Fragile).", 92));
                actionPanel.Controls.Add(CreateBullet("Duy trì nhiệt độ thích hợp cho xe đông lạnh nếu được yêu cần.", 126));
                actionPanel.Controls.Add(CreateBullet("Kiểm kê định kỳ số lượng hàng hóa vào cuối mỗi ca làm việc.", 160));
            }

            Label recentTitle = new Label
            {
                Text = role == UserRole.WarehouseStaff ? "Kiện hàng trong kho cần lưu ý" : "Đơn hàng gần đây",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(24, 582),
                AutoSize = true
            };

            dgvRecentOrders = new DataGridView
            {
                Location = new Point(24, 620),
                Size = new Size(1120, 210),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            UIStyleHelper.ApplyGridViewStyle(dgvRecentOrders);

            Controls.Add(title);
            Controls.Add(subtitle);
            Controls.Add(cards);
            Controls.Add(actionPanel);
            Controls.Add(recentTitle);
            Controls.Add(dgvRecentOrders);
        }

        private static Label AddMetricCard(FlowLayoutPanel parent, string title, string value, Color accent)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                Size = new Size(260, 105),
                Margin = new Padding(0, 0, 18, 0)
            };

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(95, 105, 120),
                Location = new Point(16, 16),
                AutoSize = true
            };

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                ForeColor = accent,
                Location = new Point(14, 44),
                AutoSize = true
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            parent.Controls.Add(card);
            return lblValue;
        }

        private static Label CreateBullet(string text, int y)
        {
            return new Label
            {
                Text = "• " + text,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(22, y),
                Size = new Size(950, 24)
            };
        }

        private void LoadRealData()
        {
            if (_reportService == null || _orderService == null || SessionManager.CurrentUser == null)
            {
                return;
            }

            UserRole role = SessionManager.CurrentUser.Role;
            List<Order> allOrders = _orderService.GetAllOrders();

            if (role == UserRole.Admin || role == UserRole.Dispatcher)
            {
                DashboardStatisticsDTO stats = _reportService.GetOverallStatistics();
                List<Vehicle> vehicles = DependencyContainer.GetVehicleService().GetAllVehicles();

                decimal revenue = 0;
                int pending = 0;
                int delivered = 0;
                _statusCounts.Clear();

                foreach (Order order in allOrders)
                {
                    revenue += order.TotalCost;
                    string status = EnumTranslator.TranslateOrderStatus(order.CurrentStatus);
                    if (!_statusCounts.ContainsKey(status))
                    {
                        _statusCounts[status] = 0;
                    }
                    _statusCounts[status]++;

                    if (order.CurrentStatus == OrderStatus.Pending)
                    {
                        pending++;
                    }
                    else if (order.CurrentStatus == OrderStatus.Delivered)
                    {
                        delivered++;
                    }
                }

                int readyVehicles = 0;
                foreach (Vehicle vehicle in vehicles)
                {
                    if (vehicle.IsAvailable())
                    {
                        readyVehicles++;
                    }
                }

                lblRevenue.Text = revenue.ToString("N0") + " đ";
                lblPending.Text = pending.ToString("N0");
                lblDelivered.Text = delivered.ToString("N0");
                lblFleet.Text = readyVehicles.ToString("N0") + "/" + stats.TotalVehicles.ToString("N0");

                LoadRecentOrders(allOrders);
                visualChart.SetData(_statusCounts);
            }
            else if (role == UserRole.Driver)
            {
                Driver? driver = null;
                var driverRepo = DependencyContainer.GetDriverRepository();
                if (driverRepo != null)
                {
                    var drivers = driverRepo.GetAll();
                    for (int i = 0; i < drivers.Count; i++)
                    {
                        if (drivers[i].AccountID == SessionManager.CurrentUser.Username || 
                            drivers[i].AccountID == SessionManager.CurrentUser.UserId)
                        {
                            driver = drivers[i];
                            break;
                        }
                    }
                }

                List<Order> driverOrders = new List<Order>();
                int completedTrips = 0;

                string driverId = driver != null ? driver.StaffID : string.Empty;
                if (!string.IsNullOrEmpty(driverId))
                {
                    foreach (Order order in allOrders)
                    {
                        if (order.AssignedDriverID == driverId)
                        {
                            driverOrders.Add(order);
                            if (order.CurrentStatus == OrderStatus.Delivered)
                            {
                                completedTrips++;
                            }
                        }
                    }
                }

                lblRevenue.Text = driverOrders.Count.ToString();
                lblPending.Text = completedTrips.ToString();
                lblDelivered.Text = (driver != null ? driver.CalculateSalary() : 15000000m).ToString("N0") + " đ";
                lblFleet.Text = (driver != null && driver.AssignedVehicles != null && driver.AssignedVehicles.Count > 0)
                    ? driver.AssignedVehicles[0].VehicleID
                    : "Chưa gán";

                LoadRecentOrders(driverOrders);
            }
            else if (role == UserRole.Customer)
            {
                Customer? customer = null;
                var customerRepo = DependencyContainer.GetCustomerRepository();
                if (customerRepo != null)
                {
                    var customers = customerRepo.GetAll();
                    for (int i = 0; i < customers.Count; i++)
                    {
                        if (customers[i].AccountID == SessionManager.CurrentUser.Username || 
                            customers[i].AccountID == SessionManager.CurrentUser.UserId)
                        {
                            customer = customers[i];
                            break;
                        }
                    }
                }

                string customerId = customer != null ? customer.Id : "CUST001";
                List<Order> sentOrders = new List<Order>();
                List<Order> receivedOrders = new List<Order>();
                List<Order> relatedOrders = new List<Order>();
                decimal totalSpent = 0;

                foreach (Order order in allOrders)
                {
                    bool isSent = order.SenderID == customerId;
                    bool isReceived = order.ReceiverID == customerId;

                    if (isSent)
                    {
                        sentOrders.Add(order);
                        relatedOrders.Add(order);
                        if (order.CurrentStatus != OrderStatus.Cancelled)
                        {
                            totalSpent += order.TotalCost;
                        }
                    }
                    else if (isReceived)
                    {
                        receivedOrders.Add(order);
                        relatedOrders.Add(order);
                    }
                }

                lblRevenue.Text = sentOrders.Count.ToString();
                lblPending.Text = receivedOrders.Count.ToString();
                lblDelivered.Text = totalSpent.ToString("N0") + " đ";
                lblFleet.Text = (customer != null ? customer.LoyaltyPoints : 0).ToString("N0") + " điểm";

                LoadRecentOrders(relatedOrders);
            }
            else if (role == UserRole.WarehouseStaff)
            {
                WarehouseStaff? staff = null;
                var staffRepo = DependencyContainer.GetWarehouseStaffRepository();
                if (staffRepo != null)
                {
                    var staffs = staffRepo.GetAll();
                    for (int i = 0; i < staffs.Count; i++)
                    {
                        if (staffs[i].AccountID == SessionManager.CurrentUser.Username || 
                            staffs[i].AccountID == SessionManager.CurrentUser.UserId)
                        {
                            staff = staffs[i];
                            break;
                        }
                    }
                }

                string warehouseId = staff != null ? staff.WarehouseID : "WAREHOUSE01";
                string shift = staff != null ? staff.Shift : "Ca ngày";

                int packageCount = 0;
                int pendingCount = 0;
                List<Order> whOrders = new List<Order>();

                foreach (Order order in allOrders)
                {
                    if (order.Route.PickupAddress.Contains(warehouseId) || order.Route.DeliveryAddress.Contains(warehouseId) || order.TrackingNumber.Contains("WH"))
                    {
                        packageCount += order.Packages?.Count ?? 0;
                        if (order.CurrentStatus == OrderStatus.Pending || order.CurrentStatus == OrderStatus.Sorting)
                        {
                            pendingCount++;
                        }
                        whOrders.Add(order);
                    }
                }

                if (whOrders.Count == 0)
                {
                    packageCount = 18;
                    pendingCount = 4;
                    for (int i = 0; i < Math.Min(5, allOrders.Count); i++)
                    {
                        whOrders.Add(allOrders[i]);
                    }
                }

                lblRevenue.Text = warehouseId;
                lblPending.Text = shift;
                lblDelivered.Text = packageCount.ToString();
                lblFleet.Text = pendingCount.ToString();

                LoadRecentOrders(whOrders);
            }
        }

        private void LoadRecentOrders(List<Order> orders)
        {
            List<DashboardOrderRow> rows = new List<DashboardOrderRow>();
            int count = 0;
            for (int i = orders.Count - 1; i >= 0 && count < 8; i--)
            {
                Order order = orders[i];
                rows.Add(new DashboardOrderRow
                {
                    TrackingNumber = order.TrackingNumber,
                    Route = order.Route != null ? order.Route.ToString() : string.Empty,
                    Weight = order.TotalWeight,
                    Cost = order.TotalCost,
                    Status = EnumTranslator.TranslateOrderStatus(order.CurrentStatus),
                    CreatedAt = order.CreatedDate.ToString("dd/MM/yyyy HH:mm")
                });
                count++;
            }

            dgvRecentOrders.DataSource = rows;
            SetRecentColumn("TrackingNumber", "Mã vận đơn");
            SetRecentColumn("Route", "Tuyến");
            SetRecentColumn("Weight", "Khối lượng");
            SetRecentColumn("Cost", "Cước phí");
            SetRecentColumn("Status", "Trạng thái");
            SetRecentColumn("CreatedAt", "Ngày tạo");
        }

        private void SetRecentColumn(string name, string text)
        {
            if (dgvRecentOrders.Columns[name] != null)
            {
                dgvRecentOrders.Columns[name]!.HeaderText = text;
            }
        }

        private void PnlChart_Paint(object? sender, PaintEventArgs e)
        {
            Panel? chartSurface = sender as Panel ?? pnlChart;
            if (chartSurface == null)
            {
                return;
            }

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle area = new Rectangle(32, 70, chartSurface.Width - 64, chartSurface.Height - 110);
            if (area.Width <= 0 || area.Height <= 0)
            {
                return;
            }

            int max = 1;
            foreach (int count in _statusCounts.Values)
            {
                if (count > max)
                {
                    max = count;
                }
            }

            if (_statusCounts.Count == 0)
            {
                g.DrawString("Chưa có dữ liệu đơn hàng.", new Font("Segoe UI", 10F), Brushes.DimGray, 32, 120);
                return;
            }

            int index = 0;
            int barWidth = Math.Max(42, area.Width / Math.Max(1, _statusCounts.Count) - 24);
            Color[] colors =
            {
                Color.FromArgb(245, 158, 11),
                Color.FromArgb(52, 152, 219),
                Color.FromArgb(46, 204, 113),
                Color.FromArgb(239, 68, 68),
                Color.FromArgb(108, 92, 231)
            };

            foreach (KeyValuePair<string, int> item in _statusCounts)
            {
                int x = area.Left + index * (barWidth + 24);
                int barHeight = (int)(area.Height * (item.Value / (double)max));
                int y = area.Bottom - barHeight;
                using SolidBrush brush = new SolidBrush(colors[index % colors.Length]);
                g.FillRectangle(brush, x, y, barWidth, barHeight);
                g.DrawString(item.Value.ToString(), new Font("Segoe UI", 9F, FontStyle.Bold), Brushes.Black, x, y - 22);
                g.DrawString(item.Key, new Font("Segoe UI", 8.5F), Brushes.DimGray, new RectangleF(x - 6, area.Bottom + 8, barWidth + 28, 42));
                index++;
            }
        }

        private void chartPanel_Paint(object? sender, PaintEventArgs e)
        {
            PnlChart_Paint(sender, e);
        }

        private sealed class DashboardOrderRow
        {
            public string TrackingNumber { get; set; } = string.Empty;
            public string Route { get; set; } = string.Empty;
            public double Weight { get; set; }
            public decimal Cost { get; set; }
            public string Status { get; set; } = string.Empty;
            public string CreatedAt { get; set; } = string.Empty;
        }
    }
}
