using System.Collections.Generic;
using Logistic.Core.Interfaces;

namespace Logistic.Core.Services
{
    // ============================================================
    // Dich vu xac thuc (Authentication Service)
    // Implement IAuthService: xu ly dang nhap, dang ky, doi mat khau,
    // reset mat khau qua cau hoi bao mat.
    // Su dung Dictionary in-memory de luu tru thong tin nguoi dung.
    // ============================================================
    public class AuthService : IAuthService
    {
        // ===== INNER CLASS: Thong tin tai khoan nguoi dung =====
        // Encapsulation: chi AuthService moi truy cap duoc thong tin nay
        private class UserAccount
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string FullName { get; set; }
            public string SecurityQuestion { get; set; }
            public string SecurityAnswer { get; set; }
            public bool IsLoggedIn { get; set; }

            public UserAccount(string username, string password, string fullName,
                               string securityQuestion, string securityAnswer)
            {
                Username = username;
                Password = password;
                FullName = fullName;
                SecurityQuestion = securityQuestion;
                SecurityAnswer = securityAnswer;
                IsLoggedIn = false;
            }
        }

        // ===== FIELDS =====

        // Kho luu tru tai khoan nguoi dung (key: username)
        private Dictionary<string, UserAccount> _accounts;

        // Danh sach username da xac thuc cau hoi bao mat (cho phep reset password)
        private List<string> _verifiedForReset;

        // ===== CONSTRUCTOR =====
        public AuthService()
        {
            _accounts = new Dictionary<string, UserAccount>();
            _verifiedForReset = new List<string>();
        }

        // ===== IAUTHSERVICE IMPLEMENTATION =====

        // Dang nhap bang username va password
        public bool Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Kiem tra tai khoan ton tai
            if (!_accounts.ContainsKey(username))
            {
                return false;
            }

            UserAccount account = _accounts[username];

            // Kiem tra mat khau
            if (account.Password != password)
            {
                return false;
            }

            account.IsLoggedIn = true;
            return true;
        }

        // Dang ky tai khoan moi
        public bool Register(string username, string password, string fullName,
                             string securityQuestion, string securityAnswer)
        {
            // Validate thong tin dau vao
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            if (string.IsNullOrEmpty(fullName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(securityQuestion) || string.IsNullOrEmpty(securityAnswer))
            {
                return false;
            }

            // Kiem tra username da ton tai chua
            if (_accounts.ContainsKey(username))
            {
                return false;
            }

            // Kiem tra do dai mat khau toi thieu
            if (password.Length < 6)
            {
                return false;
            }

            // Tao tai khoan moi
            UserAccount newAccount = new UserAccount(username, password, fullName,
                                                      securityQuestion, securityAnswer);
            _accounts.Add(username, newAccount);
            return true;
        }

        // Xac thuc cau tra loi bao mat (buoc 1 trong quy trinh reset password)
        public bool ValidateSecurityAnswer(string username, string securityAnswer)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(securityAnswer))
            {
                return false;
            }

            if (!_accounts.ContainsKey(username))
            {
                return false;
            }

            UserAccount account = _accounts[username];

            // So sanh cau tra loi (khong phan biet hoa thuong)
            if (account.SecurityAnswer.ToLower() != securityAnswer.ToLower())
            {
                return false;
            }

            // Danh dau da xac thuc thanh cong, cho phep reset password
            if (!IsVerifiedForReset(username))
            {
                _verifiedForReset.Add(username);
            }
            return true;
        }

        // Dat lai mat khau (buoc 2: phai xac thuc cau hoi bao mat truoc)
        public bool ResetPassword(string username, string newPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
            {
                return false;
            }

            // Kiem tra da xac thuc cau hoi bao mat chua
            if (!IsVerifiedForReset(username))
            {
                return false;
            }

            if (!_accounts.ContainsKey(username))
            {
                return false;
            }

            // Kiem tra do dai mat khau moi
            if (newPassword.Length < 6)
            {
                return false;
            }

            // Cap nhat mat khau moi
            UserAccount account = _accounts[username];
            account.Password = newPassword;

            // Xoa trang thai da xac thuc (1 lan dung duy nhat)
            RemoveVerifiedForReset(username);
            return true;
        }

        // Doi mat khau (can mat khau cu de xac nhan)
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                return false;
            }

            if (!_accounts.ContainsKey(username))
            {
                return false;
            }

            UserAccount account = _accounts[username];

            // Xac nhan mat khau cu
            if (account.Password != oldPassword)
            {
                return false;
            }

            // Kiem tra mat khau moi khong trung mat khau cu
            if (oldPassword == newPassword)
            {
                return false;
            }

            // Kiem tra do dai mat khau moi
            if (newPassword.Length < 6)
            {
                return false;
            }

            account.Password = newPassword;
            return true;
        }

        // Kiem tra nguoi dung da dang nhap chua
        public bool IsAuthenticated(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            if (!_accounts.ContainsKey(username))
            {
                return false;
            }
            return _accounts[username].IsLoggedIn;
        }

        // Dang xuat
        public void Logout(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return;
            }
            if (_accounts.ContainsKey(username))
            {
                _accounts[username].IsLoggedIn = false;
            }
        }

        // ===== PRIVATE HELPERS =====

        // Kiem tra username da duoc xac thuc cho reset password chua
        private bool IsVerifiedForReset(string username)
        {
            for (int i = 0; i < _verifiedForReset.Count; i++)
            {
                if (_verifiedForReset[i] == username)
                {
                    return true;
                }
            }
            return false;
        }

        // Xoa trang thai xac thuc reset password
        private void RemoveVerifiedForReset(string username)
        {
            for (int i = 0; i < _verifiedForReset.Count; i++)
            {
                if (_verifiedForReset[i] == username)
                {
                    _verifiedForReset.RemoveAt(i);
                    return;
                }
            }
        }

        // ===== ADDITIONAL METHODS =====

        // Lay cau hoi bao mat cua tai khoan (dung khi hien thi cho nguoi dung)
        public string GetSecurityQuestion(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return "";
            }
            if (!_accounts.ContainsKey(username))
            {
                return "";
            }
            return _accounts[username].SecurityQuestion;
        }

        // Lay so luong tai khoan
        public int GetAccountCount()
        {
            return _accounts.Count;
        }
    }
}
