using Logistics.Core.DataAccess.Interfaces;
using System.Collections.Generic;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;

namespace Logistics.Core.DataAccess.Repositories
{
    public class VehicleRepository : JsonRepository<Vehicle>
    {
        public VehicleRepository(string filePath) : base(filePath) { }

        public override Vehicle GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].VehicleID == id)
                {
                    return _items[i];
                }
            }
            return null!;
        }

        public override void Update(Vehicle entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].VehicleID == entity.VehicleID)
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
                if (_items[i].VehicleID == id)
                {
                    _items.RemoveAt(i);
                    SaveChanges();
                    return;
                }
            }
        }

        // Tim xe san sang
        public List<Vehicle> FindAvailableVehicles()
        {
            List<Vehicle> result = new List<Vehicle>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].IsAvailable())
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }

        // Tim xe co the cho duoc khoi luong nhat dinh
        public List<Vehicle> FindByCapacity(double requiredWeight)
        {
            List<Vehicle> result = new List<Vehicle>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].CanCarry(requiredWeight))
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }

        // Tim xe theo loai
        public List<Vehicle> FindByType(VehicleType type)
        {
            List<Vehicle> result = new List<Vehicle>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].VehicleType == type)
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }
    }
}
