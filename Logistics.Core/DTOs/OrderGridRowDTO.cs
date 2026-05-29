namespace Logistics.Core.DTOs
{
    public class OrderGridRowDTO
    {
        public string TrackingNumber { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public double TotalWeight { get; set; }
        public decimal TotalCost { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StatusCode { get; set; } = string.Empty;
        public string AssignedDriver { get; set; } = string.Empty;
        public string AssignedVehicle { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}
