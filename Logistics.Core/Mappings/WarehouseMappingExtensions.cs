using Logistics.Core.DTOs;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Utilities;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Mappings
{
    /// <summary>
    /// Extension methods chuyển đổi giữa Warehouse (domain model) và WarehouseDTO (display model).
    /// </summary>
    public static class WarehouseMappingExtensions
    {
        // Warehouse → WarehouseDTO (dùng cho DataGridView, Form display)
        public static WarehouseDTO ToDTO(this Warehouse warehouse)
        {
            if (warehouse == null) return new WarehouseDTO();

            WarehouseDTO dto = new WarehouseDTO();
            dto.WarehouseId          = warehouse.WarehouseID;
            dto.Name                 = warehouse.Name;
            dto.WarehouseType        = EnumTranslator.TranslateWarehouseType(warehouse.Type);
            dto.Address              = warehouse.Address;
            dto.TotalCapacityM3      = warehouse.TotalCapacity;
            dto.UsedCapacityM3       = warehouse.UsedCapacity;
            dto.AvailableCapacityM3  = warehouse.GetAvailableCapacity();
            dto.UtilisationPercent   = warehouse.GetUsagePercentage();
            dto.TotalLocations       = 0; // Điền từ WarehouseLocation list nếu có

            return dto;
        }
    }
}
