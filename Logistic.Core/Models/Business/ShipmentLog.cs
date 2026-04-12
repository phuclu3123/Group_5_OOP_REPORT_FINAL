using System;
using System.Runtime.Serialization; // Thu vien ho tro ISerializable
using Logistic.Core.Models.Common;

namespace Logistic.Core.Models.Business
{
    // Danh dau class co the duoc serialize
    [Serializable]
    public class ShipmentLog : ISerializable
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

        // Constructor khong tham so cho serialization
        public ShipmentLog() { }

        // ===== ISERIALIZABLE: Constructor phuc hoi (Deserialization) =====
        // Phuc hoi doi tuong ShipmentLog tu SerializationInfo khi doc tu file
        protected ShipmentLog(SerializationInfo info, StreamingContext context)
        {
            LogID = info.GetString("LogID") ?? "";
            TrackingNumber = info.GetString("TrackingNumber") ?? "";
            Status = (OrderStatus)info.GetValue("Status", typeof(OrderStatus)); // Phuc hoi enum
            Location = info.GetString("Location") ?? "";
            Timestamp = info.GetDateTime("Timestamp");
            Note = info.GetString("Note") ?? "";
        }

        // ===== ISERIALIZABLE: Ghi du lieu (Serialization) =====
        // Ghi toan bo property cua ShipmentLog vao SerializationInfo de luu tru
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LogID", LogID);
            info.AddValue("TrackingNumber", TrackingNumber);
            info.AddValue("Status", Status);       // Ghi enum OrderStatus
            info.AddValue("Location", Location);
            info.AddValue("Timestamp", Timestamp); // Ghi DateTime
            info.AddValue("Note", Note);
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