using System;
using Cuoi_ky_OOP.Models.Common;

namespace Cuoi_ky_OOP.Models.Business
{
    public class ShipmentLog
    {
        public string LogID { get; private set; }
        public string TrackingNumber { get; private set; }
        public OrderStatus Status { get; private set; }
        public string Location { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Note { get; private set; }

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

        // Constructor khong tham so cho XML serialization
        public ShipmentLog() { }

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