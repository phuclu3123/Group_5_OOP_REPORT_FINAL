using Logistics.Core.Validations;
using Logistics.Core.Models.Business;
using System.Globalization;

namespace Logistics.Core.Validations
{
    public class PackageValidator : IValidator<Package>
    {
        public ValidationResult Validate(Package package)
        {
            ValidationResult result = new ValidationResult();

            if (package == null)
            {
                result.AddError("Package cannot be null.");
                return result;
            }

            if (package.ActualWeight <= 0)
            {
                result.AddError("Package weight must be greater than 0.");
            }

            if (package.ActualWeight > 1000)
            {
                result.AddError("Package weight exceeds maximum limit of 1000 kg.");
            }

            if (string.IsNullOrWhiteSpace(package.Dimensions))
            {
                result.AddError("Package dimensions are required (format: LxWxH cm).");
            }
            else if (!HasValidDimensions(package.Dimensions))
            {
                result.AddError("Package dimensions must use format LxWxH with positive numbers in centimeters.");
            }

            return result;
        }

        private static bool HasValidDimensions(string dimensions)
        {
            string[] parts = dimensions.Split(new char[] { 'x', 'X' });
            if (parts.Length != 3)
            {
                return false;
            }

            double length;
            double width;
            double height;
            bool canParseLength = double.TryParse(parts[0].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out length);
            bool canParseWidth = double.TryParse(parts[1].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out width);
            bool canParseHeight = double.TryParse(parts[2].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out height);

            return canParseLength && canParseWidth && canParseHeight && length > 0 && width > 0 && height > 0;
        }
    }
}
