using System.Collections.Generic;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Services.Interfaces
{
    public interface ITransactionService
    {
        Transaction CreatePayment(string orderId, decimal amount, PaymentMethod paymentMethod);
        bool CompleteTransaction(string transactionId);
        bool RefundTransaction(string transactionId);
        List<Transaction> GetTransactionsByOrder(string orderId);
        List<Transaction> GetAllTransactions();
        decimal GetPaidAmount(string orderId);
        decimal GetRemainingAmount(string orderId);
        string GenerateReceipt(string transactionId);
    }
}
