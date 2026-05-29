using Logistics.Core.Models.Account;

namespace Logistics.Core.Services.Interfaces
{
    public interface IAuthService
    {
        User? Login(string username, string password);
        bool ValidateSecurityAnswer(string username, string answer);
        bool ResetPassword(string username, string newPassword);
        bool ChangePassword(string username, string currentPassword, string newPassword);
        bool Register(User newUser);
        string? GetSecurityQuestion(string username);
    }
}
