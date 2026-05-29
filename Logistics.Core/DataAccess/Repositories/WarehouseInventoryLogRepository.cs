using System.Collections.Generic;
using Logistics.Core.Models.Infrastructure;

namespace Logistics.Core.DataAccess.Repositories
{
    public class WarehouseInventoryLogRepository : JsonRepository<WarehouseInventoryLog>
    {
        public WarehouseInventoryLogRepository(string filePath) : base(filePath)
        {
        }

        public override WarehouseInventoryLog GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null && _items[i].LogID == id)
                {
                    return _items[i];
                }
            }

            return null!;
        }

        public List<WarehouseInventoryLog> FindByWarehouse(string warehouseId)
        {
            List<WarehouseInventoryLog> result = new List<WarehouseInventoryLog>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null && _items[i].WarehouseID == warehouseId)
                {
                    result.Add(_items[i]);
                }
            }

            return result;
        }

        public List<WarehouseInventoryLog> FindByPackage(string packageId)
        {
            List<WarehouseInventoryLog> result = new List<WarehouseInventoryLog>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null && _items[i].PackageID == packageId)
                {
                    result.Add(_items[i]);
                }
            }

            return result;
        }

        public override void Update(WarehouseInventoryLog entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null && _items[i].LogID == entity.LogID)
                {
                    _items[i] = entity;
                    SaveChanges();
                    return;
                }
            }
        }
    }
}
