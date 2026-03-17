using System;
using Cuoi_ky_OOP.Models.Common;
using Cuoi_ky_OOP.Models.Interfaces;
using Cuoi_ky_OOP.Models.Actors;

namespace Cuoi_ky_OOP.Models.Infrastructure
{
    public class Warehouse : IReportable, ITrackable
    {
        public string WarehouseID { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public GeoPoint Coordinate { get; private set; }
        public WarehouseType Type { get; private set; }
        public double TotalCapacity { get; private set; }
        public double UsedCapacity { get; private set; }
        public string OperatingHours { get; private set; }
        // Association: Tham chieu kieu doi tuong (Reference object) de the hien lien ket thay vi chi luu chuoi ID
        public WarehouseStaff Manager { get; private set; }

        public Warehouse(string warehouseId, string name, string address, GeoPoint coordinate,
                         WarehouseType type, double totalCapacity, string operatingHours, WarehouseStaff manager)
        {
            WarehouseID = warehouseId;
            Name = name;
            Address = address;
            Coordinate = coordinate;
            Type = type;
            TotalCapacity = totalCapacity;
            UsedCapacity = 0;
            OperatingHours = operatingHours;
            Manager = manager;
        }

        // Constructor khong tham so cho XML serialization
        public Warehouse()
        {
            WarehouseID = null!;
            Name = null!;
            Address = null!;
            Coordinate = default;
            OperatingHours = null!;
            Manager = null!;
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

        public void UpdateManager(WarehouseStaff newManager)
        {
            Manager = newManager;
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
