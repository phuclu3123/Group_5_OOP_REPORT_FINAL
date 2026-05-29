using Logistics.Core.Services.Interfaces;
using System.Collections.Generic;
using Logistics.Core.DataAccess.Interfaces;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Models.Common;
using Logistics.Core.Utilities;

namespace Logistics.Core.Services.Implementations
{
    // Service quan ly kho hang: nhap/xuat hang, kiem tra suc chua
    public class WarehouseService : IWarehouseService
    {
        private readonly WarehouseRepository _warehouseRepo;
        private readonly INotificationService _notificationService;
        private readonly WarehouseInventoryLogRepository _inventoryLogRepo;
        private readonly OrderRepository? _orderRepo;

        public WarehouseService(WarehouseRepository warehouseRepo, INotificationService notificationService, WarehouseInventoryLogRepository inventoryLogRepo, OrderRepository? orderRepo = null)
        {
            _warehouseRepo = warehouseRepo;
            _notificationService = notificationService;
            _inventoryLogRepo = inventoryLogRepo;
            _orderRepo = orderRepo;
        }

        // Nhap hang vao kho
        public bool StorePackage(string warehouseId, Package package)
        {
            return StorePackage(warehouseId, package, string.Empty);
        }

        public bool StorePackage(string warehouseId, Package package, string shelfLocation)
        {
            Warehouse warehouse = _warehouseRepo.GetById(warehouseId);
            Package? packageToStore = ResolvePackage(package);
            if (warehouse == null || packageToStore == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(packageToStore.CurrentWarehouseID))
            {
                return false;
            }

            double weight = packageToStore.GetChargeableWeight();
            if (warehouse.AddUsedCapacity(weight))
            {
                packageToStore.CheckInWarehouse(warehouseId, shelfLocation);
                _warehouseRepo.Update(warehouse);
                PersistPackage(packageToStore, OrderStatus.ArrivedAtWarehouse, "Package checked in warehouse " + warehouseId);

                // Ghi log nhap kho
                int count = _inventoryLogRepo.Count() + 1;
                string logId = "WHL" + System.DateTime.Now.ToString("yyyyMMdd") + count.ToString("0000");
                string staffId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.UserId : "System";
                WarehouseInventoryLog log = new WarehouseInventoryLog(logId, warehouseId, packageToStore.PackageID, packageToStore.OrderID, shelfLocation, InventoryTransactionType.CheckIn, weight, staffId, "Check-in package");
                _inventoryLogRepo.Add(log);
                _inventoryLogRepo.SaveChanges();

                _notificationService.Notify("Cập nhật Kho", $"Kho {warehouse.Name} vừa nhập hàng (+{weight} kg)");
                return true;
            }
            return false;
        }

        // Xuat hang khoi kho
        public bool ReleasePackage(string warehouseId, Package package)
        {
            Warehouse warehouse = _warehouseRepo.GetById(warehouseId);
            Package? packageToRelease = ResolvePackage(package);
            if (warehouse == null || packageToRelease == null)
            {
                return false;
            }

            if (!packageToRelease.IsCurrentlyInWarehouse(warehouseId))
            {
                return false;
            }

            string shelfLocation = packageToRelease.CurrentShelfLocation;
            double weight = packageToRelease.GetChargeableWeight();
            warehouse.ReleaseCapacity(weight);
            packageToRelease.CheckOutWarehouse();
            _warehouseRepo.Update(warehouse);
            PersistPackage(packageToRelease, OrderStatus.ReadyForDispatch, "Package checked out from warehouse " + warehouseId);

            // Ghi log xuat kho
            int count = _inventoryLogRepo.Count() + 1;
            string logId = "WHL" + System.DateTime.Now.ToString("yyyyMMdd") + count.ToString("0000");
            string staffId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.UserId : "System";
            WarehouseInventoryLog log = new WarehouseInventoryLog(logId, warehouseId, packageToRelease.PackageID, packageToRelease.OrderID, shelfLocation, InventoryTransactionType.CheckOut, weight, staffId, "Check-out package");
            _inventoryLogRepo.Add(log);
            _inventoryLogRepo.SaveChanges();

            _notificationService.Notify("Cập nhật Kho", $"Kho {warehouse.Name} vừa xuất hàng (-{weight} kg)");
            return true;
        }

        // Kiem tra kho con cho khong
        public bool HasSpace(string warehouseId, double requiredSpace)
        {
            Warehouse warehouse = _warehouseRepo.GetById(warehouseId);
            if (warehouse == null)
            {
                return false;
            }
            return warehouse.HasSpace(requiredSpace);
        }

        // Tim kho con cho cho khoi luong nhat dinh
        public List<Warehouse> FindAvailableWarehouses(double requiredSpace)
        {
            return _warehouseRepo.FindWithAvailableSpace(requiredSpace);
        }

        // Lay tat ca kho
        public List<Warehouse> GetAllWarehouses()
        {
            return _warehouseRepo.GetAll();
        }

        // Lay thong tin kho theo ID
        public Warehouse GetWarehouse(string warehouseId)
        {
            return _warehouseRepo.GetById(warehouseId);
        }

        public Warehouse GetWarehouseById(string warehouseId)
        {
            return _warehouseRepo.GetById(warehouseId);
        }

        public void AddWarehouse(Warehouse warehouse)
        {
            _warehouseRepo.Add(warehouse);
        }

        public void UpdateWarehouse(Warehouse warehouse)
        {
            _warehouseRepo.Update(warehouse);
        }

        public void DeleteWarehouse(string warehouseId)
        {
            _warehouseRepo.Delete(warehouseId);
        }

        public List<WarehouseInventoryLog> GetInventoryLogs(string warehouseId)
        {
            return _inventoryLogRepo.FindByWarehouse(warehouseId);
        }

        public List<WarehouseInventoryLog> GetAllInventoryLogs()
        {
            return _inventoryLogRepo.GetAll();
        }

        private Package? ResolvePackage(Package? package)
        {
            if (package == null)
            {
                return null;
            }

            if (_orderRepo == null || string.IsNullOrWhiteSpace(package.OrderID))
            {
                return package;
            }

            Order order = _orderRepo.GetById(package.OrderID);
            if (order == null)
            {
                return package;
            }

            Package? existingPackage = order.FindPackage(package.PackageID);
            if (existingPackage == null)
            {
                return package;
            }

            return existingPackage;
        }

        private void PersistPackage(Package package, OrderStatus nextStatus, string description)
        {
            if (_orderRepo == null || package == null || string.IsNullOrWhiteSpace(package.OrderID))
            {
                return;
            }

            Order order = _orderRepo.GetById(package.OrderID);
            if (order != null)
            {
                if (CanWarehouseUpdateOrderStatus(order, nextStatus))
                {
                    order.ChangeStatus(nextStatus, description, "Warehouse");
                }

                _orderRepo.Update(order);
                _orderRepo.SaveChanges();
            }
        }

        private static bool CanWarehouseUpdateOrderStatus(Order order, OrderStatus nextStatus)
        {
            OrderStatus currentStatus = order.CurrentStatus;
            if (currentStatus == OrderStatus.Delivered ||
                currentStatus == OrderStatus.Cancelled ||
                currentStatus == OrderStatus.Failed ||
                currentStatus == OrderStatus.Returned)
            {
                return false;
            }

            if (nextStatus == OrderStatus.ArrivedAtWarehouse)
            {
                return currentStatus == OrderStatus.Pending ||
                       currentStatus == OrderStatus.WaitingPickup ||
                       currentStatus == OrderStatus.PickedUp;
            }

            if (nextStatus == OrderStatus.ReadyForDispatch)
            {
                return (currentStatus == OrderStatus.ArrivedAtWarehouse ||
                        currentStatus == OrderStatus.Sorting ||
                        currentStatus == OrderStatus.Pending) &&
                       AreAllPackagesOutOfWarehouse(order);
            }

            return false;
        }

        private static bool AreAllPackagesOutOfWarehouse(Order order)
        {
            if (order.Packages == null || order.Packages.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < order.Packages.Count; i++)
            {
                Package package = order.Packages[i];
                if (package == null)
                {
                    continue;
                }

                if (package.Status == PackageStatus.InWarehouse || package.Status == PackageStatus.Sorting)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
