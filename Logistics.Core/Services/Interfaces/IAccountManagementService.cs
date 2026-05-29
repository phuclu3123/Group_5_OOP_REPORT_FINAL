using System.Collections.Generic;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Account;
using Logistics.Core.Models.Actors;

namespace Logistics.Core.Services.Interfaces
{
    public interface IAccountManagementService
    {
        AccountProvisionResultDTO ProvisionEmployeeAccount(UserRole role, Person linkedPerson, string securityQuestion, string securityAnswer);
        AccountProvisionResultDTO ResetEmployeePassword(string username);
        bool DeactivateAccount(string username);
        bool ReactivateAccount(string username);
        List<UserDTO> GetAllAccounts();
        bool UpdateProfile(string username, string email, string phone, string addressLine, string avatarPath);
    }
}
