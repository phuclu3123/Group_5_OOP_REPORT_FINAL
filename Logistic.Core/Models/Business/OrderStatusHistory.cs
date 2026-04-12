using System;
using System.Runtime.Serialization; // Thu vien ho tro ISerializable
using Logistic.Core.Models.Common;

namespace Logistic.Core.Models.Business
{
    // ============================================================
    // COMPOSITION voi Order: OrderStatusHistory CHI duoc tao ben trong
    // Order thong qua AddStatusHistory(). Khong the ton tai doc lap.
    // Ghi lai lich su thay doi trang thai don hang (Timeline Tracking).
    // ============================================================
    [Serializable]
    public class OrderStatusHistory : ISerializable
    {
        // ===== PROPERTIES =====

        // Ma dinh danh cua lich su thay doi
        public string HistoryID { get; private set; }

        // Ma tracking cua don hang lien quan
        public string OrderTrackingNumber { get; private set; }

        // Trang thai cu truoc khi thay doi
        public OrderStatus OldStatus { get; private set; }

        // Trang thai moi sau khi thay doi
        public OrderStatus NewStatus { get; private set; }

        // Thoi diem thay doi trang thai
        public DateTime ChangedDate { get; private set; }

        // Nguoi thuc hien thay doi (StaffID hoac DriverID)
        public string ChangedBy { get; private set; }

        // Ghi chu bo sung cho lan thay doi
        public string Note { get; private set; }

        // ===== CONSTRUCTORS =====

        // Constructor chinh - chi duoc goi tu ben trong Order (Composition)
        // Su dung access modifier internal de gioi han pham vi tao doi tuong
        internal OrderStatusHistory(string historyId, string orderTrackingNumber,
                                    OrderStatus oldStatus, OrderStatus newStatus,
                                    string changedBy, string note)
        {
            HistoryID = historyId;
            OrderTrackingNumber = orderTrackingNumber;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangedDate = DateTime.Now;
            ChangedBy = changedBy;
            Note = note;
        }

        // Constructor khong tham so cho serialization
        internal OrderStatusHistory()
        {
            HistoryID = "";
            OrderTrackingNumber = "";
            ChangedBy = "";
            Note = "";
        }

        // ===== ISERIALIZABLE: Constructor phuc hoi (Deserialization) =====
        // Phuc hoi doi tuong OrderStatusHistory tu SerializationInfo khi doc tu file
        protected OrderStatusHistory(SerializationInfo info, StreamingContext context)
        {
            HistoryID = info.GetString("HistoryID") ?? "";
            OrderTrackingNumber = info.GetString("OrderTrackingNumber") ?? "";

            // Phuc hoi enum bang ep kieu (cast) tu GetValue
            OldStatus = (OrderStatus)info.GetValue("OldStatus", typeof(OrderStatus));
            NewStatus = (OrderStatus)info.GetValue("NewStatus", typeof(OrderStatus));

            // Phuc hoi DateTime va string
            ChangedDate = info.GetDateTime("ChangedDate");
            ChangedBy = info.GetString("ChangedBy") ?? "";
            Note = info.GetString("Note") ?? "";
        }

        // ===== ISERIALIZABLE: Ghi du lieu (Serialization) =====
        // Ghi toan bo property cua OrderStatusHistory vao SerializationInfo de luu tru
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("HistoryID", HistoryID);
            info.AddValue("OrderTrackingNumber", OrderTrackingNumber);
            info.AddValue("OldStatus", OldStatus);       // Ghi enum OrderStatus
            info.AddValue("NewStatus", NewStatus);       // Ghi enum OrderStatus
            info.AddValue("ChangedDate", ChangedDate);   // Ghi DateTime
            info.AddValue("ChangedBy", ChangedBy);
            info.AddValue("Note", Note);
        }

        // ===== METHODS =====

        // Cap nhat ghi chu cho lich su thay doi
        public void UpdateNote(string newNote)
        {
            Note = newNote;
        }

        // Lay thong tin chi tiet cua lich su thay doi
        public string GetHistoryInfo()
        {
            return "[StatusHistory] ID: " + HistoryID +
                   " | Order: " + OrderTrackingNumber + "\n" +
                   "  " + OldStatus + " -> " + NewStatus + "\n" +
                   "  By: " + ChangedBy +
                   " | Time: " + ChangedDate.ToString("dd/MM/yyyy HH:mm") + "\n" +
                   "  Note: " + Note;
        }

        public override string ToString()
        {
            return GetHistoryInfo();
        }
    }
}
