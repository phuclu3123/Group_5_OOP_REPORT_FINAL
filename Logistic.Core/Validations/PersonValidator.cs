using System;
using Logistic.Core.Models.Actors;

namespace Logistic.Core.Validations
{
    // ============================================================
    // TEMPLATE METHOD PATTERN:
    // Lop abstract PersonValidator dinh nghia khung validate chung cho Person.
    // Phuong thuc Validate() goi ValidateCommon() + ValidateSpecific().
    // Cac lop con (DriverValidator, StaffValidator) chi override ValidateSpecific()
    // de them logic rieng ma khong can lap lai phan validate chung.
    // ============================================================
    public abstract class PersonValidator : IValidator<Person>
    {
        // ===== TEMPLATE METHOD =====
        // Khung validate chung: validate chung truoc, validate rieng sau
        // Lop con KHONG duoc override Validate(), chi override ValidateSpecific()
        public ValidationResult Validate(Person entity)
        {
            ValidationResult result = new ValidationResult();

            // Buoc 1: Validate cac truong chung cua Person
            ValidateCommon(entity, result);

            // Buoc 2: Validate cac truong rieng cua tung loai Person
            ValidateSpecific(entity, result);

            return result;
        }

        // ===== VALIDATE CHUNG =====
        // Kiem tra cac truong bat buoc chung cho moi Person
        protected void ValidateCommon(Person entity, ValidationResult result)
        {
            // Kiem tra doi tuong null
            if (entity == null)
            {
                result.AddError("Doi tuong Person khong duoc null.");
                return;
            }

            // Kiem tra ID
            if (string.IsNullOrEmpty(entity.Id))
            {
                result.AddError("ID khong duoc de trong.");
            }

            // Kiem tra ho ten
            if (string.IsNullOrEmpty(entity.FullName))
            {
                result.AddError("Ho ten khong duoc de trong.");
            }
            else if (entity.FullName.Length < 2)
            {
                result.AddError("Ho ten phai co it nhat 2 ky tu.");
            }

            // Kiem tra so dien thoai
            if (string.IsNullOrEmpty(entity.PhoneNumber))
            {
                result.AddError("So dien thoai khong duoc de trong.");
            }
            else if (entity.PhoneNumber.Length < 10)
            {
                result.AddError("So dien thoai phai co it nhat 10 so.");
            }

            // Kiem tra email
            if (string.IsNullOrEmpty(entity.Email))
            {
                result.AddError("Email khong duoc de trong.");
            }
            else
            {
                // Kiem tra email co chua ky tu '@' va '.' khong
                bool hasAt = false;
                bool hasDot = false;
                for (int i = 0; i < entity.Email.Length; i++)
                {
                    if (entity.Email[i] == '@')
                    {
                        hasAt = true;
                    }
                    if (entity.Email[i] == '.')
                    {
                        hasDot = true;
                    }
                }
                if (!hasAt || !hasDot)
                {
                    result.AddError("Email khong hop le (phai chua '@' va '.').");
                }
            }

            // Kiem tra dia chi
            if (string.IsNullOrEmpty(entity.Address))
            {
                result.AddError("Dia chi khong duoc de trong.");
            }

            // Kiem tra ngay sinh hop le
            if (entity.BirthDay > DateTime.Now)
            {
                result.AddError("Ngay sinh khong the la ngay trong tuong lai.");
            }
            else if (entity.GetAge() < 16)
            {
                result.AddError("Nguoi dung phai tu 16 tuoi tro len.");
            }
            else if (entity.GetAge() > 100)
            {
                result.AddError("Tuoi khong hop le (lon hon 100).");
            }
        }

        // ===== VALIDATE RIENG (Abstract) =====
        // Lop con bat buoc override de them logic validate dac thu
        protected abstract void ValidateSpecific(Person entity, ValidationResult result);
    }
}
