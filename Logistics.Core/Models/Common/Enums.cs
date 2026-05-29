namespace Logistics.Core.Models.Common
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
        Pending = 0,
        InTransit = 1,
        Delivered = 2,
        Cancelled = 3,
        Returned = 4,
        Failed = 5,
        WaitingPickup = 6,
        PickedUp = 7,
        ArrivedAtWarehouse = 8,
        Sorting = 9,
        ReadyForDispatch = 10,
        OutForDelivery = 11,
        DeliveryAttemptFailed = 12,
        Returning = 13
    }

    public enum PackageStatus
    {
        Created = 0,
        WaitingPickup = 1,
        PickedUp = 2,
        InWarehouse = 3,
        Sorting = 4,
        LoadedForDelivery = 5,
        InTransit = 6,
        Delivered = 7,
        FailedDelivery = 8,
        Returning = 9,
        Returned = 10,
        Lost = 11,
        Damaged = 12
    }

    public enum CodStatus
    {
        None,
        Pending,
        CollectedByDriver,
        Settled
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

    public enum DeliveryTripStatus
    {
        Planned,
        InProgress,
        Completed,
        Cancelled
    }

    public enum WarehouseType
    {
        SortingHub,
        DistributionCenter,
        TransitPoint,
        FulfillmentCenter,
        SortingCenter,
        TransshipmentPoint,
        ColdStorage
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

    public enum InventoryTransactionType
    {
        CheckIn,
        CheckOut
    }
}
