using Logistics.Core.Models.Account;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Utilities
{
    /// <summary>
    /// Manages the current login session across the application.
    /// </summary>
    public static class SessionManager
    {
        private static User? _currentUser;

        public static bool IsLoggedIn
        {
            get { return _currentUser != null; }
        }

        public static User? CurrentUser
        {
            get { return _currentUser; }
        }

        public static void Login(User user)
        {
            _currentUser = user;
        }

        public static void Logout()
        {
            _currentUser = null;
        }

        public static bool HasRole(UserRole role)
        {
            if (!IsLoggedIn)
            {
                return false;
            }
            return _currentUser != null && _currentUser.Role == role;
        }

        public static bool IsAdmin()
        {
            return HasRole(UserRole.Admin);
        }

        public static bool IsDriver()
        {
            return HasRole(UserRole.Driver);
        }

        public static bool IsDispatcher()
        {
            return HasRole(UserRole.Dispatcher);
        }

        public static bool IsCustomer()
        {
            return HasRole(UserRole.Customer);
        }

        public static bool IsWarehouseStaff()
        {
            return HasRole(UserRole.WarehouseStaff);
        }
    }
}
