using System;
using Logistics.Core.Validations;
using Logistics.Core.Models.Actors;

namespace Logistics.Core.Validations
{
    public class DriverValidator : IValidator<Driver>
    {
        public ValidationResult Validate(Driver driver)
        {
            ValidationResult result = new ValidationResult();

            if (driver == null)
            {
                result.AddError("Driver cannot be null.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(driver.LicenseNumber))
            {
                result.AddError("Driver license number is required.");
            }

            if (string.IsNullOrWhiteSpace(driver.LicenseType))
            {
                result.AddError("Driver license type is required.");
            }

            if (driver.LicenseExpiryDate <= DateTime.Now)
            {
                result.AddError("Driver license must not be expired.");
            }

            if (string.IsNullOrWhiteSpace(driver.StaffID))
            {
                result.AddError("Driver staff ID is required.");
            }

            if (string.IsNullOrWhiteSpace(driver.Department))
            {
                result.AddError("Driver department is required.");
            }

            if (driver.BaseSalary < 0)
            {
                result.AddError("Driver base salary cannot be negative.");
            }

            if (string.IsNullOrWhiteSpace(driver.FullName))
            {
                result.AddError("Driver full name is required.");
            }

            if (string.IsNullOrWhiteSpace(driver.PhoneNumber))
            {
                result.AddError("Driver phone number is required.");
            }

            PersonValidator personValidator = new PersonValidator();
            ValidationResult personResult = personValidator.Validate(driver);
            foreach (string error in personResult.Errors)
            {
                result.AddError(error);
            }

            return result;
        }
    }
}
