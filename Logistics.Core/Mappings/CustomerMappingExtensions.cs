using Logistics.Core.DTOs;
using Logistics.Core.Models.Actors;

namespace Logistics.Core.Mappings
{
    /// <summary>
    /// Extension methods chuyển đổi giữa Customer (domain model) và CustomerDTO (display model).
    /// Tách biệt logic ánh xạ ra khỏi class Model để giữ Model thuần túy.
    /// </summary>
    public static class CustomerMappingExtensions
    {
        // Customer  →  CustomerDTO  (dùng cho DataGridView, Form display)
        public static CustomerDTO ToDTO(this Customer customer)
        {
            if (customer == null) return new CustomerDTO();

            CustomerDTO dto = new CustomerDTO();
            dto.CustomerId   = customer.Id;
            dto.FullName     = customer.FullName;
            dto.Phone        = customer.PhoneNumber;
            dto.Email        = customer.Email;
            dto.Address      = customer.HomeAddress != null ? customer.HomeAddress.ToString() : string.Empty;
            dto.CustomerType = customer.CustomerType.ToString();
            dto.LoyaltyPoints = customer.LoyaltyPoints;
            dto.CreditLimit  = customer.CreditLimit;
            dto.TotalOrders  = 0; // Được điền bởi OrderService.GetOrdersByCustomer().Count từ UI

            dto.IsActive = customer.IsActive;
            return dto;
        }
    }
}
