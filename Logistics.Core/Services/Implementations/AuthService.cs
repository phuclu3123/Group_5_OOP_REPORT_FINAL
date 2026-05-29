using Logistics.Core.Services.Interfaces;
using Logistics.Core.Models.Account;
using Logistics.Core.DataAccess.Interfaces;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.Models.Common;
using Logistics.Core.Utilities;

namespace Logistics.Core.Services.Implementations
{
    /// <summary>
    /// Dịch vụ xử lý xác thực (BUS Layer - AuthService).
    /// Quản lý đăng nhập, đổi mật khẩu, đăng ký và khôi phục mật khẩu thông qua câu hỏi bảo mật.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IAuditService? _auditService;

        public AuthService(UserRepository userRepository, IAuditService? auditService = null)
        {
            _userRepository = userRepository;
            _auditService = auditService;
        }

        /// <summary>
        /// Thực hiện đăng nhập vào hệ thống.
        /// Xác thực mật khẩu sử dụng cơ chế so khớp Hash + Salt và hỗ trợ thuật toán Hash cũ (Legacy).
        /// </summary>
        /// <param name="username">Tên đăng nhập.</param>
        /// <param name="password">Mật khẩu chưa mã hóa.</param>
        /// <returns>Đối tượng User nếu thành công, ngược lại trả về null.</returns>
        public User? Login(string username, string password)
        {
            User user = _userRepository.FindByUsername(username);
            if (user == null)
            {
                _auditService?.Log(username, "LOGIN_FAILED", "User", username, "Không tìm thấy tên đăng nhập.");
                return null;
            }

            if (!user.IsActive)
            {
                _auditService?.Log(username, "LOGIN_BLOCKED", "User", username, "Tài khoản đang bị khóa.");
                return null;
            }

            bool passwordMatches = string.IsNullOrEmpty(user.PasswordSalt)
                ? PasswordHasher.VerifyPassword(password, user.PasswordHash)
                : PasswordHasher.VerifyPassword(password, user.PasswordSalt, user.PasswordHash);

            bool legacyPasswordMatches = PasswordHasher.VerifyPassword(password, user.PasswordHash);

            if (!passwordMatches && !legacyPasswordMatches)
            {
                _auditService?.Log(username, "LOGIN_FAILED", "User", username, "Mật khẩu không chính xác.");
                return null;
            }

            // Tự động nâng cấp thuật toán Hash cũ lên Hash + Salt mới khi người dùng đăng nhập thành công
            if (string.IsNullOrEmpty(user.PasswordSalt) || legacyPasswordMatches)
            {
                user.PasswordSalt = PasswordHasher.GenerateSalt();
                user.PasswordHash = PasswordHasher.HashPassword(password, user.PasswordSalt);
                _userRepository.Update(user);
            }

            _auditService?.Log(user.Username, "LOGIN_SUCCESS", "User", user.Username, "Đăng nhập thành công.");
            return user;
        }

        /// <summary>
        /// Kiểm tra câu trả lời bảo mật để khôi phục mật khẩu.
        /// </summary>
        public bool ValidateSecurityAnswer(string username, string answer)
        {
            User user = _userRepository.FindByUsername(username);
            if (user == null || !user.IsActive)
            {
                return false;
            }

            string hashedAnswer = PasswordHasher.HashPassword(answer.Trim().ToLower());
            return user.SecurityAnswerHash == hashedAnswer;
        }

        /// <summary>
        /// Khôi phục mật khẩu (khi người dùng trả lời đúng câu hỏi bảo mật).
        /// </summary>
        public bool ResetPassword(string username, string newPassword)
        {
            if (!PasswordValidator.IsValid(newPassword, out _))
            {
                return false;
            }

            User user = _userRepository.FindByUsername(username);
            if (user == null || !user.IsActive)
            {
                return false;
            }

            user.PasswordSalt = PasswordHasher.GenerateSalt();
            user.PasswordHash = PasswordHasher.HashPassword(newPassword, user.PasswordSalt);
            user.MustChangePassword = false;
            _userRepository.Update(user);
            _auditService?.Log(username, "PASSWORD_RESET_SELF", "User", username, "Khôi phục mật khẩu thành công qua câu hỏi bảo mật.");
            return true;
        }

        /// <summary>
        /// Đổi mật khẩu tài khoản đang đăng nhập.
        /// </summary>
        public bool ChangePassword(string username, string currentPassword, string newPassword)
        {
            if (!PasswordValidator.IsValid(newPassword, out _))
            {
                return false;
            }

            User? user = Login(username, currentPassword);
            if (user == null)
            {
                return false;
            }

            user.PasswordSalt = PasswordHasher.GenerateSalt();
            user.PasswordHash = PasswordHasher.HashPassword(newPassword, user.PasswordSalt);
            user.MustChangePassword = false;
            _userRepository.Update(user);
            _auditService?.Log(username, "PASSWORD_CHANGED", "User", username, "Đổi mật khẩu thành công.");
            return true;
        }

        /// <summary>
        /// Đăng ký tài khoản Khách Hàng tự phục vụ (Self-Registration).
        /// </summary>
        public bool Register(User newUser)
        {
            if (newUser == null)
            {
                return false;
            }

            if (!PasswordValidator.IsValid(newUser.PasswordHash, out _))
            {
                return false;
            }

            if (newUser.Role != UserRole.Customer)
            {
                return false;
            }

            if (_userRepository.UsernameExists(newUser.Username))
            {
                return false;
            }

            newUser.IsActive = true;
            newUser.MustChangePassword = false;
            newUser.PasswordSalt = PasswordHasher.GenerateSalt();
            newUser.PasswordHash = PasswordHasher.HashPassword(newUser.PasswordHash, newUser.PasswordSalt);
            _userRepository.Add(newUser);
            _auditService?.Log(newUser.Username, "CUSTOMER_REGISTERED", "User", newUser.Username, "Khách hàng đăng ký tài khoản tự động.");
            return true;
        }

        /// <summary>
        /// Lấy câu hỏi bảo mật của tài khoản.
        /// </summary>
        public string? GetSecurityQuestion(string username)
        {
            User user = _userRepository.FindByUsername(username);
            if (user == null || !user.IsActive)
            {
                return null;
            }
            return user.SecurityQuestion;
        }
    }
}
