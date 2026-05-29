using Logistics.Core.DTOs;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Utilities;

namespace Logistics.Core.Mappings
{
    /// <summary>
    /// Extension methods chuyển đổi giữa Vehicle (domain model) và VehicleDTO (display model).
    /// </summary>
    public static class VehicleMappingExtensions
    {
        // Vehicle → VehicleDTO (dùng cho DataGridView, Form display)
        public static VehicleDTO ToDTO(this Vehicle vehicle)
        {
            if (vehicle == null) return new VehicleDTO();

            VehicleDTO dto = new VehicleDTO();
            dto.VehicleId              = vehicle.VehicleID;
            dto.VehicleType            = EnumTranslator.TranslateVehicleType(vehicle.VehicleType);
            dto.Dimensions             = vehicle.Dimensions;
            dto.MaxWeightCapacityKg    = vehicle.MaxWeightCapacity;
            dto.MaxVolumeCapacityM3    = vehicle.MaxVolumeCapacity;
            dto.FuelLevel              = vehicle.FuelLevel;
            dto.CurrentOdometerKm      = vehicle.CurrentOdometer;
            dto.IsRefrigerated         = vehicle.IsRefrigerated;
            dto.IsAvailable            = vehicle.IsAvailable();
            dto.StatusDisplay          = EnumTranslator.TranslateVehicleStatus(vehicle.Status);

            return dto;
        }
    }
}
