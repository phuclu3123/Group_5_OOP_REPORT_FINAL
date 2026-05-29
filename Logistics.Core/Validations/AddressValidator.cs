using Logistics.Core.Validations;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Validations
{
    public class AddressValidator : IValidator<Address>
    {
        public ValidationResult Validate(Address address)
        {
            ValidationResult result = new ValidationResult();

            if (address == null)
            {
                result.AddError("Address cannot be null.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(address.StreetAndNumber))
            {
                result.AddError("Street and number is required.");
            }

            if (string.IsNullOrWhiteSpace(address.City))
            {
                result.AddError("City is required.");
            }

            if (string.IsNullOrWhiteSpace(address.District))
            {
                result.AddError("District is required.");
            }

            return result;
        }
    }
}
