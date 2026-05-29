using System.Text.RegularExpressions;

namespace Logistics.Core.Utilities
{
    public static class PasswordValidator
    {
        public static bool IsValid(string password, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                errorMessage = "Mật khẩu phải có ít nhất 8 ký tự.";
                return false;
            }

            if (!ContainsUpper(password))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một chữ cái viết hoa.";
                return false;
            }

            if (!ContainsLower(password))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một chữ cái viết thường.";
                return false;
            }

            if (!ContainsDigit(password))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một chữ số.";
                return false;
            }

            // Kiểm tra ký tự đặc biệt
            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?{}\[\]|<>]"))
            {
                errorMessage = "Mật khẩu phải chứa ít nhất một ký tự đặc biệt (ví dụ: @, #, $, %).";
                return false;
            }

            return true;
        }

        public static string GetRequirementsMessage()
        {
            return "Yêu cầu mật khẩu:\n" +
                   "- Tối thiểu 8 ký tự\n" +
                   "- Có chữ hoa, chữ thường\n" +
                   "- Có ít nhất 1 chữ số\n" +
                   "- Có ký tự đặc biệt (!@#$%...)";
        }

        private static bool ContainsUpper(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsUpper(password[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsLower(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsLower(password[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsDigit(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsDigit(password[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
