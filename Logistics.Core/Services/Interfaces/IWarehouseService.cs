using System.Collections.Generic;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Infrastructure;

namespace Logistics.Core.Services.Interfaces
{
    /// <summary>
    /// Service quản lý kho: nhập/xuất hàng, kiểm tra sức chứa.
    /// </summary>
    public interface IWarehouseService
    {
        // Nhập hàng vào kho
        bool StorePackage(string warehouseId, Package package);
        bool StorePackage(string warehouseId, Package package, string shelfLocation);

        // Xuất hàng khỏi kho
        bool ReleasePackage(string warehouseId, Package package);

        // Kiểm tra kho còn chỗ không
        bool HasSpace(string warehouseId, double requiredSpace);

        // Tìm kho còn chỗ cho khối lượng nhất định
        List<Warehouse> FindAvailableWarehouses(double requiredSpace);

        // Lấy tất cả kho
        List<Warehouse> GetAllWarehouses();

        // Lấy thông tin kho theo ID
        Warehouse GetWarehouse(string warehouseId);
        Warehouse GetWarehouseById(string warehouseId);

        // CRUD
        void AddWarehouse(Warehouse warehouse);
        void UpdateWarehouse(Warehouse warehouse);
        void DeleteWarehouse(string warehouseId);

        // Inventory Logs
        List<WarehouseInventoryLog> GetInventoryLogs(string warehouseId);
        List<WarehouseInventoryLog> GetAllInventoryLogs();
    }
}
