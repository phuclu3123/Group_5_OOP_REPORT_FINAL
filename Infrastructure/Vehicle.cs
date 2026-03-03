using System;
using System.Collections.Generic;
using System.Text;

namespace Group_OOP_FINAL.Infrastructure
{
    public abstract class Vehicle : ITrackable
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

        public Vehicle(string vehicleId, VehicleType vehicleType, double maxLoadWeight,
                       double cargoVolume, string dimensions, bool isRefrigerated)
        {
            VehicleID = vehicleId;
            VehicleType = vehicleType;
            MaxLoadWeight = maxLoadWeight;
            CargoVolume = cargoVolume;
            Dimensions = dimensions ?? "N/A";
            IsRefrigerated = isRefrigerated;
            CurrentOdometer = 0;
            FuelLevel = 100;
            Status = VehicleStatus.Ready; 
            
            if (string.IsNullOrWhiteSpace(vehicleId))
                throw new ArgumentException("VehicleID cannot be empty.");

            if (maxLoadWeight <= 0)
                throw new ArgumentException("MaxLoadWeight must be greater than 0.");

            if (cargoVolume <= 0)
                throw new ArgumentException("CargoVolume must be greater than 0.");
        }


        public Vehicle() { }

        // ===== ITrackable =====
        public string GetCurrentStatus() => Status.ToString();

        public string GetTrackingInfo()
        {
            string refrigeratedText = IsRefrigerated ? "Yes" : "No";

            return $"[Tracking Vehicle] {VehicleID} ({VehicleType})\n" +
                   $"  Status: {Status} | Fuel: {FuelLevel}%\n" +
                   $"  Odometer: {CurrentOdometer}km | Refrigerated: {refrigeratedText}";
        }

        // ===== BUSINESS METHODS =====

        public void UpdateStatus(VehicleStatus newStatus)
        {
            if (Status == VehicleStatus.Maintenance && newStatus == VehicleStatus.InTransit)
                throw new InvalidOperationException("Vehicle under maintenance cannot go to transit.");

            Status = newStatus;
        }

        public void UpdateFuelLevel(double newLevel)
        {
            FuelLevel = Math.Clamp(newLevel, 0, 100);
        }

        public void UpdateOdometer(double km)
        {
            if (km > 0)
                CurrentOdometer += km;
        }

        public bool CanCarry(double weight) =>
            weight <= MaxLoadWeight && Status == VehicleStatus.Ready;

        public bool IsAvailable() =>
            Status == VehicleStatus.Ready;

        public void SendToMaintenance() =>
            Status = VehicleStatus.Maintenance;

        public void CompleteMaintenance() =>
            Status = VehicleStatus.Ready;

        public string GetVehicleInfo()
        {
            string refrigeratedText = IsRefrigerated ? "Yes" : "No";

            return $"[Vehicle] ID: {VehicleID} | Type: {VehicleType}\n" +
                   $"  Max Load: {MaxLoadWeight}kg | Volume: {CargoVolume}m3 | Dim: {Dimensions}\n" +
                   $"  Odometer: {CurrentOdometer}km | Fuel: {FuelLevel}%\n" +
                   $"  Refrigerated: {refrigeratedText} | Status: {Status}";
        }
        public override string ToString() => GetVehicleInfo();
    }
}