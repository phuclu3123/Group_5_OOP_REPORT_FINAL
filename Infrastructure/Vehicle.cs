using System;
using Cuoi_ky_OOP.Models.Common;
using Cuoi_ky_OOP.Models.Interfaces;

namespace Cuoi_ky_OOP.Models.Infrastructure
{
    public class Vehicle : ITrackable
    {
        public string VehicleID { get; private set; }
        public VehicleType VehicleType { get; private set; }
        public double MaxLoadWeight { get; private set; }
        public double CargoVolume { get; private set; }
        public string Dimensions { get; private set; }
        public double CurrentOdometer { get; private set; }
        public bool IsRefrigerated { get; private set; }
        public double FuelLevel { get; private set; }
        public VehicleStatus Status { get; private set; }

        // Association: 1 Vehicle co the duoc phan cong cho nhieu Driver lai (1..*)
        public List<Actors.Driver> AllowedDrivers { get; private set; }

        // Aggregation: Whole-part relationship. Part can exist without Whole. (Engine can exist independently of Vehicle)
        public Engine VehicleEngine { get; private set; }

        public Vehicle(string vehicleId, VehicleType vehicleType, double maxLoadWeight,
                       double cargoVolume, string dimensions, bool isRefrigerated)
        {
            VehicleID = vehicleId;
            VehicleType = vehicleType;
            MaxLoadWeight = maxLoadWeight;
            CargoVolume = cargoVolume;
            Dimensions = dimensions;
            IsRefrigerated = isRefrigerated;
            CurrentOdometer = 0;
            FuelLevel = 100;
            Status = VehicleStatus.Ready;
            AllowedDrivers = new List<Actors.Driver>();
        }

        public void AddAllowedDriver(Actors.Driver driver)
        {
            if (driver != null && !AllowedDrivers.Contains(driver))
            {
                AllowedDrivers.Add(driver);
            }
        }

        // Method to demonstrate Aggregation: attaching an existing part to the whole
        public void InstallEngine(Engine engine)
        {
            VehicleEngine = engine;
        }

        // Constructor khong tham so cho XML serialization
        public Vehicle()
        {
            VehicleID = null!;
            Dimensions = null!;
        }

        // ===== ITrackable =====
        public string GetCurrentStatus()
        {
            return Status.ToString();
        }

        public string GetTrackingInfo()
        {
            string refrigeratedText = "No";
            if (IsRefrigerated)
            {
                refrigeratedText = "Yes";
            }
            return "[Tracking Vehicle] " + VehicleID + " (" + VehicleType + ")\n" +
                   "  Status: " + Status + " | Fuel: " + FuelLevel + "%" + "\n" +
                   "  Odometer: " + CurrentOdometer + "km | Refrigerated: " + refrigeratedText;
        }

        // ===== VEHICLE METHODS =====

        public void UpdateStatus(VehicleStatus newStatus)
        {
            Status = newStatus;
        }

        public void UpdateFuelLevel(double newLevel)
        {
            if (newLevel < 0)
            {
                FuelLevel = 0;
            }
            else if (newLevel > 100)
            {
                FuelLevel = 100;
            }
            else
            {
                FuelLevel = newLevel;
            }
        }

        public void UpdateOdometer(double km)
        {
            if (km > 0)
            {
                CurrentOdometer += km;
            }
        }

        public bool CanCarry(double weight)
        {
            return weight <= MaxLoadWeight && Status == VehicleStatus.Ready;
        }

        public bool IsAvailable()
        {
            return Status == VehicleStatus.Ready;
        }

        public void SendToMaintenance()
        {
            Status = VehicleStatus.Maintenance;
        }

        public void CompleteMaintenance()
        {
            Status = VehicleStatus.Ready;
        }

        public string GetVehicleInfo()
        {
            string refrigeratedText = "No";
            if (IsRefrigerated)
            {
                refrigeratedText = "Yes";
            }
            return "[Vehicle] ID: " + VehicleID + " | Type: " + VehicleType + "\n" +
                   "  Max Load: " + MaxLoadWeight + "kg | Volume: " + CargoVolume + "m3 | Dim: " + Dimensions + "\n" +
                   "  Odometer: " + CurrentOdometer + "km | Fuel: " + FuelLevel + "%\n" +
                   "  Refrigerated: " + refrigeratedText + " | Status: " + Status;
        }

        public override string ToString()
        {
            return GetVehicleInfo();
        }
    }
}
