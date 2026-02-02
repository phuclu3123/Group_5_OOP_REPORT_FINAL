using System;
using System.Collections.Generic;
using System.Text;

namespace Group_OOP_FINAL.Infrastructure
{
    public class ShipmentLog
    {
        public string LogId { get; set; }
        public string TrackingNumber { get; set; }
        public string Location { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }

        public ShipmentLog(string logId, string trackingNumber, string location, string status)
        {
            LogId = logId;
            TrackingNumber = trackingNumber;
            Location = location;
            Status = status;
            Timestamp = DateTime.Now;
        }
    }
}