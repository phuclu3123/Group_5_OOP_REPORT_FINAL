using System.Collections.Generic;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Infrastructure;

namespace Logistics.Core.Services.Interfaces
{
    public interface IDeliveryService
    {
        bool AssignDriver(string trackingNumber, string driverId);
        bool CompleteDelivery(string trackingNumber);
        List<Driver> GetAvailableDrivers();
        List<Vehicle> GetAvailableVehicles(double requiredWeight);
        List<Vehicle> GetAllAvailableVehicles();
        bool AssignOrderToVehicle(string orderId, string vehicleId);
        bool CanAddOrderToVehicle(string vehicleId, Order newOrder);
    }
}