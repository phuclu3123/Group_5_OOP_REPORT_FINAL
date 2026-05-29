// DTO dùng hiển thị thông tin gói hàng trong form tạo / xem đơn hàng
namespace Logistics.Core.DTOs
{
    public class PackageDTO
    {
        public string PackageId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double ActualWeightKg { get; set; }
        public double VolumeWeightKg { get; set; }
        public double ChargeableWeightKg { get; set; }
        public string Dimensions { get; set; } = string.Empty;
        public string ItemCategory { get; set; } = string.Empty;
        public bool IsFragile { get; set; }
        public decimal Value { get; set; }
        public string HandlingInstructions { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CurrentWarehouseId { get; set; } = string.Empty;
        public string CurrentShelfLocation { get; set; } = string.Empty;
        public string LastScannedAt { get; set; } = string.Empty;
    }
}
