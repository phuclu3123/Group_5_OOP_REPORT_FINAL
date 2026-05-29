using Logistics.Core.DataAccess.Interfaces;
using System.Collections.Generic;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Common;

namespace Logistics.Core.DataAccess.Repositories
{
    public class CustomerRepository : JsonRepository<Customer>
    {
        public CustomerRepository(string filePath) : base(filePath) { }

        // Tim khach hang theo ID
        public override Customer GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id == id)
                {
                    return _items[i];
                }
            }
            return null!;
        }

        // Cap nhat khach hang
        public override void Update(Customer entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id == entity.Id)
                {
                    _items[i] = entity;
                    SaveChanges();
                    return;
                }
            }
        }

        // Xoa khach hang theo ID
        public override void Delete(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id == id)
                {
                    _items.RemoveAt(i);
                    SaveChanges();
                    return;
                }
            }
        }

        // Tim khach hang theo ten
        public List<Customer> FindByName(string name)
        {
            List<Customer> result = new List<Customer>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].FullName.Contains(name))
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }

        // Tim khach hang theo loai
        public List<Customer> FindByType(CustomerType type)
        {
            List<Customer> result = new List<Customer>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].CustomerType == type)
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }
    }
}
