using Logistics.Core.DataAccess.Interfaces;
using System.Collections.Generic;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Common;

namespace Logistics.Core.DataAccess.Repositories
{
    // Repository chung cho Driver, Dispatcher, WarehouseStaff
    // Moi loai staff luu trong file JSON rieng

    public class DriverRepository : JsonRepository<Driver>
    {
        public DriverRepository(string filePath) : base(filePath) { }

        public override Driver GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].StaffID == id)
                {
                    return _items[i];
                }
            }
            return null!;
        }

        public override void Update(Driver entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].StaffID == entity.StaffID)
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
                if (_items[i].StaffID == id)
                {
                    _items.RemoveAt(i);
                    SaveChanges();
                    return;
                }
            }
        }

        // Tim tai xe san sang
        public List<Driver> FindAvailableDrivers()
        {
            List<Driver> result = new List<Driver>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].IsAvailable() && _items[i].IsActive())
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }

        // Tim tai xe theo trang thai
        public List<Driver> FindByStatus(DriverStatus status)
        {
            List<Driver> result = new List<Driver>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].DriverStatus == status)
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }
    }

    public class DispatcherRepository : JsonRepository<Dispatcher>
    {
        public DispatcherRepository(string filePath) : base(filePath) { }

        public override Dispatcher GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].StaffID == id)
                {
                    return _items[i];
                }
            }
            return null!;
        }

        public override void Update(Dispatcher entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].StaffID == entity.StaffID)
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
                if (_items[i].StaffID == id)
                {
                    _items.RemoveAt(i);
                    SaveChanges();
                    return;
                }
            }
        }
    }

    public class WarehouseStaffRepository : JsonRepository<WarehouseStaff>
    {
        public WarehouseStaffRepository(string filePath) : base(filePath) { }

        public override WarehouseStaff GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].StaffID == id)
                {
                    return _items[i];
                }
            }
            return null!;
        }

        public override void Update(WarehouseStaff entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].StaffID == entity.StaffID)
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
                if (_items[i].StaffID == id)
                {
                    _items.RemoveAt(i);
                    SaveChanges();
                    return;
                }
            }
        }

        // Tim nhan vien theo kho
        public List<WarehouseStaff> FindByWarehouse(string warehouseId)
        {
            List<WarehouseStaff> result = new List<WarehouseStaff>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].WarehouseID == warehouseId)
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }
    }

    public class AdminRepository : JsonRepository<Admin>
    {
        public AdminRepository(string filePath) : base(filePath) { }

        public override Admin GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].StaffID == id)
                {
                    return _items[i];
                }
            }
            return null!;
        }

        public override void Update(Admin entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].StaffID == entity.StaffID)
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
                if (_items[i].StaffID == id)
                {
                    _items.RemoveAt(i);
                    SaveChanges();
                    return;
                }
            }
        }
    }
}
