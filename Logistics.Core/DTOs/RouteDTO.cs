namespace Logistics.Core.DTOs
{
    public class RouteDTO
    {
        public string RouteId { get; set; } = string.Empty;
        public string OriginWarehouse { get; set; } = string.Empty;
        public string DestinationWarehouse { get; set; } = string.Empty;
        public double DistanceKm { get; set; }
        public double EstimatedHours { get; set; }
        public string AssignedVehicleId { get; set; } = string.Empty;
        public string AssignedDriverId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
