using Logistics.Core.Models.Business;

namespace Logistics.Core.Validations
{
    public class OrderValidator : IValidator<Order>
    {
        public ValidationResult Validate(Order order)
        {
            ValidationResult result = new ValidationResult();

            if (order == null)
            {
                result.AddError("Order cannot be null.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(order.TrackingNumber))
            {
                result.AddError("Order tracking number is required.");
            }

            if (string.IsNullOrWhiteSpace(order.SenderID))
            {
                result.AddError("Sender ID is required.");
            }

            if (string.IsNullOrWhiteSpace(order.ReceiverID))
            {
                result.AddError("Receiver ID is required.");
            }

            if (order.Packages == null || order.Packages.Count == 0)
            {
                result.AddError("Order must contain at least one package.");
            }

            return result;
        }
    }
}
