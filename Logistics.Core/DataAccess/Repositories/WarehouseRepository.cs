using Logistics.Core.DataAccess.Interfaces;
using System.Collections.Generic;
using System.IO;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Services.Interfaces;

namespace Logistics.Core.DataAccess.Repositories
{
    public class WarehouseRepository : JsonRepository<Warehouse>
    {
        public WarehouseRepository(string filePath) : base(filePath) { }

        public override Warehouse GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].WarehouseID == id)
                {
                    return _items[i];
                }
            }
            return null!;
        }

        public override void Update(Warehouse entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].WarehouseID == entity.WarehouseID)
                {
                    _items[i] = entity;
                    SaveChanges();
                    return;
                }
            }
        }

        public override void Delete(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].WarehouseID == id)
                {
                    _items.RemoveAt(i);
                    SaveChanges();
                    return;
                }
            }
        }

        // Tim kho con cho
        public List<Warehouse> FindWithAvailableSpace(double requiredSpace)
        {
            List<Warehouse> result = new List<Warehouse>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].HasSpace(requiredSpace))
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }

        // Tim kho theo loai
        public List<Warehouse> FindByType(WarehouseType type)
        {
            List<Warehouse> result = new List<Warehouse>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Type == type)
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }

        public void SeedData()
        {
            if (File.Exists(_filePath)) return;
            if (_items.Count > 0) return;

            Add(new Warehouse("W001", "Kho Trung Tâm Miền Bắc", "Số 1 Duy Tân, Cầu Giấy, Hà Nội", 
                new GeoPoint(21.0285, 105.8542), Logistics.Core.Models.Common.WarehouseType.FulfillmentCenter, 5000, "08:00 - 20:00", null));
            
            Add(new Warehouse("W002", "Trạm Trung Chuyển Đà Nẵng", "Lô 12 KCN Hòa Cầm, Đà Nẵng", 
                new GeoPoint(16.0544, 108.2022), Logistics.Core.Models.Common.WarehouseType.TransshipmentPoint, 2000, "24/7", null));
            
            Add(new Warehouse("W003", "Kho Phân Loại Miền Nam", "Khu Công Nghệ Cao, Quận 9, TP.HCM", 
                new GeoPoint(10.8231, 106.6297), Logistics.Core.Models.Common.WarehouseType.SortingCenter, 8000, "06:00 - 22:00", null));
            
            Add(new Warehouse("W004", "Kho Lạnh Tân Bình", "150 Trường Chinh, Tân Bình, TP.HCM", 
                new GeoPoint(10.7944, 106.6436), Logistics.Core.Models.Common.WarehouseType.ColdStorage, 1500, "08:00 - 18:00", null));

            // Cập nhật ngẫu nhiên dữ liệu đã dùng để demo
            _items[0].AddUsedCapacity(1250.5);
            _items[1].AddUsedCapacity(450.2);
            _items[2].AddUsedCapacity(3200.8);
            
            SaveChanges();
        }
    }
}
