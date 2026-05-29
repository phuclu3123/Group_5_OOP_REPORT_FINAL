using Logistics.Core.Validations;
using Logistics.Core.Models.Actors;

namespace Logistics.Core.Validations
{
    public class StaffValidator : IValidator<WarehouseStaff>
    {
        public ValidationResult Validate(WarehouseStaff staff)
        {
            ValidationResult result = new ValidationResult();

            if (staff == null)
            {
                result.AddError("Staff cannot be null.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(staff.FullName))
            {
                result.AddError("Staff full name is required.");
            }

            if (string.IsNullOrWhiteSpace(staff.StaffID))
            {
                result.AddError("Staff employee ID is required.");
            }

            if (string.IsNullOrWhiteSpace(staff.Department))
            {
                result.AddError("Staff department is required.");
            }

            if (staff.BaseSalary < 0)
            {
                result.AddError("Staff base salary cannot be negative.");
            }

            if (string.IsNullOrWhiteSpace(staff.WarehouseID))
            {
                result.AddError("Warehouse staff warehouse ID is required.");
            }

            if (string.IsNullOrWhiteSpace(staff.Shift))
            {
                result.AddError("Warehouse staff shift is required.");
            }

            if (staff.ShiftAllowance < 0)
            {
                result.AddError("Warehouse staff shift allowance cannot be negative.");
            }

            if (staff.HeavyDutyAllowance < 0)
            {
                result.AddError("Warehouse staff heavy-duty allowance cannot be negative.");
            }

            PersonValidator personValidator = new PersonValidator();
            ValidationResult personResult = personValidator.Validate(staff);
            foreach (string error in personResult.Errors)
            {
                result.AddError(error);
            }

            return result;
        }
    }
}
