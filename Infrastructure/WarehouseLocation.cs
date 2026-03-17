using System;
using Cuoi_ky_OOP.Models.Common;
using Cuoi_ky_OOP.Models.Interfaces;

namespace Cuoi_ky_OOP.Models.Infrastructure
{
    public class WarehouseLocation : ITrackable
    {
        public string LocationID { get; private set; }
        public string WarehouseID { get; private set; }
        public ZoneType ZoneType { get; private set; }
        public double MaxWeight { get; private set; }
        public bool IsAvailable { get; private set; }

        public WarehouseLocation(string locationId, string warehouseId, ZoneType zoneType,
                                 double maxWeight)
        {
            LocationID = locationId;
            WarehouseID = warehouseId;
            ZoneType = zoneType;
            MaxWeight = maxWeight;
            IsAvailable = true;
        }

        // Constructor khong tham so cho XML serialization
        public WarehouseLocation() 
        { 
            LocationID = null!;
            WarehouseID = null!;
        }

        // ===== ITrackable =====
        public string GetCurrentStatus()
        {
            if (IsAvailable) return "Available";
            return "Occupied";
        }

        public string GetTrackingInfo()
        {
            return GetLocationInfo();
        }

        // Danh dau vi tri da su dung
        public void Occupy()
        {
            IsAvailable = false;
        }

        // Giai phong vi tri
        public void Release()
        {
            IsAvailable = true;
        }

        // Doi trang thai
        public void ToggleAvailability()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
            }
            else
            {
                IsAvailable = true;
            }
        }

        // Kiem tra co chua duoc khoi luong nay khong
        public bool CanStore(double weight)
        {
            return IsAvailable && weight <= MaxWeight;
        }

        // Lay thong tin vi tri
        public string GetLocationInfo()
        {
            string availableText = "No";
            if (IsAvailable)
            {
                availableText = "Yes";
            }
            return "[Location] ID: " + LocationID + " | Warehouse: " + WarehouseID + "\n" +
                   "  Zone: " + ZoneType + " | Max Weight: " + MaxWeight + "kg\n" +
                   "  Available: " + availableText;
        }

        public override string ToString()
        {
            return GetLocationInfo();
        }
    }
}
