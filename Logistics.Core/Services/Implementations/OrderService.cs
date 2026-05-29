using Logistics.Core.Services.Interfaces;
using Logistics.Core.Configuration;
using System;
using System.Collections.Generic;
using Logistics.Core.DataAccess.Interfaces;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Account;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Security;
using Logistics.Core.Utilities;
using Logistics.Core.Validations;

namespace Logistics.Core.Services.Implementations
{
    // Service quan ly vong doi don hang
    public class OrderService : IOrderService
    {
        private OrderRepository _orderRepo;
        private readonly CustomerRepository? _customerRepo;
        private readonly DriverRepository? _driverRepo;
        private readonly VehicleRepository? _vehicleRepo;
        private readonly BusinessRules _businessRules;
        private int _orderCounter;
        private int _packageCounter;

        public OrderService(OrderRepository orderRepo, CustomerRepository? customerRepo = null, DriverRepository? driverRepo = null, VehicleRepository? vehicleRepo = null, BusinessRules? businessRules = null)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
            _driverRepo = driverRepo;
            _vehicleRepo = vehicleRepo;
            _businessRules = businessRules ?? new BusinessRules();
            _orderCounter = _orderRepo.Count();
            _packageCounter = CountExistingPackages();
        }

        // Tao tracking number tu dong
        private string GenerateTrackingNumber()
        {
            _orderCounter++;
            string date = DateTime.Now.ToString("yyyyMMdd");
            string number = _orderCounter.ToString();
            while (number.Length < 4)
            {
                number = "0" + number;
            }
            return "TN" + date + number;
        }

        private int CountExistingPackages()
        {
            int count = 0;
            List<Order> orders = _orderRepo.GetAll();
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i] != null && orders[i].Packages != null)
                {
                    count += orders[i].Packages.Count;
                }
            }
            return count;
        }

        private string GeneratePackageId(string trackingNumber)
        {
            _packageCounter++;
            string number = _packageCounter.ToString();
            while (number.Length < 4)
            {
                number = "0" + number;
            }
            return trackingNumber + "-PKG" + number;
        }

        // Tao don hang moi
        public Order CreateOrder(string senderId, string receiverId, string pickupAddress,
                                 string deliveryAddress, ServiceType serviceType)
        {
            if (!CanCreateOrder(senderId, receiverId))
            {
                throw new UnauthorizedAccessException("Nguoi dung khong co quyen tao don hang.");
            }

            string trackingNumber = GenerateTrackingNumber();
            Order order = new Order(trackingNumber, senderId, receiverId,
                                    pickupAddress, deliveryAddress, serviceType);
            _orderRepo.Add(order);
            return order;
        }

        public Package CreatePackage(string trackingNumber, string description, double actualWeight, string dimensions, bool isFragile, decimal value, string itemCategory, string handlingInstructions)
        {
            string packageId = GeneratePackageId(trackingNumber);
            Package package = new Package(packageId, trackingNumber, description, actualWeight, dimensions, isFragile, value, itemCategory, handlingInstructions);
            return package;
        }

        // Them goi hang vao don
        public bool AddPackageToOrder(string trackingNumber, Package package)
        {
            if (!CanEditOrder(trackingNumber))
            {
                throw new UnauthorizedAccessException("Nguoi dung khong co quyen cap nhat kien hang.");
            }

            Order order = _orderRepo.GetById(trackingNumber);
            if (order != null)
            {
                PackageValidator validator = new PackageValidator();
                ValidationResult validation = validator.Validate(package);
                if (!validation.IsValid)
                {
                    return false;
                }

                if (order.Packages.Count >= AppConstants.MaxPackagesPerOrder)
                {
                    return false;
                }

                order.AddPackage(package);
                _orderRepo.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdatePackage(string trackingNumber, string packageId, string description, double actualWeight, string dimensions, bool isFragile, decimal value, string itemCategory, string handlingInstructions)
        {
            if (!CanEditOrder(trackingNumber))
            {
                throw new UnauthorizedAccessException("Nguoi dung khong co quyen cap nhat kien hang.");
            }

            Order order = _orderRepo.GetById(trackingNumber);
            if (order == null)
            {
                return false;
            }

            Package? package = order.FindPackage(packageId);
            if (package == null)
            {
                return false;
            }

            Package updatedPackage = new Package(package.PackageID, package.OrderID, description, actualWeight, dimensions, isFragile, value, itemCategory, handlingInstructions);
            PackageValidator validator = new PackageValidator();
            ValidationResult validation = validator.Validate(updatedPackage);
            if (!validation.IsValid)
            {
                return false;
            }

            package.UpdatePackageInfo(description, actualWeight, dimensions, isFragile, value, itemCategory, handlingInstructions);
            order.CalculateTotalWeight();
            _orderRepo.SaveChanges();
            return true;
        }

        public bool RemovePackageFromOrder(string trackingNumber, string packageId)
        {
            if (!CanEditOrder(trackingNumber))
            {
                throw new UnauthorizedAccessException("Nguoi dung khong co quyen xoa kien hang.");
            }

            Order order = _orderRepo.GetById(trackingNumber);
            if (order == null)
            {
                return false;
            }

            bool removed = order.RemovePackage(packageId);
            if (removed)
            {
                _orderRepo.SaveChanges();
            }
            return removed;
        }

        // Cap nhat trang thai don hang
        public bool UpdateOrderStatus(string trackingNumber, OrderStatus newStatus)
        {
            Order order = _orderRepo.GetById(trackingNumber);
            if (order != null)
            {
                if (!CanUpdateStatus(order, newStatus))
                {
                    throw new UnauthorizedAccessException("Nguoi dung khong co quyen cap nhat trang thai don hang.");
                }

                if (!CanTransition(order.CurrentStatus, newStatus))
                {
                    return false;
                }

                string oldDriverId = order.AssignedDriverID;
                string oldVehicleId = order.AssignedVehicleID;

                order.ChangeStatus(newStatus, "Cap nhat trang thai tu ban dieu khien", "Dispatcher");

                // Giai phong tai xe va xe khi don thoat khoi luong van chuyen dang chay.
                if (ShouldReleaseResources(order.CurrentStatus))
                {
                    ReleaseResources(oldDriverId, oldVehicleId);
                    order.AssignDriver("");
                    order.AssignVehicle("");
                }

                _orderRepo.Update(order);
                _orderRepo.SaveChanges();
                return true;
            }
            return false;
        }

        private static bool CanTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {
            return Order.CanTransition(currentStatus, newStatus);
        }

        private static bool ShouldReleaseResources(OrderStatus status)
        {
            return status == OrderStatus.Pending ||
                   status == OrderStatus.ReadyForDispatch ||
                   status == OrderStatus.Delivered ||
                   status == OrderStatus.Cancelled ||
                   status == OrderStatus.Failed ||
                   status == OrderStatus.Returned;
        }

        private void ReleaseResources(string driverId, string vehicleId)
        {
            if (!string.IsNullOrEmpty(driverId) && _driverRepo != null)
            {
                Driver driver = _driverRepo.GetById(driverId);
                if (driver != null)
                {
                    driver.UpdateDriverStatus(DriverStatus.Available);
                    _driverRepo.Update(driver);
                    _driverRepo.SaveChanges();
                }
            }

            if (!string.IsNullOrEmpty(vehicleId) && _vehicleRepo != null)
            {
                Vehicle vehicle = _vehicleRepo.GetById(vehicleId);
                if (vehicle != null)
                {
                    vehicle.UpdateStatus(VehicleStatus.Ready);
                    _vehicleRepo.Update(vehicle);
                    _vehicleRepo.SaveChanges();
                }
            }
        }

        // Huy don hang
        public bool CancelOrder(string trackingNumber)
        {
            Order order = _orderRepo.GetById(trackingNumber);
            if (order != null && order.CurrentStatus == OrderStatus.Pending)
            {
                if (!CanCancelOrder(order))
                {
                    throw new UnauthorizedAccessException("Nguoi dung khong co quyen huy don hang.");
                }

                order.ChangeStatus(OrderStatus.Cancelled);
                _orderRepo.SaveChanges();
                return true;
            }
            return false;
        }

        // Tinh chi phi don hang
        public decimal CalculateOrderCost(string trackingNumber)
        {
            return CalculateOrderCost(trackingNumber, 0m);
        }

        public decimal CalculateOrderCost(string trackingNumber, decimal costPerKg)
        {
            Order order = _orderRepo.GetById(trackingNumber);
            if (order != null && IsOrderVisibleToCurrentUser(order))
            {
                IShippingFeeStrategy strategy;
                if (order.ServiceType == ServiceType.Express || order.ServiceType == ServiceType.Instant)
                {
                    strategy = new ExpressShippingFeeStrategy(_businessRules);
                }
                else
                {
                    strategy = new StandardShippingFeeStrategy();
                }

                decimal ratePerKg = costPerKg > 0 ? costPerKg : _businessRules.GetRatePerKg(order.ServiceType);
                order.CalculateTotalCost(strategy, ratePerKg);
                _orderRepo.SaveChanges();
                return order.TotalCost;
            }
            return 0;
        }

        public List<Package> GetPackagesByOrder(string trackingNumber)
        {
            Order order = _orderRepo.GetById(trackingNumber);
            if (order == null || order.Packages == null || !IsOrderVisibleToCurrentUser(order))
            {
                return new List<Package>();
            }

            return order.Packages;
        }

        // Lay don hang theo ID / Tracking number
        public Order GetOrderById(string trackingNumber)
        {
            Order order = _orderRepo.GetById(trackingNumber);
            if (order == null || !IsOrderVisibleToCurrentUser(order))
            {
                return null!;
            }

            return order;
        }

        // Lay don hang theo khach gui
        public List<Order> GetOrdersByCustomer(string customerId)
        {
            List<Order> bySender = _orderRepo.FindBySender(customerId);
            List<Order> byReceiver = _orderRepo.FindByReceiver(customerId);

            // Gop 2 list, tranh trung lap
            List<Order> result = new List<Order>();
            for (int i = 0; i < bySender.Count; i++)
            {
                result.Add(bySender[i]);
            }
            for (int i = 0; i < byReceiver.Count; i++)
            {
                bool isDuplicate = false;
                for (int j = 0; j < result.Count; j++)
                {
                    if (result[j].TrackingNumber == byReceiver[i].TrackingNumber)
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                if (!isDuplicate)
                {
                    result.Add(byReceiver[i]);
                }
            }
            return FilterVisibleOrders(result);
        }

        public List<Order> GetSentOrdersForCurrentCustomer()
        {
            return GetCurrentCustomerOrders(true);
        }

        public List<Order> GetReceivedOrdersForCurrentCustomer()
        {
            return GetCurrentCustomerOrders(false);
        }

        // Lay don hang theo trang thai
        public List<Order> GetOrdersByStatus(OrderStatus status)
        {
            return FilterVisibleOrders(_orderRepo.FindByStatus(status));
        }

        // Lay tat ca don hang
        public List<Order> GetAllOrders()
        {
            return FilterVisibleOrders(_orderRepo.GetAll());
        }

        private List<Order> FilterVisibleOrders(List<Order> orders)
        {
            List<Order> visible = new List<Order>();
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i] != null && IsOrderVisibleToCurrentUser(orders[i]))
                {
                    visible.Add(orders[i]);
                }
            }

            return visible;
        }

        private bool IsOrderVisibleToCurrentUser(Order order)
        {
            User? user = SessionManager.CurrentUser;
            if (user == null)
            {
                return false;
            }

            if (user.Role == UserRole.Admin || user.Role == UserRole.Dispatcher)
            {
                return true;
            }

            if (user.Role == UserRole.Driver)
            {
                string driverId = ResolveCurrentDriverId(user);
                return !string.IsNullOrWhiteSpace(driverId) && order.AssignedDriverID == driverId;
            }

            if (user.Role == UserRole.Customer)
            {
                List<string> customerIds = ResolveCurrentCustomerIds(user);
                for (int i = 0; i < customerIds.Count; i++)
                {
                    if (order.SenderID == customerIds[i] || order.ReceiverID == customerIds[i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CanCreateOrder(string senderId, string receiverId)
        {
            User? user = SessionManager.CurrentUser;
            if (user == null)
            {
                return false;
            }

            if (user.Role == UserRole.Admin || user.Role == UserRole.Dispatcher)
            {
                return true;
            }

            if (user.Role == UserRole.Customer)
            {
                List<string> customerIds = ResolveCurrentCustomerIds(user);
                for (int i = 0; i < customerIds.Count; i++)
                {
                    if (senderId == customerIds[i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private List<Order> GetCurrentCustomerOrders(bool sent)
        {
            User? user = SessionManager.CurrentUser;
            if (user == null || user.Role != UserRole.Customer)
            {
                return new List<Order>();
            }

            List<string> customerIds = ResolveCurrentCustomerIds(user);
            List<Order> result = new List<Order>();
            for (int i = 0; i < customerIds.Count; i++)
            {
                List<Order> customerOrders = sent
                    ? _orderRepo.FindBySender(customerIds[i])
                    : _orderRepo.FindByReceiver(customerIds[i]);

                for (int j = 0; j < customerOrders.Count; j++)
                {
                    if (customerOrders[j] == null || !IsOrderVisibleToCurrentUser(customerOrders[j]))
                    {
                        continue;
                    }

                    bool exists = false;
                    for (int k = 0; k < result.Count; k++)
                    {
                        if (result[k].TrackingNumber == customerOrders[j].TrackingNumber)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        result.Add(customerOrders[j]);
                    }
                }
            }

            return result;
        }

        private bool CanEditOrder(string trackingNumber)
        {
            Order order = _orderRepo.GetById(trackingNumber);
            if (order == null)
            {
                return false;
            }

            User? user = SessionManager.CurrentUser;
            if (user == null)
            {
                return false;
            }

            if (user.Role == UserRole.Admin || user.Role == UserRole.Dispatcher)
            {
                return true;
            }

            if (user.Role == UserRole.Customer && order.CurrentStatus == OrderStatus.Pending)
            {
                return IsCurrentCustomerSender(order, user);
            }

            return false;
        }

        private bool CanUpdateStatus(Order order, OrderStatus newStatus)
        {
            User? user = SessionManager.CurrentUser;
            if (user == null)
            {
                return false;
            }

            if (user.Role == UserRole.Admin || user.Role == UserRole.Dispatcher)
            {
                return true;
            }

            if (user.Role == UserRole.Driver && IsOrderVisibleToCurrentUser(order))
            {
                return newStatus == OrderStatus.Delivered ||
                       newStatus == OrderStatus.DeliveryAttemptFailed ||
                       newStatus == OrderStatus.Returned;
            }

            return false;
        }

        private bool CanCancelOrder(Order order)
        {
            User? user = SessionManager.CurrentUser;
            if (user == null)
            {
                return false;
            }

            if (user.Role == UserRole.Admin || user.Role == UserRole.Dispatcher)
            {
                return true;
            }

            return user.Role == UserRole.Customer && IsCurrentCustomerSender(order, user);
        }

        private bool IsCurrentCustomerSender(Order order, User user)
        {
            List<string> customerIds = ResolveCurrentCustomerIds(user);
            for (int i = 0; i < customerIds.Count; i++)
            {
                if (order.SenderID == customerIds[i])
                {
                    return true;
                }
            }

            return false;
        }

        private List<string> ResolveCurrentCustomerIds(User user)
        {
            List<string> ids = new List<string>();
            if (user.Person is Customer customer)
            {
                ids.Add(customer.Id);
            }

            if (_customerRepo != null)
            {
                List<Customer> customers = _customerRepo.GetAll();
                for (int i = 0; i < customers.Count; i++)
                {
                    if (customers[i].AccountID == user.Username || customers[i].AccountID == user.UserId)
                    {
                        ids.Add(customers[i].Id);
                    }
                }
            }

            if (ids.Count == 0 && user.Username.StartsWith("customer", StringComparison.OrdinalIgnoreCase))
            {
                ids.Add("CUST001");
            }

            return ids;
        }

        private string ResolveCurrentDriverId(User user)
        {
            if (user.Person is Driver driver)
            {
                return driver.StaffID;
            }

            if (_driverRepo != null)
            {
                List<Driver> drivers = _driverRepo.GetAll();
                for (int i = 0; i < drivers.Count; i++)
                {
                    if (drivers[i].AccountID == user.Username || drivers[i].AccountID == user.UserId)
                    {
                        return drivers[i].StaffID;
                    }
                }
            }

            return string.Empty;
        }
    }
}
