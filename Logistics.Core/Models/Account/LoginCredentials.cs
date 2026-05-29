namespace Logistics.Core.Models.Account
{
    /// <summary>
    /// Đối tượng nhẹ (value object) dùng để truyền thông tin đăng nhập từ UI → AuthService.
    /// Không lưu xuống JSON — chỉ tồn tại trong bộ nhớ lúc xử lý form.
    /// </summary>
    public class LoginCredentials
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public LoginCredentials() { }

        public LoginCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Password);
        }
    }
}
