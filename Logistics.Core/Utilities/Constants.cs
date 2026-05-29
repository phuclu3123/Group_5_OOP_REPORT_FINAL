namespace Logistics.Core.Utilities
{
    public static class Constants
    {
        // Application info
        public const string AppName = "Logistics Management System";
        public const string AppVersion = "1.0.0";

        // Default credentials
        public const string DefaultAdminUsername = "admin";
        public const string DefaultAdminPassword = "admin123";

        // Validation limits
        public const int MaxPackageWeightKg = 1000;
        public const int MaxPackageVolumeM3 = 100;
        public const double MaxVehicleWeightCapacityKg = 20000;
        public const double MinShippingFee = 5000;

        // Pagination
        public const int DefaultPageSize = 20;

        // Order codes
        public const string OrderPrefix = "ORD";
        public const string DriverPrefix = "DRV";
        public const string VehiclePrefix = "VEH";
        public const string CustomerPrefix = "CUS";
        public const string WarehousePrefix = "WH";
    }
}
