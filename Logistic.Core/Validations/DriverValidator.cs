using Logistic.Core.Models.Actors;
using Logistic.Core.Models.Common;

namespace Logistic.Core.Validations
{
    // ============================================================
    // INHERITANCE: Ke thua PersonValidator (Template Method Pattern)
    // DriverValidator chi override ValidateSpecific() de them cac
    // quy tac kiem tra dac thu cho tai xe (bang lai, loai xe,...).
    // Phan validate chung (ten, email, SDT,...) da duoc xu ly o PersonValidator.
    // ============================================================
    public class DriverValidator : PersonValidator
    {
        // Override phuong thuc validate rieng cho Driver
        // Chi duoc goi tu Validate() cua lop cha (Template Method)
        protected override void ValidateSpecific(Person entity, ValidationResult result)
        {
            // Ep kieu tu Person sang Driver de truy cap cac property dac thu
            Driver driver = entity as Driver;
            if (driver == null)
            {
                result.AddError("Doi tuong khong phai la Driver.");
                return;
            }

            // Kiem tra ma nhan vien
            if (string.IsNullOrEmpty(driver.StaffID))
            {
                result.AddError("Ma nhan vien (StaffID) cua tai xe khong duoc de trong.");
            }

            // Kiem tra so bang lai xe
            if (string.IsNullOrEmpty(driver.LicenseNumber))
            {
                result.AddError("So bang lai xe khong duoc de trong.");
            }

            // Kiem tra loai bang lai
            if (string.IsNullOrEmpty(driver.LicenseType))
            {
                result.AddError("Loai bang lai khong duoc de trong.");
            }
            else
            {
                // Kiem tra loai bang lai co hop le khong (A1, A2, B1, B2, C, D, E, F)
                bool isValidLicenseType = false;
                string[] validTypes = new string[] { "A1", "A2", "B1", "B2", "C", "D", "E", "F" };
                for (int i = 0; i < validTypes.Length; i++)
                {
                    if (driver.LicenseType == validTypes[i])
                    {
                        isValidLicenseType = true;
                        break;
                    }
                }
                if (!isValidLicenseType)
                {
                    result.AddError("Loai bang lai khong hop le. Chi chap nhan: A1, A2, B1, B2, C, D, E, F.");
                }
            }

            // Kiem tra luong co ban phai duong
            if (driver.BaseSalary <= 0)
            {
                result.AddError("Luong co ban cua tai xe phai lon hon 0.");
            }

            // Kiem tra so chuyen giao hang khong duoc am
            if (driver.DeliveryCount < 0)
            {
                result.AddError("So chuyen giao hang khong duoc am.");
            }

            // Kiem tra phu cap xang khong duoc am
            if (driver.FuelAllowance < 0)
            {
                result.AddError("Phu cap xang khong duoc am.");
            }
        }
    }
}
