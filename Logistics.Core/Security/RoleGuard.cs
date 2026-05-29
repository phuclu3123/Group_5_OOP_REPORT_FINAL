using Logistics.Core.Models.Account;
using Logistics.Core.Utilities;

namespace Logistics.Core.Security
{
    /// <summary>
    /// Centralized role checks for UI and service-entry guards.
    /// </summary>
    public static class RoleGuard
    {
        public static bool RequireRole(UserRole requiredRole)
        {
            return SessionManager.IsLoggedIn && SessionManager.HasRole(requiredRole);
        }

        public static bool HasAnyRole(params UserRole[] roles)
        {
            if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
            {
                return false;
            }

            for (int i = 0; i < roles.Length; i++)
            {
                if (SessionManager.CurrentUser.Role == roles[i])
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsAdmin()
        {
            return SessionManager.IsAdmin();
        }

        public static bool IsDriver()
        {
            return SessionManager.IsDriver();
        }

        public static bool IsDispatcher()
        {
            return SessionManager.IsDispatcher();
        }

        public static bool IsWarehouseStaff()
        {
            return SessionManager.IsWarehouseStaff();
        }

        public static bool IsCustomer()
        {
            return SessionManager.IsCustomer();
        }

        public static bool CanDispatch()
        {
            return IsAdmin() || IsDispatcher();
        }

        public static bool CanManageStaff()
        {
            return IsAdmin();
        }

        public static bool CanManageCustomers()
        {
            return IsAdmin() || IsDispatcher();
        }

        public static bool CanManageWarehouse()
        {
            return IsAdmin() || IsWarehouseStaff();
        }

        public static bool CanManageOrders()
        {
            return IsAdmin() || IsDispatcher() || IsCustomer();
        }

        public static bool CanViewOrders()
        {
            return IsAdmin() || IsDispatcher() || IsCustomer() || IsDriver();
        }

        public static bool CanManageVehicles()
        {
            return IsAdmin() || IsDispatcher();
        }

        public static bool CanManageMaintenance()
        {
            return IsAdmin() || IsDispatcher();
        }

        public static bool CanViewDocuments()
        {
            return IsAdmin() || IsDispatcher() || IsWarehouseStaff();
        }

        public static bool CanViewReports()
        {
            return IsAdmin() || IsDispatcher();
        }

        public static bool CanOpenAdminArea()
        {
            return IsAdmin();
        }
    }
}
