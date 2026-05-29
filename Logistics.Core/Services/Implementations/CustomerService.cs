using System;
using System.Collections.Generic;
using Logistics.Core.Configuration;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Common;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;
using Logistics.Core.Validations;

namespace Logistics.Core.Services.Implementations
{
    public class CustomerService : Interfaces.ICustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly IAuditService? _auditService;
        private readonly BusinessRules _businessRules;

        public CustomerService(CustomerRepository customerRepository, IAuditService? auditService = null, BusinessRules? businessRules = null)
        {
            _customerRepository = customerRepository;
            _auditService = auditService;
            _businessRules = businessRules ?? new BusinessRules();
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> all = _customerRepository.GetAll();
            List<Customer> active = new List<Customer>();
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].IsActive)
                {
                    active.Add(all[i]);
                }
            }

            return active;
        }

        public Customer GetCustomerById(string customerId)
        {
            return _customerRepository.GetById(customerId);
        }

        public Customer AddCustomer(string fullName, string phone, string email, Address address, CustomerType customerType, decimal creditLimit)
        {
            return AddCustomer(fullName, phone, email, address, customerType, creditLimit, null!);
        }

        public Customer AddCustomer(string fullName, string phone, string email, Address address, CustomerType customerType, decimal creditLimit, string accountId)
        {
            string customerId = GenerateCustomerId();
            Customer customer = new Customer(
                customerId,
                fullName,
                phone,
                email,
                address,
                DateTime.Now.AddYears(-25),
                Gender.Other,
                customerType,
                new GeoPoint(0, 0),
                creditLimit,
                accountId);

            EnsureValidCustomer(customer);
            _customerRepository.Add(customer);
            _auditService?.Log(GetActor(), "CUSTOMER_CREATED", "Customer", customer.Id, "Customer created: " + customer.FullName);
            return customer;
        }

        public bool UpdateCustomerContact(string customerId, string phone, string email, Address address)
        {
            Customer customer = _customerRepository.GetById(customerId);
            if (customer == null)
            {
                return false;
            }

            Customer candidate = new Customer(
                customer.Id,
                customer.FullName,
                phone,
                email,
                address,
                customer.BirthDay,
                customer.Gender,
                customer.CustomerType,
                customer.DefaultLocation,
                customer.CreditLimit,
                customer.AccountID);

            if (!IsValidCustomer(candidate))
            {
                return false;
            }

            customer.UpdatePhoneNumber(phone);
            customer.UpdateEmail(email);
            customer.UpdateAddress(address);
            _customerRepository.Update(customer);
            _auditService?.Log(GetActor(), "CUSTOMER_CONTACT_UPDATED", "Customer", customer.Id, "Customer contact updated.");
            return true;
        }

        public bool UpdateCustomerPolicy(string customerId, CustomerType customerType, decimal creditLimit)
        {
            Customer customer = _customerRepository.GetById(customerId);
            if (customer == null)
            {
                return false;
            }

            Customer candidate = new Customer(
                customer.Id,
                customer.FullName,
                customer.PhoneNumber,
                customer.Email,
                customer.HomeAddress,
                customer.BirthDay,
                customer.Gender,
                customerType,
                customer.DefaultLocation,
                creditLimit,
                customer.AccountID);

            if (!IsValidCustomer(candidate))
            {
                return false;
            }

            customer.UpdateCustomerType(customerType);
            customer.UpdateCreditLimit(creditLimit);
            _customerRepository.Update(customer);
            _auditService?.Log(GetActor(), "CUSTOMER_POLICY_UPDATED", "Customer", customer.Id, "Customer policy updated.");
            return true;
        }

        public bool AddLoyaltyPoints(string customerId, int points)
        {
            return AdjustLoyaltyPoints(customerId, points);
        }

        public bool AdjustLoyaltyPoints(string customerId, int points)
        {
            Customer customer = _customerRepository.GetById(customerId);
            if (customer == null)
            {
                return false;
            }

            customer.AddLoyaltyPoints(points);
            CustomerType suggestedType = SuggestCustomerType(customer.LoyaltyPoints, 0);
            if (suggestedType > customer.CustomerType)
            {
                customer.UpdateCustomerType(suggestedType);
                customer.UpdateCreditLimit(_businessRules.GetDefaultCreditLimit(suggestedType));
            }
            _customerRepository.Update(customer);
            _auditService?.Log(GetActor(), "CUSTOMER_POINTS_UPDATED", "Customer", customer.Id, "Adjusted loyalty points by " + points);
            return true;
        }

        public bool ResetLoyaltyPoints(string customerId)
        {
            Customer customer = _customerRepository.GetById(customerId);
            if (customer == null)
            {
                return false;
            }

            customer.SetLoyaltyPoints(0);
            _customerRepository.Update(customer);
            _auditService?.Log(GetActor(), "CUSTOMER_POINTS_RESET", "Customer", customer.Id, "Reset loyalty points.");
            return true;
        }

        public int CalculateEarnedPoints(decimal orderAmount, CustomerType customerType)
        {
            if (orderAmount <= 0)
            {
                return 0;
            }

            return _businessRules.CalculateEarnedPoints(orderAmount, customerType);
        }

        public CustomerType SuggestCustomerType(int loyaltyPoints, decimal totalSpending)
        {
            return _businessRules.SuggestCustomerType(loyaltyPoints, totalSpending);
        }

        public string GetCustomerPolicySummary(Customer customer)
        {
            if (customer == null)
            {
                return string.Empty;
            }

            decimal discount = _businessRules.GetDiscountRate(customer.CustomerType);
            int multiplier = customer.CustomerType == CustomerType.Enterprise ? 3 : customer.CustomerType == CustomerType.VIP ? 2 : 1;
            return "CHINH SACH KHACH HANG\n" +
                   "Khach hang: " + customer.FullName + " (" + customer.Id + ")\n" +
                   "Hang hien tai: " + customer.CustomerType + "\n" +
                   "Diem hien tai: " + customer.LoyaltyPoints.ToString("N0") + "\n" +
                   "Han muc tin dung: " + customer.CreditLimit.ToString("N0") + " VND\n\n" +
                   "Quy tac tich diem: 1 diem / " + _businessRules.VndPerPoint.ToString("N0") + " VND cuoc van chuyen.\n" +
                   "He so hang " + customer.CustomerType + ": x" + multiplier + "\n" +
                   "Uu dai cuoc: " + discount.ToString("P0") + "\n\n" +
                   "Dieu kien nang hang:\n" +
                   "- VIP: tu " + _businessRules.VipPointThreshold.ToString("N0") + " diem hoac tong chi tieu tu " + _businessRules.VipCreditLimit.ToString("N0") + " VND.\n" +
                   "- Enterprise: tu " + _businessRules.EnterprisePointThreshold.ToString("N0") + " diem hoac tong chi tieu tu " + _businessRules.EnterpriseCreditLimit.ToString("N0") + " VND.\n\n" +
                   "Dac quyen:\n" +
                   "- Standard: tich diem co ban, ho tro theo thu tu tiep nhan.\n" +
                   "- VIP: uu tien dieu phoi, giam " + _businessRules.VipDiscountRate.ToString("P0") + " phi dich vu, nhan doi diem.\n" +
                   "- Enterprise: han muc cong no rieng, giam " + _businessRules.EnterpriseDiscountRate.ToString("P0") + " phi dich vu, gap ba diem, uu tien xu ly su co.";
        }

        public bool DeleteCustomer(string customerId)
        {
            Customer customer = _customerRepository.GetById(customerId);
            if (customer == null)
            {
                return false;
            }

            customer.Deactivate();
            _customerRepository.Update(customer);
            _auditService?.Log(GetActor(), "CUSTOMER_DEACTIVATED", "Customer", customer.Id, "Customer soft-deleted.");
            return true;
        }

        private static string GetActor()
        {
            return SessionManager.CurrentUser != null ? SessionManager.CurrentUser.Username : "system";
        }

        private static bool IsValidCustomer(Customer customer)
        {
            CustomerValidator validator = new CustomerValidator();
            ValidationResult validation = validator.Validate(customer);
            return validation.IsValid;
        }

        private static void EnsureValidCustomer(Customer customer)
        {
            CustomerValidator validator = new CustomerValidator();
            ValidationResult validation = validator.Validate(customer);
            if (!validation.IsValid)
            {
                throw new ArgumentException(BuildValidationMessage(validation));
            }
        }

        private static string BuildValidationMessage(ValidationResult validation)
        {
            return "Customer data is invalid: " + string.Join(" ", validation.Errors);
        }

        private string GenerateCustomerId()
        {
            int nextNumber = _customerRepository.Count() + 1;
            string suffix = nextNumber.ToString();
            while (suffix.Length < 4)
            {
                suffix = "0" + suffix;
            }

            return "CUS" + DateTime.Now.ToString("yyyyMMdd") + suffix;
        }
    }
}
