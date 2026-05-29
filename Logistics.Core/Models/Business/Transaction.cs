using System;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Models.Business
{
    public class Transaction
    {
        public string TransactionID { get; set; }
        public string OrderID { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime Timestamp { get; set; }

        public Transaction(string transactionId, string orderId, decimal amount, PaymentMethod paymentMethod)
        {
            TransactionID = transactionId;
            OrderID = orderId;
            Amount = amount;
            PaymentMethod = paymentMethod;
            Status = TransactionStatus.Pending;
            Timestamp = DateTime.Now;
        }

        // Constructor khong tham so cho JSON serialization
        public Transaction()
        {
            TransactionID = string.Empty;
            OrderID = string.Empty;
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
