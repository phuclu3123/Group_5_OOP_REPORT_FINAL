// DTO dùng hiển thị thông tin nhân viên (Admin, Dispatcher, WarehouseStaff) trên UI
namespace Logistics.Core.DTOs
{
    public class StaffDTO
    {
        public string StaffId { get; set; } = string.Empty;
        public string PersonId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string WorkStatus { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }
        public decimal TotalSalary { get; set; }
        public string JoinDate { get; set; } = string.Empty;
        public int YearsOfService { get; set; }

        // Các trường đặc thù theo loại Staff (có thể null nếu không áp dụng)
        public string? ManagedRegion { get; set; }     // Cho Dispatcher
        public string? WarehouseId { get; set; }       // Cho WarehouseStaff
        public string? Shift { get; set; }              // Cho WarehouseStaff
        public string? AdminCode { get; set; }          // Cho Admin
    }
}
