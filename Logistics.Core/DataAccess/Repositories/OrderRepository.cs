using Logistics.Core.DataAccess.Interfaces;
using System.Collections.Generic;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;

namespace Logistics.Core.DataAccess.Repositories
{
    public class OrderRepository : JsonRepository<Order>
    {
        public OrderRepository(string filePath) : base(filePath)
        {
            RepairInvalidOrderData();
        }

        private void RepairInvalidOrderData()
        {
            bool changed = false;
            List<Order> validOrders = new List<Order>();

            for (int i = 0; i < _items.Count; i++)
            {
                Order order = _items[i];
                if (order == null || order.IsBlankRecord())
                {
                    changed = true;
                    continue;
                }

                order.EnsureRuntimeDefaults();
                if (order.IsStructurallyValid())
                {
                    validOrders.Add(order);
                }
                else
                {
                    changed = true;
                }
            }

            if (_items.Count > 0 && validOrders.Count == 0)
            {
                _items = DataSeeder.BuildSampleOrders();
                SaveChanges();
                return;
            }

            if (changed)
            {
                _items = validOrders;
                SaveChanges();
            }
        }

        // Tim don hang theo tracking number
        public override Order GetById(string id)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].TrackingNumber == id)
                {
                    return _items[i];
                }
            }
            return null!;
        }

        public override void Update(Order entity)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].TrackingNumber == entity.TrackingNumber)
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
                if (_items[i].TrackingNumber == id)
                {
                    _items.RemoveAt(i);
                    SaveChanges();
                    return;
                }
            }
        }

        // Tim don hang theo trang thai
        public List<Order> FindByStatus(OrderStatus status)
        {
            List<Order> result = new List<Order>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].CurrentStatus == status)
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }

        // Tim don hang theo khach gui
        public List<Order> FindBySender(string senderId)
        {
            List<Order> result = new List<Order>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].SenderID == senderId)
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }

        // Tim don hang theo khach nhan
        public List<Order> FindByReceiver(string receiverId)
        {
            List<Order> result = new List<Order>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].ReceiverID == receiverId)
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }

        // Tim don hang theo tai xe
        public List<Order> FindByDriver(string driverId)
        {
            List<Order> result = new List<Order>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].AssignedDriverID == driverId)
                {
                    result.Add(_items[i]);
                }
            }
            return result;
        }
    }
}
