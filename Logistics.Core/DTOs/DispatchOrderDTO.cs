namespace Logistics.Core.DTOs
{
    public class DispatchOrderDTO
    {
        public string TrackingNumber { get; set; } = string.Empty;
        public string SenderID { get; set; } = string.Empty;
        public string ReceiverID { get; set; } = string.Empty;
        public double TotalWeight { get; set; }
        public string Status { get; set; } = string.Empty;
        public string AssignedDriverID { get; set; } = string.Empty;
        public string AssignedVehicleID { get; set; } = string.Empty;
    }
}
