using System;
using System.Collections.Generic;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Infrastructure;

namespace Logistics.Core.Services.Interfaces
{
    public interface IMaintenanceService
    {
        List<MaintenanceDTO> GetAllMaintenanceLogs();
        List<MaintenanceDTO> GetDueMaintenance();
        MaintenanceLog CreateMaintenanceLog(string vehicleId, DateTime serviceDate, decimal cost, string description, string serviceProvider, DateTime nextDueDate);
        bool AddMaintenanceLog(string vehicleId, MaintenanceLog log);
        bool SendVehicleToMaintenance(string vehicleId);
        bool CompleteMaintenance(string vehicleId);
        bool UpdateVehicleOdometer(string vehicleId, double kilometers);
    }
}
