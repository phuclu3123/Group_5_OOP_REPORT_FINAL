using Logistics.Core.DTOs;
using Logistics.Core.Models.Business;

namespace Logistics.Core.Mappings
{
    /// <summary>
    /// Extension methods chuyển đổi giữa Order (domain model) và OrderDTO (display model).
    /// Tách biệt logic ánh xạ ra khỏi class Model để giữ Model thuần túy (chỉ properties + domain behavior).
    /// </summary>
    public static class OrderMappingExtensions
    {
        // Order  →  OrderDTO  (dùng cho DataGridView, Form display)
        public static OrderDTO ToDTO(this Order order)
        {
            if (order == null) return new OrderDTO();

            double totalWeight = 0;
            if (order.Packages != null)
            {
                for (int i = 0; i < order.Packages.Count; i++)
                {
                    if (order.Packages[i] != null)
                        totalWeight += order.Packages[i].ActualWeight;
                }
            }

            OrderDTO dto = new OrderDTO();
            dto.OrderId          = order.TrackingNumber;
            dto.TrackingNumber   = order.TrackingNumber;
            dto.TotalWeight      = totalWeight;
            dto.Status           = order.CurrentStatus.ToString();
            dto.CreatedAt        = order.CreatedDate;
            dto.SenderID         = order.SenderID;
            dto.ReceiverID       = order.ReceiverID;

            if (order.Route != null)
            {
                dto.PickupAddress   = order.Route.PickupAddress   ?? string.Empty;
                dto.DeliveryAddress = order.Route.DeliveryAddress ?? string.Empty;
            }

            dto.CodAmount        = order.CodAmount;
            dto.CodStatus        = order.CodStatus.ToString();
            dto.FailureCount     = order.FailureCount;
            dto.TotalVolume      = order.GetTotalVolume();

            return dto;
        }

        // OrderDTO  →  Order fields  (dùng khi cần cập nhật Model từ dữ liệu UI nhập vào)
        public static void CopyToOrder(this OrderDTO dto, Order order)
        {
            if (dto == null || order == null) return;
            order.UpdateDeliveryAddress(dto.DeliveryAddress ?? string.Empty);
        }
    }
}
