using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cuoi_ky_OOP.Models.Business
{
    public enum ShipmentLogStatus { InTransit, Delivered, Failed, Returned }
    public class ShipmentLog
    {
        private string LogID { get; set; } = string.Empty;
        private string TrackingNumber { get; set; } = string.Empty;
        private ShipmentLogStatus Status { get; set; }
        private string Location { get; set; } = string.Empty;
        private DateTime Timestamp { get; set; } = DateTime.UtcNow;
        private string Note { get; set; } = string.Empty;
        public ShipmentLog() {}
    }
}