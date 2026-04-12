using Logistic.Core.Models.Business;

namespace Logistic.Core.Validations
{
    // ============================================================
    // Validator cho Package (Goi hang)
    // Kiem tra khoi luong, kich thuoc, mo ta truoc khi them vao don hang.
    // ============================================================
    public class PackageValidator : IValidator<Package>
    {
        // Thuc hien kiem tra tinh hop le cua goi hang
        public ValidationResult Validate(Package entity)
        {
            ValidationResult result = new ValidationResult();

            // Kiem tra doi tuong null
            if (entity == null)
            {
                result.AddError("Goi hang khong duoc null.");
                return result;
            }

            // Kiem tra ma goi hang
            if (string.IsNullOrEmpty(entity.PackageID))
            {
                result.AddError("Ma goi hang (PackageID) khong duoc de trong.");
            }

            // Kiem tra ma don hang lien quan
            if (string.IsNullOrEmpty(entity.OrderID))
            {
                result.AddError("Ma don hang (OrderID) khong duoc de trong.");
            }

            // Kiem tra mo ta
            if (string.IsNullOrEmpty(entity.Description))
            {
                result.AddError("Mo ta goi hang khong duoc de trong.");
            }

            // Kiem tra khoi luong thuc te phai lon hon 0
            if (entity.ActualWeight <= 0)
            {
                result.AddError("Khoi luong thuc te phai lon hon 0 kg.");
            }
            else if (entity.ActualWeight > 50000)
            {
                result.AddError("Khoi luong thuc te khong duoc vuot qua 50,000 kg.");
            }

            // Kiem tra kich thuoc (format DxRxC)
            if (string.IsNullOrEmpty(entity.Dimensions))
            {
                result.AddError("Kich thuoc goi hang khong duoc de trong.");
            }
            else
            {
                // Kiem tra format "DxRxC" (3 phan cach nhau boi 'x')
                string[] parts = entity.Dimensions.Split('x');
                if (parts.Length != 3)
                {
                    result.AddError("Kich thuoc phai theo format DxRxC (vd: 30x20x15).");
                }
                else
                {
                    // Kiem tra tung phan co phai la so duong khong
                    for (int i = 0; i < parts.Length; i++)
                    {
                        double dimension = 0;
                        bool isValid = double.TryParse(parts[i].Trim(), out dimension);
                        if (!isValid || dimension <= 0)
                        {
                            result.AddError("Kich thuoc phan thu " + (i + 1) + " phai la so duong.");
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
