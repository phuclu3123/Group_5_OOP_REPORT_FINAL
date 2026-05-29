using Logistics.Core.Models.Actors;
using System;
using System.Text.RegularExpressions;

namespace Logistics.Core.Validations
{
    /// <summary>
    /// Validates base Person-level data (FullName, PhoneNumber, Email, BirthDate).
    /// </summary>
    public class PersonValidator : IValidator<Person>
    {
        public ValidationResult Validate(Person person)
        {
            ValidationResult result = new ValidationResult();

            if (person == null)
            {
                result.AddError("Person cannot be null.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(person.FullName))
            {
                result.AddError("Full name is required.");
            }
            else if (person.FullName.Length < 3)
            {
                result.AddError("Full name must be at least 3 characters.");
            }

            if (string.IsNullOrWhiteSpace(person.PhoneNumber))
            {
                result.AddError("Phone number is required.");
            }
            else if (!Regex.IsMatch(person.PhoneNumber, @"^0\d{9}$"))
            {
                result.AddError("Phone number format is invalid. It must be a 10-digit number starting with 0.");
            }

            if (string.IsNullOrWhiteSpace(person.Email))
            {
                result.AddError("Email is required.");
            }
            else if (!IsValidEmail(person.Email))
            {
                result.AddError("Email format is invalid.");
            }

            // Check BirthDay (Age >= 18)
            int age = DateTime.Now.Year - person.BirthDay.Year;
            if (person.BirthDay.Date > DateTime.Now.AddYears(-age)) age--; // Precision fix for birthdays

            if (age < 18)
            {
                result.AddError("Person must be at least 18 years old.");
            }

            return result;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
