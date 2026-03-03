using System;
using System.Collections.Generic;
using System.Text;

namespace Group_OOP_FINAL.Infrastructure
{
    public class Equipment : ITrackable
    {
        public class Equipment : ITrackable
        {
            public string EquipmentID { get; }
            public EquipmentType Type { get; }
            public string WarehouseID { get; private set; }
            public EquipmentStatus Status { get; private set; }

            public Equipment(string equipmentID,
                             EquipmentType type,
                             string warehouseID,
                             EquipmentStatus status)
            {
                if (string.IsNullOrWhiteSpace(equipmentID))
                    throw new ArgumentException("EquipmentID cannot be empty.");

                if (string.IsNullOrWhiteSpace(warehouseID))
                    throw new ArgumentException("WarehouseID cannot be empty.");

                EquipmentID = equipmentID;
                Type = type;
                WarehouseID = warehouseID;
                Status = status;
            }

            // Constructor cho serialization
            public Equipment() { }

            // ===== ITrackable =====
            public string GetCurrentStatus() => Status.ToString();

            public string GetTrackingInfo()
            {
                return $"[Equipment] ID: {EquipmentID}\n" +
                       $"  Type: {Type} | Warehouse: {WarehouseID}\n" +
                       $"  Status: {Status}";
            }

            // ===== BUSINESS METHODS =====

            public void UpdateStatus(EquipmentStatus newStatus)
            {
                Status = newStatus;
            }

            public void MoveToWarehouse(string newWarehouseId)
            {
                if (string.IsNullOrWhiteSpace(newWarehouseId))
                    throw new ArgumentException("WarehouseID cannot be empty.");

                WarehouseID = newWarehouseId;
            }

            public bool IsAvailable()
            {
                return Status == EquipmentStatus.Active;
            }

            public override string ToString() => GetTrackingInfo();
        }
    }
}