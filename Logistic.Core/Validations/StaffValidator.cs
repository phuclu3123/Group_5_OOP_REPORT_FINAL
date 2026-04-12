using Logistic.Core.Models.Actors;
using Logistic.Core.Models.Common;

namespace Logistic.Core.Validations
{
    // ============================================================
    // INHERITANCE: Ke thua PersonValidator (Template Method Pattern)
    // StaffValidator chi override ValidateSpecific() de them cac
    // quy tac kiem tra dac thu cho nhan vien (ma NV, phong ban, luong,...).
    // Phan validate chung (ten, email, SDT,...) da duoc xu ly o PersonValidator.
    // ============================================================
    public class StaffValidator : PersonValidator
    {
        // Override phuong thuc validate rieng cho Staff
        // Chi duoc goi tu Validate() cua lop cha (Template Method)
        protected override void ValidateSpecific(Person entity, ValidationResult result)
        {
            // Ep kieu tu Person sang Staff de truy cap cac property dac thu
            Staff staff = entity as Staff;
            if (staff == null)
            {
                result.AddError("Doi tuong khong phai la Staff.");
                return;
            }

            // Kiem tra ma nhan vien
            if (string.IsNullOrEmpty(staff.StaffID))
            {
                result.AddError("Ma nhan vien (StaffID) khong duoc de trong.");
            }

            // Kiem tra phong ban
            if (string.IsNullOrEmpty(staff.Department))
            {
                result.AddError("Phong ban khong duoc de trong.");
            }

            // Kiem tra luong co ban phai la so duong
            if (staff.BaseSalary <= 0)
            {
                result.AddError("Luong co ban phai lon hon 0.");
            }

            // Kiem tra luong co ban khong vuot muc toi da
            if (staff.BaseSalary > 500000000)
            {
                result.AddError("Luong co ban khong duoc vuot qua 500,000,000 VND.");
            }

            // Kiem tra ngay vao lam khong duoc la tuong lai
            if (staff.JoinDate > System.DateTime.Now)
            {
                result.AddError("Ngay vao lam khong the la ngay trong tuong lai.");
            }

            // Kiem tra trang thai lam viec co hop le khong
            bool isValidStatus = false;
            WorkStatus[] validStatuses = new WorkStatus[]
            {
                WorkStatus.Active,
                WorkStatus.Resigned,
                WorkStatus.OnLeave
            };
            for (int i = 0; i < validStatuses.Length; i++)
            {
                if (staff.WorkStatus == validStatuses[i])
                {
                    isValidStatus = true;
                    break;
                }
            }
            if (!isValidStatus)
            {
                result.AddError("Trang thai lam viec khong hop le.");
            }
        }
    }
}
