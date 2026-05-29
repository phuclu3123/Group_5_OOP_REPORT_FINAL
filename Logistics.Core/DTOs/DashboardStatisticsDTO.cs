// DTO dùng để hiển thị thống kê tổng hợp trên Dashboard UI
namespace Logistics.Core.DTOs
{
    /// <summary>
    /// Dữ liệu thống kê tổng hợp theo tháng — dùng riêng cho Dashboard UI.
    /// Được sinh ra bởi ReportService.GetMonthlyStatistics().
    /// </summary>
    public class DashboardStatisticsDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int InTransitOrders { get; set; }
        public int DeliveredOrders { get; set; }
        public int CancelledOrders { get; set; }
        public int FailedOrders { get; set; }
        public int ReturnedOrders { get; set; }   // Đơn hàng hoàn trả

        public decimal TotalRevenue { get; set; }

        /// <summary>Tỷ lệ giao hàng thành công (0–100).</summary>
        public decimal SuccessRate { get; set; }

        // Thống kê tài nguyên
        public int TotalStaff { get; set; }
        public int TotalDrivers { get; set; }
        public int AvailableDrivers { get; set; }
        public int TotalVehicles { get; set; }
        public int AvailableVehicles { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalWarehouses { get; set; }
    }
}
