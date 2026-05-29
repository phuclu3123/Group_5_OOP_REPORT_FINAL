// DTO dùng hiển thị thông tin tài xế trên DataGridView và Form UI
namespace Logistics.Core.DTOs
{
    public class DriverDTO
    {
        public string DriverId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string LicenseType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string WorkStatus { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public int DeliveryCount { get; set; }
        public decimal TotalSalary { get; set; }
    }
}
