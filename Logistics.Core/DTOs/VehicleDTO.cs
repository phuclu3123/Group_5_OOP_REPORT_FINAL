// DTO dùng hiển thị thông tin phương tiện trên DataGridView và Form UI
namespace Logistics.Core.DTOs
{
    public class VehicleDTO
    {
        public string VehicleId { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string Dimensions { get; set; } = string.Empty;
        public double MaxWeightCapacityKg { get; set; }
        public double MaxVolumeCapacityM3 { get; set; }
        public double FuelLevel { get; set; }
        public double CurrentOdometerKm { get; set; }
        public bool IsRefrigerated { get; set; }
        public bool IsAvailable { get; set; }
        public string StatusDisplay { get; set; } = string.Empty;
    }
}
