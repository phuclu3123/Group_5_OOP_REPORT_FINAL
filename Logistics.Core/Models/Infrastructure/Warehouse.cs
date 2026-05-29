using System;
using System.Runtime.Serialization;
using Logistics.Core.Models.Common;
using Logistics.Core.Models.Interfaces;
using Logistics.Core.Models.Actors;
using Newtonsoft.Json;

namespace Logistics.Core.Models.Infrastructure
{
    public class Warehouse : IReportable, ITrackable, ISerializable
    {
        [JsonProperty("WarehouseID")]
        public string WarehouseID { get; private set; }
        
        [JsonProperty("Name")]
        public string Name { get; private set; }
        
        [JsonProperty("Address")]
        public string Address { get; private set; }
        
        [JsonProperty("Coordinate")]
        public GeoPoint Coordinate { get; private set; }
        
        [JsonProperty("Type")]
        public WarehouseType Type { get; private set; }
        
        [JsonProperty("TotalCapacity")]
        public double TotalCapacity { get; private set; }
        
        [JsonProperty("UsedCapacity")]
        public double UsedCapacity { get; private set; }
        
        [JsonProperty("OperatingHours")]
        public string OperatingHours { get; private set; }
        
        // Association: Tham chieu kieu doi tuong (Reference object) de the hien lien ket thay vi chi luu chuoi ID
        [JsonProperty("Manager")]
        public WarehouseStaff? Manager { get; private set; }

        [JsonConstructor]
        public Warehouse(string WarehouseID, string Name, string Address, GeoPoint Coordinate,
                         WarehouseType Type, double TotalCapacity, string OperatingHours, WarehouseStaff? Manager)
        {
            this.WarehouseID = WarehouseID;
            this.Name = Name;
            this.Address = Address;
            this.Coordinate = Coordinate;
            this.Type = Type;
            this.TotalCapacity = TotalCapacity;
            this.UsedCapacity = 0;
            this.OperatingHours = OperatingHours;
            this.Manager = Manager;
        }

        // Constructor khong tham so cho JSON serialization
        public Warehouse() 
        { 
            WarehouseID = string.Empty;
            Name = string.Empty;
            Address = string.Empty;
            Coordinate = default;
            OperatingHours = string.Empty;
            Manager = null;
        }

        // Constructor cho ISerializable (Deserialization)
        protected Warehouse(SerializationInfo info, StreamingContext context)
        {
            WarehouseID = info.GetString("WarehouseID") ?? string.Empty;
            Name = info.GetString("Name") ?? string.Empty;
            Address = info.GetString("Address") ?? string.Empty;
            Coordinate = (GeoPoint)info.GetValue("Coordinate", typeof(GeoPoint))!;
            Type = (WarehouseType)info.GetValue("Type", typeof(WarehouseType))!;
            TotalCapacity = info.GetDouble("TotalCapacity");
            UsedCapacity = info.GetDouble("UsedCapacity");
            OperatingHours = info.GetString("OperatingHours") ?? string.Empty;
            Manager = (WarehouseStaff?)info.GetValue("Manager", typeof(WarehouseStaff));
        }

        // Phuong thuc ISerializable (Serialization)
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("WarehouseID", WarehouseID);
            info.AddValue("Name", Name);
            info.AddValue("Address", Address);
            info.AddValue("Coordinate", Coordinate);
            info.AddValue("Type", Type);
            info.AddValue("TotalCapacity", TotalCapacity);
            info.AddValue("UsedCapacity", UsedCapacity);
            info.AddValue("OperatingHours", OperatingHours);
            info.AddValue("Manager", Manager);
        }

        // ===== IReportable =====
        public string GenerateReport()
        {
            double availableCapacity = GetAvailableCapacity();
            double usagePercent = GetUsagePercentage();
            return "========== BAO CAO KHO HANG ==========\n" +
                   "  ID: " + WarehouseID + " | Ten: " + Name + "\n" +
                   "  Dia chi: " + Address + "\n" +
                   "  Toa do: " + Coordinate + "\n" +
                   "  Loai: " + Type + "\n" +
                   "  Suc chua: " + UsedCapacity + "/" + TotalCapacity + " (" + usagePercent.ToString("F1") + "%)\n" +
                   "  Con trong: " + availableCapacity + "\n" +
                   "  Gio hoat dong: " + OperatingHours + "\n" +
                   "  Quan ly: " + (Manager != null ? Manager.FullName : "Chua gan") + "\n" +
                   "=======================================";
        }

        // ===== ITrackable =====
        public string GetCurrentStatus()
        {
            return "Active";
        }

        public string GetTrackingInfo()
        {
            return GetWarehouseInfo();
        }

        // ===== WAREHOUSE METHODS =====

        public double GetAvailableCapacity()
        {
            return TotalCapacity - UsedCapacity;
        }

        public bool AddUsedCapacity(double amount)
        {
            if (UsedCapacity + amount <= TotalCapacity)
            {
                UsedCapacity += amount;
                return true;
            }
            return false;
        }

        public void ReleaseCapacity(double amount)
        {
            UsedCapacity = UsedCapacity - amount;
            if (UsedCapacity < 0)
            {
                UsedCapacity = 0;
            }
        }

        public bool HasSpace(double requiredSpace)
        {
            return GetAvailableCapacity() >= requiredSpace;
        }

        public void UpdateOperatingHours(string newHours)
        {
            OperatingHours = newHours;
        }

        public void UpdateManager(WarehouseStaff? newManager)
        {
            Manager = newManager;
        }

        public void UpdateInfo(string name, string address, double totalCapacity, WarehouseType type)
        {
            Name = name;
            Address = address;
            TotalCapacity = totalCapacity;
            Type = type;
        }

        public double GetUsagePercentage()
        {
            if (TotalCapacity == 0)
            {
                return 0;
            }
            return (UsedCapacity / TotalCapacity) * 100;
        }

        public string GetWarehouseInfo()
        {
            return "[Warehouse] ID: " + WarehouseID + " | Name: " + Name + "\n" +
                   "  Address: " + Address + " | Coordinate: " + Coordinate + "\n" +
                   "  Type: " + Type + " | Capacity: " + UsedCapacity + "/" + TotalCapacity + " (" + GetUsagePercentage().ToString("F1") + "%)\n" +
                   "  Hours: " + OperatingHours + " | Manager: " + (Manager != null ? Manager.StaffID : "None");
        }

        public override string ToString()
        {
            return GetWarehouseInfo();
        }
    }
}

