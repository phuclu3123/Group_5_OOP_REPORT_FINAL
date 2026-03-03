using System;
using System.Collections.Generic;
using System.Text;

namespace Group_OOP_FINAL.Infrastructure
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

            if (string.IsNullOrWhiteSpace(locationId))
                throw new ArgumentException("LocationID cannot be empty.");

            if (string.IsNullOrWhiteSpace(warehouseId))
                throw new ArgumentException("WarehouseID cannot be empty.");

            if (maxWeight <= 0)
                throw new ArgumentException("MaxWeight must be greater than 0.");

        }
        public WarehouseLocation() { }

        // ===== BUSINESS METHODS =====
        public void Occupy()
        {
            if (!IsAvailable)
                throw new InvalidOperationException("Location is already occupied.");

            IsAvailable = false;
        }
        public void Release()
        {
            if (IsAvailable)
                throw new InvalidOperationException("Location is already free.");

            IsAvailable = true;
        }

        public bool CanStore(double weight) =>
            IsAvailable && weight > 0 && weight <= MaxWeight;

        public string GetLocationInfo()
        {
            string availableText = IsAvailable ? "Yes" : "No";

            return $"[Location] ID: {LocationID} | Warehouse: {WarehouseID}\n" +
                   $"  Zone: {ZoneType} | Max Weight: {MaxWeight}kg\n" +
                   $"  Available: {availableText}";
        }

        public override string ToString() => GetLocationInfo();
    }
}