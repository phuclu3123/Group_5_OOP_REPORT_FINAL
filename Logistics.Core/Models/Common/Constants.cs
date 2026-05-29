namespace Logistics.Core.Models.Common
{
    /// <summary>
    /// Hằng số dùng chung trong toàn bộ hệ thống logistics.
    /// </summary>
    public static class AppConstants
    {
        // ===== ORDER =====
        public const int MaxPackagesPerOrder = 50;
        public const decimal DefaultCostPerKg = 5000m;       // VND/kg
        public const decimal DefaultExpressMultiplier = 1.8m; // Hệ số dịch vụ Express

        // ===== LOYALTY POINTS =====
        public const int PointsPerOrder = 10;                 // Điểm thưởng mỗi đơn hàng thành công
        public const int PointsRedemptionRate = 100;          // 100 điểm = 1,000 VND giảm giá

        // ===== DRIVER =====
        public const decimal DefaultBonusPerDelivery = 50_000m;   // VND/chuyến
        public const decimal DefaultFuelAllowance    = 1_500_000m; // VND/tháng

        // ===== VEHICLE =====
        public const double LowFuelThreshold = 20.0;           // % nhiên liệu cảnh báo thấp
        public const double OdometerMaintenanceInterval = 5000; // km giữa 2 lần bảo dưỡng

        // ===== WAREHOUSE =====
        public const double WarehouseHighUtilisationThreshold = 0.90; // 90% đầy → cảnh báo

        // ===== SALARY =====
        public const decimal DefaultAdminManagementAllowance    = 3_000_000m;
        public const decimal DefaultDispatcherRegionAllowance   = 2_000_000m;
        public const decimal DefaultWarehouseHeavyDutyAllowance = 500_000m;
        public const decimal DefaultWarehouseDayShiftAllowance  = 500_000m;
        public const decimal DefaultWarehouseNightShiftAllowance = 1_500_000m;

        // ===== SECURITY =====
        /// <summary>Số lần đăng nhập sai tối đa trước khi khoá tài khoản tạm thời.</summary>
        public const int MaxFailedLoginAttempts = 5;

        // ===== DATA / JSON =====
        public const string DefaultDataFolder = "Data";
        public const string DefaultAdminUsername = "admin";
    }
}
