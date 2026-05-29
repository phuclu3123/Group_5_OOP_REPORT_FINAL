using System;

namespace Logistics.Core.DTOs
{
    public class OrderDTO
    {
        public string OrderId { get; set; } = string.Empty;
        public string TrackingNumber { get; set; } = string.Empty;
        public double TotalWeight { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
        public string SenderID { get; set; } = string.Empty;
        public string ReceiverID { get; set; } = string.Empty;
        public string PickupAddress { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;

        public decimal CodAmount { get; set; }
        public string CodStatus { get; set; } = string.Empty;
        public int FailureCount { get; set; }
        public double TotalVolume { get; set; }
    }
}
