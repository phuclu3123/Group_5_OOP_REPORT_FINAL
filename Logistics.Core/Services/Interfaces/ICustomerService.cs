using System.Collections.Generic;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(string customerId);
        Customer AddCustomer(string fullName, string phone, string email, Address address, CustomerType customerType, decimal creditLimit);
        Customer AddCustomer(string fullName, string phone, string email, Address address, CustomerType customerType, decimal creditLimit, string accountId);
        bool UpdateCustomerContact(string customerId, string phone, string email, Address address);
        bool UpdateCustomerPolicy(string customerId, CustomerType customerType, decimal creditLimit);
        bool AddLoyaltyPoints(string customerId, int points);
        bool AdjustLoyaltyPoints(string customerId, int points);
        bool ResetLoyaltyPoints(string customerId);
        int CalculateEarnedPoints(decimal orderAmount, CustomerType customerType);
        CustomerType SuggestCustomerType(int loyaltyPoints, decimal totalSpending);
        string GetCustomerPolicySummary(Customer customer);
        bool DeleteCustomer(string customerId);
    }
}
