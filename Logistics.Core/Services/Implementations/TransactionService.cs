using System;
using System.Collections.Generic;
using System.Text;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;
using Logistics.Core.Services.Interfaces;

namespace Logistics.Core.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly OrderRepository _orderRepository;
        private readonly CustomerRepository? _customerRepository;
        private int _counter;

        public TransactionService(TransactionRepository transactionRepository, OrderRepository orderRepository, CustomerRepository? customerRepository = null)
        {
            _transactionRepository = transactionRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _counter = _transactionRepository.Count();
        }

        public Transaction CreatePayment(string orderId, decimal amount, PaymentMethod paymentMethod)
        {
            Order order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new ArgumentException("Khong tim thay don hang " + orderId);
            }

            if (amount <= 0)
            {
                throw new ArgumentException("So tien thanh toan phai lon hon 0.");
            }

            decimal remainingAmount = GetRemainingAmount(orderId);
            if (amount > remainingAmount)
            {
                throw new InvalidOperationException("So tien thanh toan vuot qua so tien con lai cua don hang.");
            }

            if (paymentMethod == PaymentMethod.Credit)
            {
                ValidateCreditPayment(order, amount);
            }

            _counter++;
            string id = "TXN" + DateTime.Now.ToString("yyyyMMdd") + _counter.ToString("0000");
            Transaction transaction = new Transaction(id, orderId, amount, paymentMethod);
            transaction.CompleteTransaction();
            _transactionRepository.Add(transaction);
            return transaction;
        }

        private void ValidateCreditPayment(Order order, decimal amount)
        {
            if (_customerRepository == null)
            {
                return;
            }

            Customer customer = _customerRepository.GetById(order.SenderID);
            if (customer == null)
            {
                throw new InvalidOperationException("Khong tim thay khach hang de ghi cong no.");
            }

            decimal usedCredit = GetUsedCredit(customer.Id);
            if (usedCredit + amount > customer.CreditLimit)
            {
                throw new InvalidOperationException("Vuot han muc cong no cua khach hang. Han muc: " + customer.CreditLimit.ToString("N0") + " VND, da su dung: " + usedCredit.ToString("N0") + " VND.");
            }
        }

        private decimal GetUsedCredit(string customerId)
        {
            decimal total = 0m;
            List<Transaction> transactions = _transactionRepository.GetAll();
            for (int i = 0; i < transactions.Count; i++)
            {
                Transaction transaction = transactions[i];
                if (transaction == null || transaction.PaymentMethod != PaymentMethod.Credit || transaction.Status == TransactionStatus.Refunded)
                {
                    continue;
                }

                Order transactionOrder = _orderRepository.GetById(transaction.OrderID);
                if (transactionOrder != null && transactionOrder.SenderID == customerId)
                {
                    total += transaction.Amount;
                }
            }

            return total;
        }

        public bool CompleteTransaction(string transactionId)
        {
            Transaction transaction = _transactionRepository.GetById(transactionId);
            if (transaction == null) return false;
            transaction.CompleteTransaction();
            _transactionRepository.Update(transaction);
            return true;
        }

        public bool RefundTransaction(string transactionId)
        {
            Transaction transaction = _transactionRepository.GetById(transactionId);
            if (transaction == null) return false;
            transaction.RefundTransaction();
            _transactionRepository.Update(transaction);
            return true;
        }

        public List<Transaction> GetTransactionsByOrder(string orderId)
        {
            return _transactionRepository.FindByOrder(orderId);
        }

        public List<Transaction> GetAllTransactions()
        {
            return _transactionRepository.GetAll();
        }

        public decimal GetPaidAmount(string orderId)
        {
            decimal total = 0;
            List<Transaction> transactions = _transactionRepository.FindByOrder(orderId);
            for (int i = 0; i < transactions.Count; i++)
            {
                if (transactions[i] != null && transactions[i].Status == TransactionStatus.Completed)
                {
                    total += transactions[i].Amount;
                }
            }

            return total;
        }

        public decimal GetRemainingAmount(string orderId)
        {
            Order order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                return 0;
            }

            decimal remaining = order.TotalCost - GetPaidAmount(orderId);
            return remaining > 0 ? remaining : 0;
        }

        public string GenerateReceipt(string transactionId)
        {
            Transaction transaction = _transactionRepository.GetById(transactionId);
            if (transaction == null)
            {
                return "Khong tim thay giao dich: " + transactionId;
            }

            Order order = _orderRepository.GetById(transaction.OrderID);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("CONG TY VAN CHUYEN LOGISTICS");
            builder.AppendLine("PHIEU THU / BIEN NHAN THANH TOAN");
            builder.AppendLine("----------------------------------------");
            builder.AppendLine("Ma giao dich: " + transaction.TransactionID);
            builder.AppendLine("Ma van don: " + transaction.OrderID);
            builder.AppendLine("Ngay thu: " + transaction.Timestamp.ToString("dd/MM/yyyy HH:mm"));
            builder.AppendLine("Hinh thuc: " + transaction.PaymentMethod);
            builder.AppendLine("Trang thai: " + transaction.Status);
            builder.AppendLine("So tien: " + transaction.Amount.ToString("N0") + " VND");
            if (order != null)
            {
                builder.AppendLine("Nguoi gui: " + order.SenderID);
                builder.AppendLine("Nguoi nhan: " + order.ReceiverID);
            }
            builder.AppendLine("----------------------------------------");
            builder.AppendLine("Nguoi thu tien                 Khach hang");
            builder.AppendLine();
            builder.AppendLine("................          ................");
            return builder.ToString();
        }
    }
}
