using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Cuoi_ky_OOP.Models.Business
{
    public enum PaymentMethod { CreditCard, DebitCard, BankTransfer, Cash, EWallet }
    public enum TransactionStatus { Pending, Completed, Failed, Refunded }

    public class Transaction
    {
        private string TransactionId { get; set; } = string.Empty;
        private string OrderID { get; set; } = string.Empty;
        private decimal Amount { get; set; }
        private PaymentMethod PaymentMethod { get; set; }
        private TransactionStatus Status { get; set; }
        private DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public Transaction() {}
    }
}