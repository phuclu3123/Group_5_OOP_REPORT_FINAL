using System;
using System.Text.RegularExpressions;
using Logistics.Core.Models.Actors;

namespace Logistics.Core.Validations
{
    /// <summary>
    /// Validates Customer data: FullName, Phone, Email, CustomerType, CreditLimit.
    /// </summary>
    public class CustomerValidator : IValidator<Customer>
    {
        public ValidationResult Validate(Customer customer)
        {
            ValidationResult result = new ValidationResult();

            if (customer == null)
            {
                result.AddError("Customer cannot be null.");
                return result;
            }

            // Tên khách hàng
            if (string.IsNullOrWhiteSpace(customer.FullName))
            {
                result.AddError("Tên khách hàng không được để trống.");
            }
            else if (customer.FullName.Length < 3)
            {
                result.AddError("Tên khách hàng phải có ít nhất 3 ký tự.");
            }

            // Số điện thoại
            if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
            {
                result.AddError("Số điện thoại không được để trống.");
            }
            else if (!Regex.IsMatch(customer.PhoneNumber, @"^(0|\+84)\d{9}$"))
            {
                result.AddError("Số điện thoại không hợp lệ (phải bắt đầu bằng 0 hoặc +84, 10 chữ số).");
            }

            // Email
            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                try
                {
                    System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(customer.Email);
                    if (addr.Address != customer.Email)
                        result.AddError("Định dạng email không hợp lệ.");
                }
                catch
                {
                    result.AddError("Định dạng email không hợp lệ.");
                }
            }

            // Hạn mức tín dụng
            if (customer.CreditLimit < 0)
            {
                result.AddError("Hạn mức tín dụng không được âm.");
            }

            return result;
        }
    }
}
