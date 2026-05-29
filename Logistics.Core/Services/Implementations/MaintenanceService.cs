using System;
using System.Collections.Generic;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;
using Logistics.Core.Security;
using Logistics.Core.Services.Interfaces;

namespace Logistics.Core.Services.Implementations
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly VehicleRepository _vehicleRepository;
        private int _maintenanceCounter;

        public MaintenanceService(VehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
            _maintenanceCounter = CountExistingLogs();
        }

        private int CountExistingLogs()
        {
            int count = 0;
            List<Vehicle> vehicles = _vehicleRepository.GetAll();
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i] != null && vehicles[i].MaintenanceHistory != null)
                {
                    count += vehicles[i].MaintenanceHistory.Count;
                }
            }
            return count;
        }

        private string GenerateLogId()
        {
            _maintenanceCounter++;
            string number = _maintenanceCounter.ToString();
            while (number.Length < 4)
            {
                number = "0" + number;
            }
            return "MT" + DateTime.Now.ToString("yyyyMMdd") + number;
        }

        public List<MaintenanceDTO> GetAllMaintenanceLogs()
        {
            List<MaintenanceDTO> result = new List<MaintenanceDTO>();
            List<Vehicle> vehicles = _vehicleRepository.GetAll();
            for (int i = 0; i < vehicles.Count; i++)
            {
                Vehicle vehicle = vehicles[i];
                if (vehicle == null || vehicle.MaintenanceHistory == null)
                {
                    continue;
                }

                for (int j = 0; j < vehicle.MaintenanceHistory.Count; j++)
                {
                    MaintenanceLog log = vehicle.MaintenanceHistory[j];
                    result.Add(CreateDTO(vehicle, log));
                }
            }
            return result;
        }

        public List<MaintenanceDTO> GetDueMaintenance()
        {
            List<MaintenanceDTO> allLogs = GetAllMaintenanceLogs();
            List<MaintenanceDTO> result = new List<MaintenanceDTO>();
            for (int i = 0; i < allLogs.Count; i++)
            {
                if (allLogs[i].IsDue)
                {
                    result.Add(allLogs[i]);
                }
            }
            return result;
        }

        public MaintenanceLog CreateMaintenanceLog(string vehicleId, DateTime serviceDate, decimal cost, string description, string serviceProvider, DateTime nextDueDate)
        {
            return new MaintenanceLog(GenerateLogId(), vehicleId, serviceDate, cost, description, serviceProvider, nextDueDate);
        }

        public bool AddMaintenanceLog(string vehicleId, MaintenanceLog log)
        {
            if (!RoleGuard.CanManageVehicles())
            {
                throw new UnauthorizedAccessException("Nguoi dung khong co quyen cap nhat bao tri xe.");
            }

            Vehicle vehicle = _vehicleRepository.GetById(vehicleId);
            if (vehicle == null || log == null)
            {
                return false;
            }

            vehicle.AddMaintenanceLog(log);
            if (vehicle.Status == VehicleStatus.Maintenance)
            {
                vehicle.CompleteMaintenance();
            }
            _vehicleRepository.Update(vehicle);
            return true;
        }

        public bool SendVehicleToMaintenance(string vehicleId)
        {
            if (!RoleGuard.CanManageVehicles())
            {
                throw new UnauthorizedAccessException("Nguoi dung khong co quyen dua xe vao bao tri.");
            }

            Vehicle vehicle = _vehicleRepository.GetById(vehicleId);
            if (vehicle == null)
            {
                return false;
            }

            vehicle.SendToMaintenance();
            _vehicleRepository.Update(vehicle);
            return true;
        }

        public bool CompleteMaintenance(string vehicleId)
        {
            if (!RoleGuard.CanManageVehicles())
            {
                throw new UnauthorizedAccessException("Nguoi dung khong co quyen hoan tat bao tri.");
            }

            Vehicle vehicle = _vehicleRepository.GetById(vehicleId);
            if (vehicle == null)
            {
                return false;
            }

            vehicle.CompleteMaintenance();
            _vehicleRepository.Update(vehicle);
            return true;
        }

        public bool UpdateVehicleOdometer(string vehicleId, double kilometers)
        {
            if (!RoleGuard.CanManageVehicles())
            {
                throw new UnauthorizedAccessException("Nguoi dung khong co quyen cap nhat odometer.");
            }

            Vehicle vehicle = _vehicleRepository.GetById(vehicleId);
            if (vehicle == null || kilometers <= 0)
            {
                return false;
            }

            vehicle.UpdateOdometer(kilometers);
            _vehicleRepository.Update(vehicle);
            return true;
        }

        private MaintenanceDTO CreateDTO(Vehicle vehicle, MaintenanceLog log)
        {
            MaintenanceDTO dto = new MaintenanceDTO();
            dto.VehicleId = vehicle.VehicleID;
            dto.LogId = log.LogID;
            dto.ServiceDate = log.ServiceDate;
            dto.Description = log.Description;
            dto.ServiceProvider = log.ServiceProvider;
            dto.Cost = log.Cost;
            dto.NextDueDate = log.NextDueDate;
            dto.IsDue = log.IsDue();
            dto.VehicleStatus = vehicle.Status.ToString();
            return dto;
        }
    }
}
