using Logistics.Core.Models.Business;

namespace Logistics.Core.Services.Interfaces
{
    /// <summary>
    /// Service gợi ý tối ưu hóa lộ trình: đề xuất tài xế và xe phù hợp cho đơn hàng.
    /// </summary>
    public interface IRouteOptimizationService
    {
        // Gợi ý cặp Tài xe + Xe phù hợp cho 1 Đơn hàng
        string SuggestDriverAndVehicle(Order order);
    }
}
