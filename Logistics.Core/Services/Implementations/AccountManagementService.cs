using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Logistics.Core.DataAccess.Repositories;
using Logistics.Core.DTOs;
using Logistics.Core.Models.Account;
using Logistics.Core.Models.Actors;
using Logistics.Core.Models.Common;
using Logistics.Core.Services.Interfaces;
using Logistics.Core.Utilities;

namespace Logistics.Core.Services.Implementations
{
    public class AccountManagementService : Interfaces.IAccountManagementService
    {
        private readonly UserRepository _userRepository;
        private readonly IAuditService? _auditService;
        private readonly DriverRepository? _driverRepository;
        private readonly DispatcherRepository? _dispatcherRepository;
        private readonly WarehouseStaffRepository? _warehouseStaffRepository;

        public AccountManagementService(
            UserRepository userRepository,
            IAuditService? auditService = null,
            DriverRepository? driverRepository = null,
            DispatcherRepository? dispatcherRepository = null,
            WarehouseStaffRepository? warehouseStaffRepository = null)
        {
            _userRepository = userRepository;
            _auditService = auditService;
            _driverRepository = driverRepository;
            _dispatcherRepository = dispatcherRepository;
            _warehouseStaffRepository = warehouseStaffRepository;
        }

        public AccountProvisionResultDTO ProvisionEmployeeAccount(UserRole role, Person linkedPerson, string securityQuestion, string securityAnswer)
        {
            if (role == UserRole.Customer)
            {
                return Failure("Khach hang tu dang ky tai man hinh Register, khong cap theo luong nhan vien.");
            }

            if (linkedPerson == null)
            {
                return Failure("Can co ho so nhan vien truoc khi cap tai khoan.");
            }

            string username = BuildUsername(linkedPerson.FullName);
            string baseUsername = username;
            int index = 1;
            while (_userRepository.UsernameExists(username))
            {
                username = baseUsername + index.ToString("D2");
                index++;
            }

            string temporaryPassword = GenerateTemporaryPassword();
            string salt = PasswordHasher.GenerateSalt();
            User user = new User
            {
                Username = username,
                PasswordSalt = salt,
                PasswordHash = PasswordHasher.HashPassword(temporaryPassword, salt),
                Role = role,
                Person = linkedPerson,
                SecurityQuestion = securityQuestion,
                SecurityAnswerHash = PasswordHasher.HashPassword(securityAnswer.Trim().ToLower()),
                MustChangePassword = true,
                IsActive = true
            };

            _userRepository.Add(user);
            _auditService?.Log(GetActor(), "EMPLOYEE_ACCOUNT_PROVISIONED", "User", username, "Provisioned account for " + linkedPerson.FullName + ".");
            return new AccountProvisionResultDTO
            {
                Success = true,
                Username = username,
                TemporaryPassword = temporaryPassword,
                Message = "Da cap tai khoan nhan vien. Mat khau tam thoi chi hien thi mot lan.",
                Account = ToDTO(user)
            };
        }

        public AccountProvisionResultDTO ResetEmployeePassword(string username)
        {
            User user = _userRepository.FindByUsername(username);
            if (user == null)
            {
                return Failure("Khong tim thay tai khoan.");
            }

            if (user.Role == UserRole.Customer)
            {
                return Failure("Khach hang dung luong quen mat khau bang cau hoi bao mat.");
            }

            string temporaryPassword = GenerateTemporaryPassword();
            user.PasswordSalt = PasswordHasher.GenerateSalt();
            user.PasswordHash = PasswordHasher.HashPassword(temporaryPassword, user.PasswordSalt);
            user.MustChangePassword = true;
            user.IsActive = true;
            _userRepository.Update(user);
            _auditService?.Log(GetActor(), "EMPLOYEE_PASSWORD_RESET", "User", user.Username, "Temporary password issued.");

            return new AccountProvisionResultDTO
            {
                Success = true,
                Username = user.Username,
                TemporaryPassword = temporaryPassword,
                Message = "Da reset mat khau tam thoi. Nhan vien phai doi mat khau khi dang nhap.",
                Account = ToDTO(user)
            };
        }

        public bool DeactivateAccount(string username)
        {
            User user = _userRepository.FindByUsername(username);
            if (user == null)
            {
                return false;
            }

            user.IsActive = false;
            _userRepository.Update(user);
            _auditService?.Log(GetActor(), "ACCOUNT_DEACTIVATED", "User", user.Username, "Account deactivated.");
            return true;
        }

        public bool ReactivateAccount(string username)
        {
            User user = _userRepository.FindByUsername(username);
            if (user == null)
            {
                return false;
            }

            user.IsActive = true;
            _userRepository.Update(user);
            _auditService?.Log(GetActor(), "ACCOUNT_REACTIVATED", "User", user.Username, "Account reactivated.");
            return true;
        }

        public List<UserDTO> GetAllAccounts()
        {
            List<UserDTO> result = new List<UserDTO>();
            List<User> users = _userRepository.GetAll();
            for (int i = 0; i < users.Count; i++)
            {
                result.Add(ToDTO(users[i]));
            }

            return result;
        }

        public bool UpdateProfile(string username, string email, string phone, string addressLine, string avatarPath)
        {
            if (string.IsNullOrWhiteSpace(email) || email.IndexOf('@') < 0 || email.IndexOf('.') < 0)
            {
                throw new ArgumentException("Email không đúng định dạng.");
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Số điện thoại không được để trống.");
            }

            string trimmedPhone = phone.Trim();
            if (trimmedPhone.Length < 10 || trimmedPhone.Length > 11 || trimmedPhone[0] != '0')
            {
                throw new ArgumentException("Số điện thoại phải bắt đầu bằng số 0 và có từ 10 đến 11 chữ số.");
            }

            for (int i = 0; i < trimmedPhone.Length; i++)
            {
                if (!char.IsDigit(trimmedPhone[i]))
                {
                    throw new ArgumentException("Số điện thoại chỉ được chứa các ký tự số.");
                }
            }

            if (string.IsNullOrWhiteSpace(addressLine))
            {
                throw new ArgumentException("Địa chỉ không được để trống.");
            }

            User user = _userRepository.FindByUsername(username);
            if (user == null)
            {
                return false;
            }

            user.AvatarPath = avatarPath;
            if (user.Person != null)
            {
                user.Person.UpdateEmail(email.Trim());
                user.Person.UpdatePhoneNumber(trimmedPhone);
                user.Person.UpdateAddress(new Address(addressLine.Trim(), "", "", "", "", "Việt Nam"));
            }

            _userRepository.Update(user);

            // Đồng bộ dữ liệu sang các bảng nhân sự (dal) khác
            if (user.Person != null)
            {
                Staff? staff = user.Person as Staff;
                if (staff != null)
                {
                    if (user.Role == UserRole.Driver && _driverRepository != null)
                    {
                        Driver driver = _driverRepository.GetById(staff.StaffID);
                        if (driver != null)
                        {
                            driver.UpdateEmail(email.Trim());
                            driver.UpdatePhoneNumber(trimmedPhone);
                            driver.UpdateAddress(new Address(addressLine.Trim(), "", "", "", "", "Việt Nam"));
                            _driverRepository.Update(driver);
                        }
                    }
                    else if (user.Role == UserRole.Dispatcher && _dispatcherRepository != null)
                    {
                        Dispatcher dispatcher = _dispatcherRepository.GetById(staff.StaffID);
                        if (dispatcher != null)
                        {
                            dispatcher.UpdateEmail(email.Trim());
                            dispatcher.UpdatePhoneNumber(trimmedPhone);
                            dispatcher.UpdateAddress(new Address(addressLine.Trim(), "", "", "", "", "Việt Nam"));
                            _dispatcherRepository.Update(dispatcher);
                        }
                    }
                    else if (user.Role == UserRole.WarehouseStaff && _warehouseStaffRepository != null)
                    {
                        WarehouseStaff ws = _warehouseStaffRepository.GetById(staff.StaffID);
                        if (ws != null)
                        {
                            ws.UpdateEmail(email.Trim());
                            ws.UpdatePhoneNumber(trimmedPhone);
                            ws.UpdateAddress(new Address(addressLine.Trim(), "", "", "", "", "Việt Nam"));
                            _warehouseStaffRepository.Update(ws);
                        }
                    }
                }
            }

            _auditService?.Log(username, "PROFILE_UPDATED", "User", username, "Cập nhật thông tin cá nhân qua BUS.");
            return true;
        }

        private static AccountProvisionResultDTO Failure(string message)
        {
            return new AccountProvisionResultDTO
            {
                Success = false,
                Message = message
            };
        }

        private static string GetActor()
        {
            return SessionManager.CurrentUser != null ? SessionManager.CurrentUser.Username : "system";
        }

        private static UserDTO ToDTO(User user)
        {
            UserDTO dto = new UserDTO();
            dto.UserId = user.UserId;
            dto.Username = user.Username;
            dto.Role = user.Role.ToString();
            dto.IsActive = user.IsActive;
            dto.MustChangePassword = user.MustChangePassword;
            dto.CreatedAt = user.CreatedAt;
            dto.AvatarPath = user.AvatarPath;

            if (user.Person != null)
            {
                dto.LinkedPersonId = user.Person.Id;
                dto.LinkedPersonName = user.Person.FullName;
            }

            return dto;
        }

        private static string BuildUsername(string fullName)
        {
            string normalized = RemoveDiacritics(fullName).Trim().ToLowerInvariant();
            string[] parts = normalized.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                return "employee";
            }

            string username = parts[parts.Length - 1];
            for (int i = 0; i < parts.Length - 1; i++)
            {
                username += parts[i][0];
            }

            return username;
        }

        private static string RemoveDiacritics(string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < normalized.Length; i++)
            {
                System.Globalization.UnicodeCategory category = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(normalized[i]);
                if (category != System.Globalization.UnicodeCategory.NonSpacingMark && char.IsLetterOrDigit(normalized[i]) || normalized[i] == ' ')
                {
                    builder.Append(normalized[i]);
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        private static string GenerateTemporaryPassword()
        {
            const string lower = "abcdefghijkmnopqrstuvwxyz";
            const string upper = "ABCDEFGHJKLMNPQRSTUVWXYZ";
            const string digits = "23456789";
            const string special = "@#$%";
            string all = lower + upper + digits + special;

            char[] chars = new char[12];
            chars[0] = Pick(upper);
            chars[1] = Pick(lower);
            chars[2] = Pick(digits);
            chars[3] = Pick(special);
            for (int i = 4; i < chars.Length; i++)
            {
                chars[i] = Pick(all);
            }

            Shuffle(chars);
            return new string(chars);
        }

        private static char Pick(string source)
        {
            return source[RandomNumberGenerator.GetInt32(source.Length)];
        }

        private static void Shuffle(char[] chars)
        {
            for (int i = chars.Length - 1; i > 0; i--)
            {
                int j = RandomNumberGenerator.GetInt32(i + 1);
                char temp = chars[i];
                chars[i] = chars[j];
                chars[j] = temp;
            }
        }
    }
}
