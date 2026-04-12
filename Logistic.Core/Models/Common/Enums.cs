namespace Logistic.Core.Models.Common
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum CustomerType
    {
        Standard,
        VIP,
        Enterprise
    }

    public enum Role
    {
        Admin,
        Driver,
        WarehouseManager,
        Dispatcher
    }

    public enum WorkStatus
    {
        Active,
        Resigned,
        OnLeave
    }

    public enum DriverStatus
    {
        Available,
        Busy,
        OffDuty
    }

    public enum PaymentMethod
    {
        COD,
        Banking,
        EWallet,
        Credit
    }

    public enum ServiceType
    {
        Standard,
        Express,
        Instant
    }

    public enum OrderStatus
    {
        Pending,
        InTransit,
        Delivered,
        Cancelled,
        Returned
    }

    public enum TransactionStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }

    public enum IssueType
    {
        Damaged,
        Lost,
        Delay,
        WrongAddress
    }

    public enum ResolutionStatus
    {
        Open,
        Investigating,
        Resolved
    }

    public enum VehicleType
    {
        Motorbike,
        Van,
        Truck_1Ton,
        Container_40ft,
        ColdStorageTruck
    }

    public enum VehicleStatus
    {
        Ready,
        Maintenance,
        InTransit,
        Broken
    }

    public enum WarehouseType
    {
        SortingHub,
        DistributionCenter,
        TransitPoint
    }

    public enum ZoneType
    {
        Normal,
        ColdStorage,
        Hazardous,
        HighValue
    }

    public enum EquipmentType
    {
        Forklift,
        HandScanner,
        Pallet
    }

    public enum EquipmentStatus
    {
        Active,
        Broken,
        UnderMaintenance
    }
}
