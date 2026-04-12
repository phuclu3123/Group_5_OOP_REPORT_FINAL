using Logistic.Core.Models.Business;

namespace Logistic.Core.Validations
{
    // ============================================================
    // Validator cho Order (Don hang)
    // Kiem tra tinh hop le cua cac truong bat buoc trong don hang
    // truoc khi xu ly hoac luu tru.
    // ============================================================
    public class OrderValidator : IValidator<Order>
    {
        // Thuc hien kiem tra tinh hop le cua don hang
        public ValidationResult Validate(Order entity)
        {
            ValidationResult result = new ValidationResult();

            // Kiem tra doi tuong null
            if (entity == null)
            {
                result.AddError("Don hang khong duoc null.");
                return result;
            }

            // Kiem tra ma tracking
            if (string.IsNullOrEmpty(entity.TrackingNumber))
            {
                result.AddError("Ma tracking khong duoc de trong.");
            }

            // Kiem tra thong tin nguoi gui
            if (string.IsNullOrEmpty(entity.SenderID))
            {
                result.AddError("Ma nguoi gui (SenderID) khong duoc de trong.");
            }

            // Kiem tra thong tin nguoi nhan
            if (string.IsNullOrEmpty(entity.ReceiverID))
            {
                result.AddError("Ma nguoi nhan (ReceiverID) khong duoc de trong.");
            }

            // Kiem tra dia chi lay hang
            if (string.IsNullOrEmpty(entity.PickupAddress))
            {
                result.AddError("Dia chi lay hang khong duoc de trong.");
            }
            else if (entity.PickupAddress.Length < 5)
            {
                result.AddError("Dia chi lay hang phai co it nhat 5 ky tu.");
            }

            // Kiem tra dia chi giao hang
            if (string.IsNullOrEmpty(entity.DeliveryAddress))
            {
                result.AddError("Dia chi giao hang khong duoc de trong.");
            }
            else if (entity.DeliveryAddress.Length < 5)
            {
                result.AddError("Dia chi giao hang phai co it nhat 5 ky tu.");
            }

            // Kiem tra dia chi lay va giao khong duoc giong nhau
            if (!string.IsNullOrEmpty(entity.PickupAddress) &&
                !string.IsNullOrEmpty(entity.DeliveryAddress) &&
                entity.PickupAddress == entity.DeliveryAddress)
            {
                result.AddError("Dia chi lay hang va giao hang khong duoc giong nhau.");
            }

            // Kiem tra phai co it nhat 1 goi hang
            if (entity.Packages == null || entity.Packages.Count == 0)
            {
                result.AddError("Don hang phai co it nhat 1 goi hang (Package).");
            }

            // Kiem tra tong khoi luong hop le
            if (entity.TotalWeight < 0)
            {
                result.AddError("Tong khoi luong khong duoc am.");
            }

            // Kiem tra tong chi phi hop le
            if (entity.TotalCost < 0)
            {
                result.AddError("Tong chi phi khong duoc am.");
            }

            return result;
        }
    }
}
