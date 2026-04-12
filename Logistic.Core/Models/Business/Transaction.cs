using System;
using System.Runtime.Serialization; // Thu vien ho tro ISerializable
using Logistic.Core.Models.Common;

namespace Logistic.Core.Models.Business
{
    // Danh dau class co the duoc serialize
    [Serializable]
    public class Transaction : ISerializable
    {
        public string TransactionID { get; private set; }
        public string OrderID { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public TransactionStatus Status { get; private set; }
        public DateTime Timestamp { get; private set; }

        public Transaction(string transactionId, string orderId, decimal amount, PaymentMethod paymentMethod)
        {
            TransactionID = transactionId;
            OrderID = orderId;
            Amount = amount;
            PaymentMethod = paymentMethod;
            Status = TransactionStatus.Pending;
            Timestamp = DateTime.Now;
        }

        // Constructor khong tham so cho serialization
        public Transaction() { }

        // ===== ISERIALIZABLE: Constructor phuc hoi (Deserialization) =====
        // Phuc hoi doi tuong Transaction tu SerializationInfo khi doc tu file
        protected Transaction(SerializationInfo info, StreamingContext context)
        {
            TransactionID = info.GetString("TransactionID") ?? "";
            OrderID = info.GetString("OrderID") ?? "";
            Amount = info.GetDecimal("Amount");
            PaymentMethod = (PaymentMethod)info.GetValue("PaymentMethod", typeof(PaymentMethod)); // Phuc hoi enum
            Status = (TransactionStatus)info.GetValue("Status", typeof(TransactionStatus));       // Phuc hoi enum
            Timestamp = info.GetDateTime("Timestamp");
        }

        // ===== ISERIALIZABLE: Ghi du lieu (Serialization) =====
        // Ghi toan bo property cua Transaction vao SerializationInfo de luu tru
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TransactionID", TransactionID);
            info.AddValue("OrderID", OrderID);
            info.AddValue("Amount", Amount);
            info.AddValue("PaymentMethod", PaymentMethod); // Ghi enum PaymentMethod
            info.AddValue("Status", Status);               // Ghi enum TransactionStatus
            info.AddValue("Timestamp", Timestamp);         // Ghi DateTime
        }

        // Hoan tat giao dich
        public void CompleteTransaction()
        {
            Status = TransactionStatus.Completed;
        }

        // Hoan tien giao dich
        public void RefundTransaction()
        {
            if (Status == TransactionStatus.Completed)
            {
                Status = TransactionStatus.Refunded;
            }
        }

        // Danh dau giao dich that bai
        public void FailTransaction()
        {
            Status = TransactionStatus.Failed;
        }

        // Kiem tra giao dich da hoan tat chua
        public bool IsCompleted()
        {
            return Status == TransactionStatus.Completed;
        }

        // Lay thong tin giao dich
        public string GetTransactionInfo()
        {
            return "[Transaction] ID: " + TransactionID + " | Order: " + OrderID + "\n" +
                   "  Amount: " + Amount.ToString("N0") + " VND | Method: " + PaymentMethod + "\n" +
                   "  Status: " + Status + " | Time: " + Timestamp.ToString("dd/MM/yyyy HH:mm");
        }

        public override string ToString()
        {
            return GetTransactionInfo();
        }
    }
}