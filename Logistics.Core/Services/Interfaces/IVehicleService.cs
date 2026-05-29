using System.Collections.Generic;
using Logistics.Core.Models.Infrastructure;

namespace Logistics.Core.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý phương tiện vận tải (BUS).
    /// </summary>
    public interface IVehicleService
    {
        List<Vehicle> GetAllVehicles();
        Vehicle GetVehicleById(string vehicleId);
        bool AddVehicle(Vehicle vehicle);
        bool UpdateVehicle(Vehicle vehicle);
        bool DeleteVehicle(string vehicleId);
    }
}
