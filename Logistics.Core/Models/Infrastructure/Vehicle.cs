using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Interfaces;
using Newtonsoft.Json;

namespace Logistics.Core.Models.Infrastructure
{
    /// <summary>
    /// Mo hinh phuong tien van tai.
    /// Lop nay quan ly nang luc cho hang, nhien lieu, trang thai van hanh,
    /// lich su bao tri va dong co dang gan voi xe.
    /// </summary>
    public class Vehicle : ITrackable, ISerializable
    {
        [JsonProperty]
        public string VehicleID { get; private set; }
        [JsonProperty]
        public VehicleType VehicleType { get; private set; }
        public double MaxWeightCapacity { get; set; }
        public double MaxVolumeCapacity { get; set; }

        public void UpdateDetails(VehicleType type, double weight, double volume)
        {
            this.VehicleType = type;
            this.MaxWeightCapacity = weight;
            this.MaxVolumeCapacity = volume;
        }

        public void UpdateOperationalDetails(string dimensions, bool isRefrigerated, double fuelLevel, double currentOdometer)
        {
            Dimensions = dimensions;
            IsRefrigerated = isRefrigerated;
            UpdateFuelLevel(fuelLevel);
            if (currentOdometer >= 0)
            {
                CurrentOdometer = currentOdometer;
            }
        }
        [JsonProperty]
        public string Dimensions { get; private set; }
        [JsonProperty]
        public double CurrentOdometer { get; private set; }
        [JsonProperty]
        public bool IsRefrigerated { get; private set; }
        [JsonProperty]
        public double FuelLevel { get; private set; }
        [JsonProperty]
        public VehicleStatus Status { get; private set; }

        // Association: mot xe co the duoc phep van hanh boi nhieu tai xe.
        [JsonProperty]
        public List<Actors.Driver> AllowedDrivers { get; private set; }
        [JsonProperty]
        public List<MaintenanceLog> MaintenanceHistory { get; private set; }

        // Aggregation: Engine co the ton tai doc lap va duoc gan vao Vehicle.
        [JsonProperty]
        public Engine VehicleEngine { get; private set; }

        public Vehicle(string vehicleId, VehicleType vehicleType, double maxLoadWeight,
                       double cargoVolume, string dimensions, bool isRefrigerated)
        {
            VehicleID = vehicleId;
            VehicleType = vehicleType;
            MaxWeightCapacity = maxLoadWeight;
            MaxVolumeCapacity = cargoVolume;
            Dimensions = dimensions;
            IsRefrigerated = isRefrigerated;
            CurrentOdometer = 0;
            FuelLevel = 100;
            Status = VehicleStatus.Ready;
            AllowedDrivers = new List<Actors.Driver>();
            MaintenanceHistory = new List<MaintenanceLog>();
            VehicleEngine = new Engine();
        }

        public void AddAllowedDriver(Actors.Driver driver)
        {
            if (driver != null && !AllowedDrivers.Contains(driver))
            {
                AllowedDrivers.Add(driver);
            }
        }

        // Gan mot dong co da ton tai vao xe.
        public void InstallEngine(Engine engine)
        {
            VehicleEngine = engine;
        }

        // Constructor khong tham so bat buoc cho Newtonsoft.Json.
        public Vehicle() 
        { 
            VehicleID = string.Empty;
            Dimensions = string.Empty;
            AllowedDrivers = new List<Actors.Driver>();
            MaintenanceHistory = new List<MaintenanceLog>();
            VehicleEngine = new Engine("", "", 0);
        }

        // Constructor phuc vu ISerializable khi doc du lieu cu.
        protected Vehicle(SerializationInfo info, StreamingContext context)
        {
            VehicleID = info.GetString("VehicleID") ?? string.Empty;
            VehicleType = (VehicleType)info.GetValue("VehicleType", typeof(VehicleType))!;
            MaxWeightCapacity = info.GetDouble("MaxWeightCapacity");
            MaxVolumeCapacity = info.GetDouble("MaxVolumeCapacity");
            Dimensions = info.GetString("Dimensions") ?? string.Empty;
            CurrentOdometer = info.GetDouble("CurrentOdometer");
            IsRefrigerated = info.GetBoolean("IsRefrigerated");
            FuelLevel = info.GetDouble("FuelLevel");
            Status = (VehicleStatus)info.GetValue("Status", typeof(VehicleStatus))!;
            AllowedDrivers = (List<Actors.Driver>)info.GetValue("AllowedDrivers", typeof(List<Actors.Driver>))!;
            MaintenanceHistory = (List<MaintenanceLog>)(info.GetValue("MaintenanceHistory", typeof(List<MaintenanceLog>)) ?? new List<MaintenanceLog>());
            VehicleEngine = (Engine)info.GetValue("VehicleEngine", typeof(Engine))!;
        }

        // Ghi day du cac truong can thiet khi serialize bang ISerializable.
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("VehicleID", VehicleID);
            info.AddValue("VehicleType", VehicleType);
            info.AddValue("MaxWeightCapacity", MaxWeightCapacity);
            info.AddValue("MaxVolumeCapacity", MaxVolumeCapacity);
            info.AddValue("Dimensions", Dimensions);
            info.AddValue("CurrentOdometer", CurrentOdometer);
            info.AddValue("IsRefrigerated", IsRefrigerated);
            info.AddValue("FuelLevel", FuelLevel);
            info.AddValue("Status", Status);
            info.AddValue("AllowedDrivers", AllowedDrivers);
            info.AddValue("MaintenanceHistory", MaintenanceHistory);
            info.AddValue("VehicleEngine", VehicleEngine);
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

        public bool CanCarry(double weight, double volume = 0)
        {
            return weight <= MaxWeightCapacity && volume <= MaxVolumeCapacity && Status == VehicleStatus.Ready;
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

        public void AddMaintenanceLog(MaintenanceLog log)
        {
            if (log != null) MaintenanceHistory.Add(log);
        }

        public bool NeedsMaintenance()
        {
            if (MaintenanceHistory == null || MaintenanceHistory.Count == 0) return true;
            for (int i = 0; i < MaintenanceHistory.Count; i++)
            {
                if (MaintenanceHistory[i].IsDue()) return true;
            }
            return false;
        }

        public string GetVehicleInfo()
        {
            string refrigeratedText = "No";
            if (IsRefrigerated)
            {
                refrigeratedText = "Yes";
            }
            return "[Vehicle] ID: " + VehicleID + " | Type: " + VehicleType + "\n" +
                   "  Max Load: " + MaxWeightCapacity + "kg | Volume: " + MaxVolumeCapacity + "m3 | Dim: " + Dimensions + "\n" +
                   "  Odometer: " + CurrentOdometer + "km | Fuel: " + FuelLevel + "%\n" +
                   "  Refrigerated: " + refrigeratedText + " | Status: " + Status;
        }

        public override string ToString()
        {
            return GetVehicleInfo();
        }
    }
}

