using System.Collections.Generic;
using Logistic.Core.Models.Business;
using Logistic.Core.Models.Common;
using Logistic.Core.Models.Actors;
using Logistic.Core.Mappings;

namespace Logistic.Core.Services
{
    // ============================================================
    // Dich vu dieu phoi don hang (Dispatch Service)
    // Phan cong don hang cho tai xe dua tren khu vuc va trang thai.
    // Su dung InMemoryDataStore (Singleton) de truy cap du lieu chung.
    // ============================================================
    public class DispatchService
    {
        // ===== DEPENDENCIES =====

        // Kho du lieu in-memory (Singleton)
        private InMemoryDataStore _dataStore;

        // Danh sach tai xe dang quan ly
        private List<Driver> _drivers;

        // ===== CONSTRUCTOR =====
        public DispatchService()
        {
            _dataStore = InMemoryDataStore.GetInstance();
            _drivers = new List<Driver>();
        }

        // ===== DISPATCH METHODS =====

        // Them tai xe vao danh sach quan ly
        public void RegisterDriver(Driver driver)
        {
            if (driver == null)
            {
                return;
            }
            // Kiem tra trung lap
            for (int i = 0; i < _drivers.Count; i++)
            {
                if (_drivers[i].StaffID == driver.StaffID)
                {
                    return;
                }
            }
            _drivers.Add(driver);
        }

        // Phan cong don hang cho tai xe san sang dau tien (Round Robin don gian)
        public bool DispatchOrder(string trackingNumber)
        {
            // Tim don hang
            Order order = _dataStore.GetOrderByTracking(trackingNumber);
            if (order == null)
            {
                return false;
            }

            // Chi dieu phoi don hang o trang thai Pending
            if (order.CurrentStatus != OrderStatus.Pending)
            {
                return false;
            }

            // Tim tai xe san sang
            Driver availableDriver = FindAvailableDriver();
            if (availableDriver == null)
            {
                return false;
            }

            // Gan tai xe cho don hang
            order.AssignDriver(availableDriver.StaffID);
            order.UpdateStatus(OrderStatus.InTransit, availableDriver.StaffID,
                               "Dieu phoi boi tai xe " + availableDriver.FullName);

            // Cap nhat trang thai tai xe
            availableDriver.StartDelivery();

            return true;
        }

        // Lay danh sach don hang dang cho xu ly (Pending)
        public List<Order> GetPendingOrders()
        {
            List<Order> allOrders = _dataStore.GetAllOrders();
            List<Order> pendingOrders = new List<Order>();
            for (int i = 0; i < allOrders.Count; i++)
            {
                if (allOrders[i].CurrentStatus == OrderStatus.Pending)
                {
                    pendingOrders.Add(allOrders[i]);
                }
            }
            return pendingOrders;
        }

        // Lay danh sach tai xe san sang
        public List<Driver> GetAvailableDrivers()
        {
            List<Driver> availableDrivers = new List<Driver>();
            for (int i = 0; i < _drivers.Count; i++)
            {
                if (_drivers[i].IsAvailable())
                {
                    availableDrivers.Add(_drivers[i]);
                }
            }
            return availableDrivers;
        }

        // Danh dau tai xe hoan thanh chuyen giao hang
        public bool MarkDeliveryComplete(string driverId, string trackingNumber)
        {
            // Tim tai xe
            Driver driver = FindDriverById(driverId);
            if (driver == null)
            {
                return false;
            }

            // Tim don hang
            Order order = _dataStore.GetOrderByTracking(trackingNumber);
            if (order == null)
            {
                return false;
            }

            // Cap nhat trang thai
            driver.CompleteDelivery();
            order.UpdateStatus(OrderStatus.Delivered, driverId,
                               "Hoan tat boi tai xe " + driver.FullName);

            return true;
        }

        // Lay so luong tai xe dang quan ly
        public int GetDriverCount()
        {
            return _drivers.Count;
        }

        // ===== PRIVATE HELPERS =====

        // Tim tai xe san sang dau tien
        private Driver FindAvailableDriver()
        {
            for (int i = 0; i < _drivers.Count; i++)
            {
                if (_drivers[i].IsAvailable())
                {
                    return _drivers[i];
                }
            }
            return null;
        }

        // Tim tai xe theo ID
        private Driver FindDriverById(string driverId)
        {
            for (int i = 0; i < _drivers.Count; i++)
            {
                if (_drivers[i].StaffID == driverId)
                {
                    return _drivers[i];
                }
            }
            return null;
        }
    }
}
