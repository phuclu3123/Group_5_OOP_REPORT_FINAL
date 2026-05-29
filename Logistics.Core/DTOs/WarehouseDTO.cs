// DTO dùng hiển thị thông tin kho hàng trên DataGridView và Form UI
namespace Logistics.Core.DTOs
{
    public class WarehouseDTO
    {
        public string WarehouseId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string WarehouseType { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double TotalCapacityM3 { get; set; }
        public double UsedCapacityM3 { get; set; }
        public double AvailableCapacityM3 { get; set; }
        public double UtilisationPercent { get; set; }
        public int TotalLocations { get; set; }
    }
}
