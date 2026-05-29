using System;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Models.Infrastructure
{
    public class WarehouseInventoryLog
    {
        public string LogID { get; set; } = string.Empty;
        public string WarehouseID { get; set; } = string.Empty;
        public string PackageID { get; set; } = string.Empty;
        public string TrackingNumber { get; set; } = string.Empty;
        public string ShelfLocation { get; set; } = string.Empty;
        public InventoryTransactionType TransactionType { get; set; }
        public double PackageWeight { get; set; }
        public DateTime Timestamp { get; set; }
        public string PerformedBy { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;

        public WarehouseInventoryLog()
        {
        }

        public WarehouseInventoryLog(string logId, string warehouseId, string packageId, string trackingNumber, InventoryTransactionType transactionType, double packageWeight, string performedBy)
            : this(logId, warehouseId, packageId, trackingNumber, string.Empty, transactionType, packageWeight, performedBy, string.Empty)
        {
        }

        public WarehouseInventoryLog(string logId, string warehouseId, string packageId, string trackingNumber, string shelfLocation, InventoryTransactionType transactionType, double packageWeight, string performedBy, string note)
        {
            LogID = logId;
            WarehouseID = warehouseId;
            PackageID = packageId;
            TrackingNumber = trackingNumber;
            ShelfLocation = shelfLocation;
            TransactionType = transactionType;
            PackageWeight = packageWeight;
            Timestamp = DateTime.Now;
            PerformedBy = performedBy;
            Note = note;
        }
    }
}
