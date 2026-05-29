using System;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Models.Business
{
    public class ShipmentLog
    {
        public string LogID { get; set; }
        public string TrackingNumber { get; set; }
        public OrderStatus Status { get; set; }
        public string Location { get; set; }
        public DateTime Timestamp { get; set; }
        public string Note { get; set; }

        public ShipmentLog(string logId, string trackingNumber, OrderStatus status,
                           string location, string note)
        {
            LogID = logId;
            TrackingNumber = trackingNumber;
            Status = status;
            Location = location;
            Timestamp = DateTime.Now;
            Note = note;
        }

        // Constructor khong tham so cho JSON serialization
        public ShipmentLog()
        {
            LogID = string.Empty;
            TrackingNumber = string.Empty;
            Location = string.Empty;
            Note = string.Empty;
        }

        // Cap nhat ghi chu
        public void UpdateNote(string newNote)
        {
            Note = newNote;
        }

        // Lay thong tin log
        public string GetLogInfo()
        {
            return "[ShipmentLog] ID: " + LogID + " | Tracking: " + TrackingNumber + "\n" +
                   "  Status: " + Status + " | Location: " + Location + "\n" +
                   "  Time: " + Timestamp.ToString("dd/MM/yyyy HH:mm") + " | Note: " + Note;
        }

        public override string ToString()
        {
            return GetLogInfo();
        }
    }
}
