// DTO dùng hiển thị thông tin khách hàng trên DataGridView và Form UI
namespace Logistics.Core.DTOs
{
    public class CustomerDTO
    {
        public string CustomerId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string CustomerType { get; set; } = string.Empty;
        public int LoyaltyPoints { get; set; }
        public decimal CreditLimit { get; set; }
        public int TotalOrders { get; set; }
        public bool IsActive { get; set; }
    }
}
