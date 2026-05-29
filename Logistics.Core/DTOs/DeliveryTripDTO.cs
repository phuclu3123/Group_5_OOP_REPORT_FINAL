namespace Logistics.Core.DTOs
{
    public class DeliveryTripDTO
    {
        public string TripID { get; set; } = string.Empty;
        public string VehicleID { get; set; } = string.Empty;
        public string DriverID { get; set; } = string.Empty;
        public int OrderCount { get; set; }
        public string OrderList { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}
