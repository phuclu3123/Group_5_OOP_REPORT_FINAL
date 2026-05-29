using Logistics.Core.Services.Interfaces;
using System.Collections.Generic;
using Logistics.Core.Configuration;
using Logistics.Core.DataAccess.Interfaces;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Exceptions;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Account;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Utilities;

namespace Logistics.Core.Services.Implementations
{
    // Service quan ly giao hang: gan tai xe, xe, theo doi
    public class DeliveryService : IDeliveryService
    {
        private DriverRepository _driverRepo;
        private VehicleRepository _vehicleRepo;
        private OrderRepository _orderRepo;
        private DeliveryTripRepository _tripRepo;
        private TransactionRepository _transactionRepo;
        private CustomerRepository? _customerRepo;
        private BusinessRules _businessRules;

        public DeliveryService(DriverRepository driverRepo, VehicleRepository vehicleRepo, OrderRepository orderRepo, DeliveryTripRepository tripRepo, TransactionRepository transactionRepo, CustomerRepository? customerRepo = null, BusinessRules? businessRules = null)
        {
            _driverRepo = driverRepo;
            _vehicleRepo = vehicleRepo;
            _orderRepo = orderRepo;
            _tripRepo = tripRepo;
            _transactionRepo = transactionRepo;
            _customerRepo = customerRepo;
            _businessRules = businessRules ?? new BusinessRules();
        }

        // Gan tai xe cho don hang
        public bool AssignDriver(string trackingNumber, string driverId)
        {
            if (!IsAdminOrDispatcher())
            {
                return false;
            }

            Order order = _orderRepo.GetById(trackingNumber);
            Driver driver = _driverRepo.GetById(driverId);

            if (order == null || driver == null)
            {
                return false;
            }

            if (!driver.IsAvailable())
            {
                return false;
            }

            if (!order.CanTransitionTo(OrderStatus.InTransit))
            {
                return false;
            }

            order.AssignDriver(driverId);
            order.ChangeStatus(OrderStatus.InTransit);
            driver.StartDelivery();

            _orderRepo.SaveChanges();
            _driverRepo.SaveChanges();
            return true;
        }

        // Hoan thanh giao hang
        public bool CompleteDelivery(string trackingNumber)
        {
            Order order = _orderRepo.GetById(trackingNumber);
            if (order == null)
            {
                return false;
            }

            if (!CanCompleteOrder(order))
            {
                return false;
            }

            if (order.CurrentStatus != OrderStatus.InTransit && order.CurrentStatus != OrderStatus.OutForDelivery)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(order.AssignedDriverID) && string.IsNullOrWhiteSpace(order.AssignedVehicleID))
            {
                return false;
            }

            order.ChangeStatus(OrderStatus.Delivered);
            AwardLoyaltyPoints(order);

            // Xu ly thu tien COD neu co
            if (order.CodAmount > 0)
            {
                order.UpdateCodStatus(CodStatus.CollectedByDriver);
                
                int count = _transactionRepo.Count() + 1;
                string txnId = "TXN" + System.DateTime.Now.ToString("yyyyMMdd") + count.ToString("0000");
                Transaction transaction = new Transaction(txnId, order.TrackingNumber, order.CodAmount, PaymentMethod.COD);
                transaction.CompleteTransaction();
                _transactionRepo.Add(transaction);
                _transactionRepo.SaveChanges();
            }

            bool releasedByTrip = CompleteTripIfReady(order.TrackingNumber);
            if (!releasedByTrip)
            {
                ReleaseSingleOrderResources(order);
            }

            _orderRepo.SaveChanges();
            return true;
        }

        private void AwardLoyaltyPoints(Order order)
        {
            if (_customerRepo == null || order == null)
            {
                return;
            }

            Customer customer = _customerRepo.GetById(order.SenderID);
            if (customer == null)
            {
                return;
            }

            int points = _businessRules.CalculateEarnedPoints(order.TotalCost, customer.CustomerType);
            if (points <= 0)
            {
                return;
            }

            customer.AddLoyaltyPoints(points);
            CustomerType suggestedType = _businessRules.SuggestCustomerType(customer.LoyaltyPoints, 0);
            if (suggestedType > customer.CustomerType)
            {
                customer.UpdateCustomerType(suggestedType);
                customer.UpdateCreditLimit(_businessRules.GetDefaultCreditLimit(suggestedType));
            }

            _customerRepo.Update(customer);
            _customerRepo.SaveChanges();
        }

        private bool CompleteTripIfReady(string trackingNumber)
        {
            DeliveryTrip trip = FindActiveTripByOrder(trackingNumber);
            if (trip == null)
            {
                return false;
            }

            if (!AreAllTripOrdersTerminal(trip))
            {
                RecordDriverDelivery(trip.DriverID);
                return true;
            }

            trip.Complete();
            RecordDriverDelivery(trip.DriverID);
            ReleaseTripResources(trip);
            _tripRepo.Update(trip);
            _tripRepo.SaveChanges();
            return true;
        }

        private DeliveryTrip FindActiveTripByOrder(string trackingNumber)
        {
            List<DeliveryTrip> trips = _tripRepo.GetAll();
            for (int i = 0; i < trips.Count; i++)
            {
                DeliveryTrip trip = trips[i];
                if ((trip.Status == DeliveryTripStatus.Planned || trip.Status == DeliveryTripStatus.InProgress)
                    && trip.OrderIds != null
                    && trip.OrderIds.Contains(trackingNumber))
                {
                    return trip;
                }
            }

            return null!;
        }

        private bool AreAllTripOrdersTerminal(DeliveryTrip trip)
        {
            if (trip.OrderIds == null || trip.OrderIds.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < trip.OrderIds.Count; i++)
            {
                Order order = _orderRepo.GetById(trip.OrderIds[i]);
                if (order == null || !IsTerminalOrderStatus(order.CurrentStatus))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsTerminalOrderStatus(OrderStatus status)
        {
            return status == OrderStatus.Delivered
                || status == OrderStatus.Cancelled
                || status == OrderStatus.Returned
                || status == OrderStatus.Failed;
        }

        private void ReleaseSingleOrderResources(Order order)
        {
            if (order.AssignedDriverID != "")
            {
                Driver driver = _driverRepo.GetById(order.AssignedDriverID);
                if (driver != null)
                {
                    driver.CompleteDelivery();
                    _driverRepo.Update(driver);
                    _driverRepo.SaveChanges();
                }
            }

            if (order.AssignedVehicleID != "")
            {
                Vehicle vehicle = _vehicleRepo.GetById(order.AssignedVehicleID);
                if (vehicle != null)
                {
                    vehicle.UpdateStatus(VehicleStatus.Ready);
                    _vehicleRepo.Update(vehicle);
                    _vehicleRepo.SaveChanges();
                }
            }
        }

        private void RecordDriverDelivery(string driverId)
        {
            if (driverId != "")
            {
                Driver driver = _driverRepo.GetById(driverId);
                if (driver != null)
                {
                    driver.RecordDelivery();
                    _driverRepo.Update(driver);
                    _driverRepo.SaveChanges();
                }
            }
        }

        private void ReleaseTripResources(DeliveryTrip trip)
        {
            Vehicle vehicle = _vehicleRepo.GetById(trip.VehicleID);
            if (vehicle != null)
            {
                vehicle.UpdateStatus(VehicleStatus.Ready);
                _vehicleRepo.Update(vehicle);
                _vehicleRepo.SaveChanges();
            }

            Driver driver = _driverRepo.GetById(trip.DriverID);
            if (driver != null)
            {
                driver.UpdateDriverStatus(DriverStatus.Available);
                _driverRepo.Update(driver);
                _driverRepo.SaveChanges();
            }
        }

        // Lay danh sach tai xe san sang
        public List<Driver> GetAvailableDrivers()
        {
            if (!IsAdminOrDispatcher())
            {
                return new List<Driver>();
            }

            return _driverRepo.FindAvailableDrivers();
        }

        // Lay danh sach xe phu hop voi khoi luong
        public List<Vehicle> GetAvailableVehicles(double requiredWeight)
        {
            if (!IsAdminOrDispatcher())
            {
                return new List<Vehicle>();
            }

            return _vehicleRepo.FindByCapacity(requiredWeight);
        }

        // Lay danh sach tat ca xe san sang
        public List<Vehicle> GetAllAvailableVehicles()
        {
            if (!IsAdminOrDispatcher())
            {
                return new List<Vehicle>();
            }

            return _vehicleRepo.FindAvailableVehicles();
        }

        // Gan don hang cho xe kem kiem tra suc chua
        public bool AssignOrderToVehicle(string orderId, string vehicleId)
        {
            if (!IsAdminOrDispatcher())
            {
                return false;
            }

            Order order = _orderRepo.GetById(orderId);
            Vehicle vehicle = _vehicleRepo.GetById(vehicleId);

            if (order == null || vehicle == null)
            {
                return false;
            }

            double totalWeight = 0;
            double totalVolume = 0;
            
            if (order.Packages != null)
            {
                for (int i = 0; i < order.Packages.Count; i++)
                {
                    if (order.Packages[i] != null)
                    {
                        totalWeight += order.Packages[i].ActualWeight;
                        totalVolume += order.Packages[i].VolumeWeight;
                    }
                }
            }

            if (totalWeight > vehicle.MaxWeightCapacity)
            {
                throw new InsufficientCapacityException(
                    "Order weight " + totalWeight + "kg exceeds vehicle capacity " + vehicle.MaxWeightCapacity + "kg"
                );
            }

            if (totalVolume > vehicle.MaxVolumeCapacity)
            {
                throw new InsufficientCapacityException(
                    "Order volume " + totalVolume + "m3 exceeds vehicle capacity " + vehicle.MaxVolumeCapacity + "m3"
                );
            }

            if (!order.CanTransitionTo(OrderStatus.InTransit))
            {
                return false;
            }

            order.AssignVehicle(vehicleId);
            order.ChangeStatus(OrderStatus.InTransit, "Assigned to vehicle " + vehicle.VehicleID, "Dispatcher");
            vehicle.UpdateStatus(VehicleStatus.InTransit);
            
            _orderRepo.Update(order);
            _vehicleRepo.Update(vehicle);
            _orderRepo.SaveChanges();
            _vehicleRepo.SaveChanges();

            return true;
        }

        // Kiem tra thu xe con bao nhieu khong gian
        public bool CanAddOrderToVehicle(string vehicleId, Order newOrder)
        {
            if (!IsAdminOrDispatcher())
            {
                return false;
            }

            Vehicle vehicle = _vehicleRepo.GetById(vehicleId);
            if (vehicle == null || newOrder == null) return false;

            double usedWeight = 0;
            double usedVolume = 0;

            List<Order> allOrders = _orderRepo.GetAll();
            for (int k = 0; k < allOrders.Count; k++)
            {
                Order o = allOrders[k];
                if (o != null && o.AssignedVehicleID == vehicleId && o.CurrentStatus != OrderStatus.Delivered)
                {
                    if (o.Packages != null)
                    {
                        for (int p = 0; p < o.Packages.Count; p++)
                        {
                            if (o.Packages[p] != null)
                            {
                                usedWeight += o.Packages[p].ActualWeight;
                                usedVolume += o.Packages[p].VolumeWeight;
                            }
                        }
                    }
                }
            }

            double newWeight = 0;
            double newVolume = 0;
            if (newOrder.Packages != null)
            {
                for (int p = 0; p < newOrder.Packages.Count; p++)
                {
                    if (newOrder.Packages[p] != null)
                    {
                        newWeight += newOrder.Packages[p].ActualWeight;
                        newVolume += newOrder.Packages[p].VolumeWeight;
                    }
                }
            }

            bool canFitWeight = (usedWeight + newWeight) <= vehicle.MaxWeightCapacity;
            bool canFitVolume = (usedVolume + newVolume) <= vehicle.MaxVolumeCapacity;

            return canFitWeight && canFitVolume;
        }

        private static bool IsAdminOrDispatcher()
        {
            return SessionManager.CurrentUser != null &&
                   (SessionManager.CurrentUser.Role == UserRole.Admin ||
                    SessionManager.CurrentUser.Role == UserRole.Dispatcher);
        }

        private bool CanCompleteOrder(Order order)
        {
            if (IsAdminOrDispatcher())
            {
                return true;
            }

            User? user = SessionManager.CurrentUser;
            if (user == null || user.Role != UserRole.Driver)
            {
                return false;
            }

            string driverId = ResolveCurrentDriverId(user);
            return !string.IsNullOrWhiteSpace(driverId) && order.AssignedDriverID == driverId;
        }

        private string ResolveCurrentDriverId(User user)
        {
            if (user.Person is Driver driver)
            {
                return driver.StaffID;
            }

            List<Driver> drivers = _driverRepo.GetAll();
            for (int i = 0; i < drivers.Count; i++)
            {
                if (drivers[i].AccountID == user.Username || drivers[i].AccountID == user.UserId)
                {
                    return drivers[i].StaffID;
                }
            }

            return string.Empty;
        }
    }
}
