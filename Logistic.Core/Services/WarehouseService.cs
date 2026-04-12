using System.Collections.Generic;
using Logistic.Core.Models.Business;
using Logistic.Core.Models.Infrastructure;
using Logistic.Core.Mappings;

namespace Logistic.Core.Services
{
    // ============================================================
    // Dich vu quan ly kho (Warehouse Service)
    // Xu ly nhap/xuat goi hang, kiem tra ton kho, quan ly suc chua kho.
    // Su dung InMemoryDataStore (Singleton) de truy cap du lieu chung.
    // ============================================================
    public class WarehouseService
    {
        // ===== DEPENDENCIES =====

        // Kho du lieu in-memory (Singleton)
        private InMemoryDataStore _dataStore;

        // Danh sach kho hang dang quan ly
        private List<Warehouse> _warehouses;

        // Danh sach goi hang dang trong kho (key: warehouseId, value: danh sach packageId)
        private Dictionary<string, List<string>> _warehouseInventory;

        // ===== CONSTRUCTOR =====
        public WarehouseService()
        {
            _dataStore = InMemoryDataStore.GetInstance();
            _warehouses = new List<Warehouse>();
            _warehouseInventory = new Dictionary<string, List<string>>();
        }

        // ===== WAREHOUSE MANAGEMENT =====

        // Dang ky kho hang vao he thong
        public void RegisterWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
            {
                return;
            }
            // Kiem tra trung lap
            for (int i = 0; i < _warehouses.Count; i++)
            {
                if (_warehouses[i].WarehouseID == warehouse.WarehouseID)
                {
                    return;
                }
            }
            _warehouses.Add(warehouse);
            // Khoi tao danh sach ton kho cho kho moi
            if (!_warehouseInventory.ContainsKey(warehouse.WarehouseID))
            {
                _warehouseInventory.Add(warehouse.WarehouseID, new List<string>());
            }
        }

        // ===== PACKAGE OPERATIONS =====

        // Nhap goi hang vao kho
        public bool ReceivePackage(string warehouseId, string packageId, double weight)
        {
            // Tim kho
            Warehouse warehouse = FindWarehouseById(warehouseId);
            if (warehouse == null)
            {
                return false;
            }

            // Kiem tra kho con du suc chua khong
            if (!warehouse.HasSpace(weight))
            {
                return false;
            }

            // Tang dung luong su dung cua kho
            bool added = warehouse.AddUsedCapacity(weight);
            if (!added)
            {
                return false;
            }

            // Them packageId vao danh sach ton kho
            if (_warehouseInventory.ContainsKey(warehouseId))
            {
                _warehouseInventory[warehouseId].Add(packageId);
            }

            return true;
        }

        // Xuat goi hang khoi kho
        public bool DispatchPackage(string warehouseId, string packageId, double weight)
        {
            // Tim kho
            Warehouse warehouse = FindWarehouseById(warehouseId);
            if (warehouse == null)
            {
                return false;
            }

            // Kiem tra goi hang co trong kho khong
            if (!_warehouseInventory.ContainsKey(warehouseId))
            {
                return false;
            }

            List<string> inventory = _warehouseInventory[warehouseId];
            bool found = false;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i] == packageId)
                {
                    inventory.RemoveAt(i);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return false;
            }

            // Giam dung luong su dung cua kho
            warehouse.ReleaseCapacity(weight);
            return true;
        }

        // ===== QUERY OPERATIONS =====

        // Lay danh sach goi hang trong kho
        public List<string> GetInventory(string warehouseId)
        {
            if (!_warehouseInventory.ContainsKey(warehouseId))
            {
                return new List<string>();
            }

            // Tra ve ban sao de bao ve du lieu goc (Encapsulation)
            List<string> inventory = _warehouseInventory[warehouseId];
            List<string> copy = new List<string>();
            for (int i = 0; i < inventory.Count; i++)
            {
                copy.Add(inventory[i]);
            }
            return copy;
        }

        // Kiem tra goi hang co trong kho khong
        public bool CheckAvailability(string warehouseId, string packageId)
        {
            if (!_warehouseInventory.ContainsKey(warehouseId))
            {
                return false;
            }

            List<string> inventory = _warehouseInventory[warehouseId];
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i] == packageId)
                {
                    return true;
                }
            }
            return false;
        }

        // Lay thong tin suc chua con lai cua kho
        public double GetAvailableCapacity(string warehouseId)
        {
            Warehouse warehouse = FindWarehouseById(warehouseId);
            if (warehouse == null)
            {
                return 0;
            }
            return warehouse.GetAvailableCapacity();
        }

        // Lay bao cao tong hop cua kho
        public string GetWarehouseReport(string warehouseId)
        {
            Warehouse warehouse = FindWarehouseById(warehouseId);
            if (warehouse == null)
            {
                return "Khong tim thay kho: " + warehouseId;
            }
            return warehouse.GenerateReport();
        }

        // Lay so luong kho dang quan ly
        public int GetWarehouseCount()
        {
            return _warehouses.Count;
        }

        // ===== PRIVATE HELPERS =====

        // Tim kho theo ID
        private Warehouse FindWarehouseById(string warehouseId)
        {
            for (int i = 0; i < _warehouses.Count; i++)
            {
                if (_warehouses[i].WarehouseID == warehouseId)
                {
                    return _warehouses[i];
                }
            }
            return null;
        }
    }
}
