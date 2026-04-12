using System.Collections.Generic;
using Logistic.Core.Models.Business;
using Logistic.Core.Models.Common;
using Logistic.Core.Interfaces;
using Logistic.Core.Mappings;

namespace Logistic.Core.Services
{
    // ============================================================
    // Dich vu giao hang (Delivery Service)
    // Implement IDeliveryService: xu ly logic gan don cho xe,
    // kiem tra suc chua, va quan ly trang thai giao hang.
    // Su dung InMemoryDataStore (Singleton) de truy cap du lieu chung.
    // ============================================================
    public class DeliveryService : IDeliveryService
    {
        // ===== DEPENDENCIES =====

        // Kho du lieu in-memory (Singleton)
        private InMemoryDataStore _dataStore;

        // ===== CONSTRUCTOR =====
        public DeliveryService()
        {
            _dataStore = InMemoryDataStore.GetInstance();
        }

        // ===== IDELIVERYSERVICE IMPLEMENTATION =====

        // Gan don hang vao phuong tien - kiem tra suc chua truoc khi gan
        public bool AssignOrderToVehicle(string orderId, string vehicleId, double weight, double volume)
        {
            // Tim phuong tien
            Vehicle vehicle = _dataStore.GetVehicleById(vehicleId);
            if (vehicle == null)
            {
                return false;
            }

            // Kiem tra phuong tien co san sang khong
            if (!vehicle.IsAvailable())
            {
                return false;
            }

            // Kiem tra suc chua truoc khi nap
            if (!vehicle.HasCapacityFor(weight, volume))
            {
                return false;
            }

            // Nap don hang len xe
            bool loaded = vehicle.LoadOrder(orderId, weight, volume);
            if (!loaded)
            {
                return false;
            }

            // Cap nhat trang thai don hang sang InTransit
            Order order = _dataStore.GetOrderByTracking(orderId);
            if (order != null)
            {
                order.UpdateStatus(OrderStatus.InTransit, "SYSTEM", "Gan don vao xe " + vehicleId);
            }

            return true;
        }

        // Kiem tra phuong tien co du suc chua cho khoi luong va the tich chi dinh
        public bool CheckVehicleCapacity(string vehicleId, double weight, double volume)
        {
            Vehicle vehicle = _dataStore.GetVehicleById(vehicleId);
            if (vehicle == null)
            {
                return false;
            }
            return vehicle.HasCapacityFor(weight, volume);
        }

        // Lay danh sach phuong tien san sang (trang thai Ready)
        public List<Vehicle> GetAvailableVehicles()
        {
            List<Vehicle> allVehicles = _dataStore.GetAllVehicles();
            List<Vehicle> availableVehicles = new List<Vehicle>();
            for (int i = 0; i < allVehicles.Count; i++)
            {
                if (allVehicles[i].IsAvailable())
                {
                    availableVehicles.Add(allVehicles[i]);
                }
            }
            return availableVehicles;
        }

        // Danh dau don hang da giao thanh cong va do hang khoi xe
        public bool CompleteDelivery(string orderId, string vehicleId)
        {
            // Tim phuong tien
            Vehicle vehicle = _dataStore.GetVehicleById(vehicleId);
            if (vehicle == null)
            {
                return false;
            }

            // Tim don hang
            Order order = _dataStore.GetOrderByTracking(orderId);
            if (order == null)
            {
                return false;
            }

            // Do hang khoi xe (dung khoi luong va the tich = 0 vi da giao)
            vehicle.UnloadOrder(orderId, order.TotalWeight, 0);

            // Cap nhat trang thai don hang
            order.UpdateStatus(OrderStatus.Delivered, "SYSTEM", "Giao hang thanh cong boi xe " + vehicleId);

            return true;
        }

        // Lay trang thai giao hang cua don
        public string GetDeliveryStatus(string orderId)
        {
            Order order = _dataStore.GetOrderByTracking(orderId);
            if (order == null)
            {
                return "Khong tim thay don hang: " + orderId;
            }
            return order.GetTrackingInfo();
        }

        // ===== ADDITIONAL METHODS =====

        // Tim phuong tien phu hop nhat cho khoi luong va the tich chi dinh
        // Tra ve phuong tien co suc chua con lai nho nhat (Best Fit)
        public Vehicle FindBestFitVehicle(double weight, double volume)
        {
            List<Vehicle> availableVehicles = GetAvailableVehicles();
            Vehicle bestFit = null;
            double smallestRemainingWeight = double.MaxValue;

            for (int i = 0; i < availableVehicles.Count; i++)
            {
                Vehicle current = availableVehicles[i];
                if (current.HasCapacityFor(weight, volume))
                {
                    double remainingWeight = current.GetRemainingWeight();
                    if (remainingWeight < smallestRemainingWeight)
                    {
                        smallestRemainingWeight = remainingWeight;
                        bestFit = current;
                    }
                }
            }
            return bestFit;
        }
    }
}
