using Logistics.Core.Services.Interfaces;
using System.Collections.Generic;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Infrastructure;

namespace Logistics.Core.Services.Implementations
{
    public class RouteOptimizationService : IRouteOptimizationService
    {
        private DriverRepository _driverRepo;
        private VehicleRepository _vehicleRepo;

        public RouteOptimizationService(DriverRepository driverRepo, VehicleRepository vehicleRepo)
        {
            _driverRepo = driverRepo;
            _vehicleRepo = vehicleRepo;
        }

        // Goi y cap Tai xe + Xe phu hop cho 1 Don hang
        public string SuggestDriverAndVehicle(Order order)
        {
            if (order == null) return "Don hang khong ton tai.";
            
            List<Driver> availableDrivers = _driverRepo.FindAvailableDrivers();
            List<Vehicle> availableVehicles = _vehicleRepo.FindAvailableVehicles();

            double requiredWeight = order.TotalWeight;
            double requiredVolume = order.GetTotalVolume();
            bool requiresRefrigeration = RequiresRefrigeration(order);
            
            // Chon xe nho nhat dap ung tai trong, the tich, nhien lieu va yeu cau xe lanh.
            Vehicle? selectedVehicle = null;
            for (int i = 0; i < availableVehicles.Count; i++)
            {
                Vehicle candidate = availableVehicles[i];
                if (!candidate.CanCarry(requiredWeight, requiredVolume))
                {
                    continue;
                }

                if (candidate.FuelLevel <= AppConstants.LowFuelThreshold)
                {
                    continue;
                }

                if (requiresRefrigeration && !candidate.IsRefrigerated)
                {
                    continue;
                }

                if (selectedVehicle == null ||
                    candidate.MaxWeightCapacity < selectedVehicle.MaxWeightCapacity ||
                    (candidate.MaxWeightCapacity == selectedVehicle.MaxWeightCapacity && candidate.MaxVolumeCapacity < selectedVehicle.MaxVolumeCapacity))
                {
                    selectedVehicle = candidate;
                }
            }

            if (selectedVehicle == null)
            {
                return "Khong tim thay xe phu hop cho don " + order.TrackingNumber +
                       ". Can " + requiredWeight.ToString("N2") + "kg, " + requiredVolume.ToString("N3") + "m3" +
                       (requiresRefrigeration ? ", yeu cau xe lanh." : ".");
            }

            // Chon tai xe co bang lai phu hop va con hieu luc.
            Driver? selectedDriver = null;
            for (int i = 0; i < availableDrivers.Count; i++)
            {
                Driver driver = availableDrivers[i];
                if (driver.IsLicenseValid() && CanDriveVehicle(driver, selectedVehicle))
                {
                    selectedDriver = driver;
                    break;
                }
            }

            if (selectedDriver == null)
            {
                return "Khong tim thay tai xe phu hop cho xe " + selectedVehicle.VehicleID;
            }

            return "De xuat dieu phoi: Giao don " + order.TrackingNumber + 
                   " cho Tai xe " + selectedDriver.FullName + " (" + selectedDriver.StaffID + ")" +
                   " lai xe " + selectedVehicle.VehicleID +
                   ". Tai trong " + requiredWeight.ToString("N2") + "kg, the tich " + requiredVolume.ToString("N3") + "m3.";
        }

        private static bool CanDriveVehicle(Driver driver, Vehicle vehicle)
        {
            string licenseType = driver.LicenseType == null ? string.Empty : driver.LicenseType.ToUpperInvariant();

            if (vehicle.VehicleType == VehicleType.Motorbike)
            {
                return licenseType == "A1" || licenseType == "A2" || licenseType == "B1" || licenseType == "B2" || licenseType == "C";
            }

            if (vehicle.VehicleType == VehicleType.Van || vehicle.VehicleType == VehicleType.Truck_1Ton || vehicle.VehicleType == VehicleType.ColdStorageTruck)
            {
                return licenseType == "B2" || licenseType == "C";
            }

            if (vehicle.VehicleType == VehicleType.Container_40ft)
            {
                return licenseType == "C";
            }

            return false;
        }

        private static bool RequiresRefrigeration(Order order)
        {
            if (order.Packages == null)
            {
                return false;
            }

            for (int i = 0; i < order.Packages.Count; i++)
            {
                Package package = order.Packages[i];
                if (package == null)
                {
                    continue;
                }

                string category = package.ItemCategory == null ? string.Empty : package.ItemCategory.ToLowerInvariant();
                string handling = package.HandlingInstructions == null ? string.Empty : package.HandlingInstructions.ToLowerInvariant();
                if (category.Contains("thuc pham") ||
                    category.Contains("y te") ||
                    category.Contains("lanh") ||
                    handling.Contains("giu mat") ||
                    handling.Contains("cold") ||
                    handling.Contains("refriger"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
