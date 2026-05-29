using Logistics.Core.DTOs;
using Logistics.Core.Models.Business;

namespace Logistics.Core.Mappings
{
    /// <summary>
    /// Extension methods chuyển đổi giữa Package (domain model) và PackageDTO (display model).
    /// </summary>
    public static class PackageMappingExtensions
    {
        // Package → PackageDTO (dùng cho DataGridView trong form chi tiết đơn hàng)
        public static PackageDTO ToDTO(this Package package)
        {
            if (package == null) return new PackageDTO();

            PackageDTO dto = new PackageDTO();
            dto.PackageId            = package.PackageID;
            dto.OrderId              = package.OrderID;
            dto.Description          = package.Description;
            dto.ActualWeightKg       = package.ActualWeight;
            dto.VolumeWeightKg       = package.VolumeWeight;
            dto.ChargeableWeightKg   = package.GetChargeableWeight();
            dto.Dimensions           = package.Dimensions;
            dto.ItemCategory         = package.ItemCategory;
            dto.IsFragile            = package.CheckIfFragile();
            dto.Value                = package.Value;
            dto.HandlingInstructions = package.HandlingInstructions;
            dto.Status               = package.Status.ToString();
            dto.CurrentWarehouseId   = package.CurrentWarehouseID;
            dto.CurrentShelfLocation = package.CurrentShelfLocation;
            dto.LastScannedAt        = package.LastScannedAt.HasValue ? package.LastScannedAt.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty;

            return dto;
        }
    }
}
