namespace Logistic.Core.Validations
{
    // ============================================================
    // Validator cho dia chi (dang string)
    // Kiem tra chuoi dia chi co hop le hay khong:
    // khong rong, do dai toi thieu, co chua so nha hoac ten duong co ban.
    // ============================================================
    public class AddressValidator : IValidator<string>
    {
        // ===== HANG SO =====

        // Do dai toi thieu cua dia chi hop le
        private const int MIN_ADDRESS_LENGTH = 5;

        // Do dai toi da cua dia chi
        private const int MAX_ADDRESS_LENGTH = 500;

        // Thuc hien kiem tra tinh hop le cua chuoi dia chi
        public ValidationResult Validate(string entity)
        {
            ValidationResult result = new ValidationResult();

            // Kiem tra null hoac rong
            if (string.IsNullOrEmpty(entity))
            {
                result.AddError("Dia chi khong duoc de trong.");
                return result;
            }

            // Loai bo khoang trang thua 2 dau
            string trimmedAddress = entity.Trim();

            // Kiem tra do dai toi thieu
            if (trimmedAddress.Length < MIN_ADDRESS_LENGTH)
            {
                result.AddError("Dia chi phai co it nhat " + MIN_ADDRESS_LENGTH + " ky tu.");
            }

            // Kiem tra do dai toi da
            if (trimmedAddress.Length > MAX_ADDRESS_LENGTH)
            {
                result.AddError("Dia chi khong duoc vuot qua " + MAX_ADDRESS_LENGTH + " ky tu.");
            }

            // Kiem tra dia chi co chua it nhat 1 chu cai (khong chi la so/ky tu dac biet)
            bool hasLetter = false;
            for (int i = 0; i < trimmedAddress.Length; i++)
            {
                if (char.IsLetter(trimmedAddress[i]))
                {
                    hasLetter = true;
                    break;
                }
            }
            if (!hasLetter)
            {
                result.AddError("Dia chi phai chua it nhat 1 chu cai.");
            }

            // Kiem tra dia chi khong chi gom khoang trang
            bool hasNonSpace = false;
            for (int i = 0; i < trimmedAddress.Length; i++)
            {
                if (trimmedAddress[i] != ' ')
                {
                    hasNonSpace = true;
                    break;
                }
            }
            if (!hasNonSpace)
            {
                result.AddError("Dia chi khong duoc chi gom khoang trang.");
            }

            return result;
        }
    }
}
