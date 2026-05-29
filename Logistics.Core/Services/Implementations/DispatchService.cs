using Logistics.Core.Services.Interfaces;
using Logistics.Core.DTOs;
using Logistics.Core.DataAccess.Interfaces;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;
using System.Collections.Generic;

namespace Logistics.Core.Services.Implementations
{
    /// <summary>
    /// Manages vehicle dispatch assignments and scheduling.
    /// </summary>
    public class DispatchService : IDispatchService
    {
        private readonly VehicleRepository _vehicleRepository;
        private readonly OrderRepository _orderRepository;
        private readonly DriverRepository _driverRepository;
        private readonly DeliveryTripRepository _tripRepository;
        private readonly INotificationService _notificationService;

        public DispatchService(VehicleRepository vehicleRepository, OrderRepository orderRepository, DriverRepository driverRepository, DeliveryTripRepository tripRepository, INotificationService notificationService)
        {
            _vehicleRepository = vehicleRepository;
            _orderRepository = orderRepository;
            _driverRepository = driverRepository;
            _tripRepository = tripRepository;
            _notificationService = notificationService;
        }

        public List<Vehicle> GetAvailableVehicles()
        {
            List<Vehicle> all = _vehicleRepository.GetAll();
            List<Vehicle> available = new List<Vehicle>();
            foreach (Vehicle vehicle in all)
            {
                if (vehicle.IsAvailable())
                {
                    available.Add(vehicle);
                }
            }
            return available;
        }

        public bool AssignVehicleToOrder(string vehicleId, string orderId)
        {
            Vehicle vehicle = _vehicleRepository.GetById(vehicleId);
            if (vehicle == null || !vehicle.IsAvailable())
            {
                return false;
            }

            Order order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                return false;
            }

            if (!order.CanTransitionTo(OrderStatus.InTransit))
            {
                return false;
            }

            order.AssignVehicle(vehicleId);
            order.ChangeStatus(OrderStatus.InTransit, "Assigned vehicle " + vehicle.VehicleID, "Dispatcher");
            vehicle.UpdateStatus(VehicleStatus.InTransit);
            _vehicleRepository.Update(vehicle);
            _orderRepository.Update(order);
            _orderRepository.SaveChanges();
            _vehicleRepository.SaveChanges();

            _notificationService.Notify("Điều phối xe", $"Xe {vehicleId} đã rời bãi để giao đơn hàng {orderId}");
            return true;
        }

        public bool AssignDispatch(string vehicleId, string driverId, string orderId)
        {
            DeliveryTripDTO trip = CreateTripDispatch(vehicleId, driverId, new List<string> { orderId });
            return !string.IsNullOrWhiteSpace(trip.TripID);
        }

        public DeliveryTripDTO CreateTripDispatch(string vehicleId, string driverId, List<string> orderIds)
        {
            Vehicle vehicle = _vehicleRepository.GetById(vehicleId);
            Driver driver = _driverRepository.GetById(driverId);

            if (vehicle == null || driver == null || orderIds == null || orderIds.Count == 0)
            {
                return new DeliveryTripDTO();
            }

            if (!vehicle.IsAvailable() || !driver.IsAvailable())
            {
                return new DeliveryTripDTO();
            }

            // Kiem tra han che tai trong va the tich cua phuong tien
            double totalWeight = 0;
            double totalVolume = 0;
            for (int i = 0; i < orderIds.Count; i++)
            {
                Order o = _orderRepository.GetById(orderIds[i]);
                if (o != null)
                {
                    totalWeight += o.TotalWeight;
                    totalVolume += o.GetTotalVolume();
                }
            }

            if (totalWeight > vehicle.MaxWeightCapacity)
            {
                throw new System.InvalidOperationException("Khong the dieu phoi: Vuot qua tai trong cho phep cua xe " + vehicle.VehicleID + ".\nTai trong xe: " + vehicle.MaxWeightCapacity + "kg, Tong trong luong hang: " + totalWeight + "kg.");
            }
            if (totalVolume > vehicle.MaxVolumeCapacity)
            {
                throw new System.InvalidOperationException("Khong the dieu phoi: Vuot qua the tich cho phep cua xe " + vehicle.VehicleID + ".\nThe tich xe: " + vehicle.MaxVolumeCapacity + "m3, Tong the tich hang: " + totalVolume.ToString("F3") + "m3.");
            }

            VehicleStatus previousVehicleStatus = vehicle.Status;
            DriverStatus previousDriverStatus = driver.DriverStatus;
            List<Order> assignedOrders = new List<Order>();
            List<OrderStatus> previousOrderStatuses = new List<OrderStatus>();
            List<string> previousVehicleIds = new List<string>();
            List<string> previousDriverIds = new List<string>();

            try
            {
                string tripId = "TRIP" + System.DateTime.Now.ToString("yyyyMMdd") + (_tripRepository.Count() + 1).ToString("0000");
                DeliveryTrip trip = new DeliveryTrip(tripId, vehicleId, driverId);

                for (int i = 0; i < orderIds.Count; i++)
                {
                    Order order = _orderRepository.GetById(orderIds[i]);
                    if (order == null || !CanDispatchOrder(order))
                    {
                        throw new System.InvalidOperationException("Order is not ready for dispatch: " + orderIds[i]);
                    }

                    assignedOrders.Add(order);
                    previousOrderStatuses.Add(order.CurrentStatus);
                    previousVehicleIds.Add(order.AssignedVehicleID);
                    previousDriverIds.Add(order.AssignedDriverID);

                    order.AssignVehicle(vehicleId);
                    order.AssignDriver(driverId);
                    order.ChangeStatus(OrderStatus.InTransit, "Assigned to trip " + tripId, "Dispatcher");
                    trip.AddOrder(order.TrackingNumber);
                }

                trip.Start();
                vehicle.UpdateStatus(VehicleStatus.InTransit);
                driver.UpdateDriverStatus(DriverStatus.Busy);

                for (int i = 0; i < assignedOrders.Count; i++)
                {
                    _orderRepository.Update(assignedOrders[i]);
                }
                _vehicleRepository.Update(vehicle);
                _driverRepository.Update(driver);
                _tripRepository.Add(trip);
                _orderRepository.SaveChanges();
                _vehicleRepository.SaveChanges();
                _driverRepository.SaveChanges();

                _notificationService.Notify("Dieu phoi chuyen xe", "Da tao chuyen " + tripId + " cho " + orderIds.Count + " don hang.");
                return ToDTO(trip);
            }
            catch
            {
                for (int i = 0; i < assignedOrders.Count; i++)
                {
                    assignedOrders[i].AssignVehicle(previousVehicleIds[i]);
                    assignedOrders[i].AssignDriver(previousDriverIds[i]);
                    assignedOrders[i].ChangeStatus(previousOrderStatuses[i], "Rollback dispatch assignment", "System");
                    _orderRepository.Update(assignedOrders[i]);
                }
                vehicle.UpdateStatus(previousVehicleStatus);
                driver.UpdateDriverStatus(previousDriverStatus);

                _vehicleRepository.Update(vehicle);
                _driverRepository.Update(driver);
                _orderRepository.SaveChanges();
                _vehicleRepository.SaveChanges();
                _driverRepository.SaveChanges();
                return new DeliveryTripDTO();
            }
        }

        public List<DeliveryTripDTO> GetActiveTrips()
        {
            List<DeliveryTripDTO> result = new List<DeliveryTripDTO>();
            List<DeliveryTrip> trips = _tripRepository.GetAll();
            for (int i = 0; i < trips.Count; i++)
            {
                if (trips[i].Status == DeliveryTripStatus.Planned || trips[i].Status == DeliveryTripStatus.InProgress)
                {
                    result.Add(ToDTO(trips[i]));
                }
            }

            return result;
        }

        public bool CompleteTrip(string tripId)
        {
            DeliveryTrip trip = _tripRepository.GetById(tripId);
            if (trip == null || trip.Status != DeliveryTripStatus.InProgress)
            {
                return false;
            }

            if (!AreTripOrdersTerminal(trip))
            {
                return false;
            }

            trip.Complete();
            ReleaseTripResources(trip);
            _tripRepository.Update(trip);
            _tripRepository.SaveChanges();
            return true;
        }

        public bool CancelTrip(string tripId)
        {
            DeliveryTrip trip = _tripRepository.GetById(tripId);
            if (trip == null || (trip.Status != DeliveryTripStatus.Planned && trip.Status != DeliveryTripStatus.InProgress))
            {
                return false;
            }

            if (trip.OrderIds != null)
            {
                for (int i = 0; i < trip.OrderIds.Count; i++)
                {
                    Order order = _orderRepository.GetById(trip.OrderIds[i]);
                    if (order != null && order.CurrentStatus == OrderStatus.InTransit)
                    {
                        order.AssignVehicle(string.Empty);
                        order.AssignDriver(string.Empty);
                        order.ChangeStatus(OrderStatus.Pending, "Trip " + trip.TripID + " cancelled; waiting for new dispatch.", "Dispatcher");
                        _orderRepository.Update(order);
                    }
                }
            }

            trip.Cancel();
            ReleaseTripResources(trip);
            _tripRepository.Update(trip);
            _orderRepository.SaveChanges();
            _tripRepository.SaveChanges();
            return true;
        }

        private bool AreTripOrdersTerminal(DeliveryTrip trip)
        {
            if (trip.OrderIds == null || trip.OrderIds.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < trip.OrderIds.Count; i++)
            {
                Order order = _orderRepository.GetById(trip.OrderIds[i]);
                if (order == null || !IsTerminalOrderStatus(order.CurrentStatus))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CanDispatchOrder(Order order)
        {
            return order.CanTransitionTo(OrderStatus.InTransit) &&
                  (order.CurrentStatus == OrderStatus.Pending ||
                   order.CurrentStatus == OrderStatus.PickedUp ||
                   order.CurrentStatus == OrderStatus.ArrivedAtWarehouse ||
                   order.CurrentStatus == OrderStatus.Sorting ||
                   order.CurrentStatus == OrderStatus.ReadyForDispatch);
        }

        private static bool IsTerminalOrderStatus(OrderStatus status)
        {
            return status == OrderStatus.Delivered
                || status == OrderStatus.Cancelled
                || status == OrderStatus.Returned
                || status == OrderStatus.Failed;
        }

        private void ReleaseTripResources(DeliveryTrip trip)
        {
            Vehicle vehicle = _vehicleRepository.GetById(trip.VehicleID);
            if (vehicle != null)
            {
                vehicle.UpdateStatus(VehicleStatus.Ready);
                _vehicleRepository.Update(vehicle);
                _vehicleRepository.SaveChanges();
            }

            Driver driver = _driverRepository.GetById(trip.DriverID);
            if (driver != null)
            {
                driver.UpdateDriverStatus(DriverStatus.Available);
                _driverRepository.Update(driver);
                _driverRepository.SaveChanges();
            }
        }

        private static DeliveryTripDTO ToDTO(DeliveryTrip trip)
        {
            DeliveryTripDTO dto = new DeliveryTripDTO();
            dto.TripID = trip.TripID;
            dto.VehicleID = trip.VehicleID;
            dto.DriverID = trip.DriverID;
            dto.OrderCount = trip.OrderIds != null ? trip.OrderIds.Count : 0;
            dto.OrderList = trip.OrderIds != null ? string.Join(", ", trip.OrderIds) : string.Empty;
            dto.Status = trip.Status.ToString();
            dto.CreatedAt = trip.CreatedAt.ToString("dd/MM/yyyy HH:mm");
            return dto;
        }

        public bool ReleaseVehicle(string vehicleId)
        {
            Vehicle vehicle = _vehicleRepository.GetById(vehicleId);
            if (vehicle == null)
            {
                return false;
            }

            vehicle.UpdateStatus(VehicleStatus.Ready);
            _vehicleRepository.Update(vehicle);
            _vehicleRepository.SaveChanges();
            return true;
        }
    }
}
