using Logistics.Core.Models.Actors;

namespace Logistics.Core.Validations
{
    public class DispatcherValidator : IValidator<Dispatcher>
    {
        public ValidationResult Validate(Dispatcher dispatcher)
        {
            ValidationResult result = new ValidationResult();

            if (dispatcher == null)
            {
                result.AddError("Dispatcher cannot be null.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(dispatcher.StaffID))
            {
                result.AddError("Dispatcher staff ID is required.");
            }

            if (string.IsNullOrWhiteSpace(dispatcher.Department))
            {
                result.AddError("Dispatcher department is required.");
            }

            if (string.IsNullOrWhiteSpace(dispatcher.ManagedRegion))
            {
                result.AddError("Dispatcher managed region is required.");
            }

            if (dispatcher.BaseSalary < 0)
            {
                result.AddError("Dispatcher base salary cannot be negative.");
            }

            if (dispatcher.KpiBonus < 0)
            {
                result.AddError("Dispatcher KPI bonus cannot be negative.");
            }

            if (dispatcher.RegionAllowance < 0)
            {
                result.AddError("Dispatcher region allowance cannot be negative.");
            }

            PersonValidator personValidator = new PersonValidator();
            ValidationResult personResult = personValidator.Validate(dispatcher);
            foreach (string error in personResult.Errors)
            {
                result.AddError(error);
            }

            return result;
        }
    }
}
