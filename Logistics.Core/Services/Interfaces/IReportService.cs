using Logistics.Core.DTOs;

namespace Logistics.Core.Services.Interfaces
{
    /// <summary>
    /// Service tạo báo cáo và thống kê: hóa đơn, dashboard tháng, tổng quan.
    /// </summary>
    public interface IReportService
    {
        // Tạo hóa đơn văn bản cho đơn hàng
        string GenerateInvoice(string orderId);

        // Thống kê tổng hợp theo tháng
        DashboardStatisticsDTO GetMonthlyStatistics(int year, int month);

        // Thống kê tổng hợp toàn bộ dữ liệu (không lọc tháng)
        DashboardStatisticsDTO GetOverallStatistics();
    }
}
