using System.Collections.Generic;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Infrastructure;

namespace Logistics.Core.Services.Interfaces
{
    public interface IDispatchService
    {
        List<Vehicle> GetAvailableVehicles();

        bool AssignVehicleToOrder(string vehicleId, string orderId);

        bool AssignDispatch(string vehicleId, string driverId, string orderId);

        DeliveryTripDTO CreateTripDispatch(string vehicleId, string driverId, List<string> orderIds);

        List<DeliveryTripDTO> GetActiveTrips();

        bool CompleteTrip(string tripId);

        bool CancelTrip(string tripId);

        bool ReleaseVehicle(string vehicleId);
    }
}
