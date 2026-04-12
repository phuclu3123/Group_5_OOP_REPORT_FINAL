namespace Logistic.Core.Interfaces
{
    // ============================================================
    // Interface dich vu xac thuc (Authentication Service)
    // Dinh nghia cac thao tac dang nhap, dang ky, doi/reset mat khau.
    // Ho tro bao mat bang cau hoi bi mat (Security Question).
    // ============================================================
    public interface IAuthService
    {
        // Dang nhap bang username va password, tra ve true neu thanh cong
        bool Login(string username, string password);

        // Dang ky tai khoan moi voi thong tin nguoi dung
        bool Register(string username, string password, string fullName,
                      string securityQuestion, string securityAnswer);

        // Xac thuc cau tra loi bao mat (dung cho quy trinh reset password)
        bool ValidateSecurityAnswer(string username, string securityAnswer);

        // Dat lai mat khau sau khi xac thuc cau hoi bao mat thanh cong
        bool ResetPassword(string username, string newPassword);

        // Doi mat khau (can nhap mat khau cu de xac nhan)
        bool ChangePassword(string username, string oldPassword, string newPassword);

        // Kiem tra nguoi dung da dang nhap chua
        bool IsAuthenticated(string username);

        // Dang xuat
        void Logout(string username);
    }
}
