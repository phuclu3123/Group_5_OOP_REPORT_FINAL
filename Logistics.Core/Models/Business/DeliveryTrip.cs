using System;
using System.Collections.Generic;
using Logistics.Core.Models.Common;
using Newtonsoft.Json;

namespace Logistics.Core.Models.Business
{
    public class DeliveryTrip
    {
        [JsonProperty]
        public string TripID { get; private set; }
        [JsonProperty]
        public string VehicleID { get; private set; }
        [JsonProperty]
        public string DriverID { get; private set; }
        [JsonProperty]
        public List<string> OrderIds { get; private set; }
        [JsonProperty]
        public DeliveryTripStatus Status { get; private set; }
        [JsonProperty]
        public DateTime CreatedAt { get; private set; }
        [JsonProperty]
        public DateTime? StartedAt { get; private set; }
        [JsonProperty]
        public DateTime? CompletedAt { get; private set; }

        public DeliveryTrip(string tripId, string vehicleId, string driverId)
        {
            TripID = tripId;
            VehicleID = vehicleId;
            DriverID = driverId;
            OrderIds = new List<string>();
            Status = DeliveryTripStatus.Planned;
            CreatedAt = DateTime.Now;
        }

        public DeliveryTrip()
        {
            TripID = string.Empty;
            VehicleID = string.Empty;
            DriverID = string.Empty;
            OrderIds = new List<string>();
            CreatedAt = DateTime.Now;
        }

        public void AddOrder(string orderId)
        {
            if (!string.IsNullOrWhiteSpace(orderId) && !OrderIds.Contains(orderId))
            {
                OrderIds.Add(orderId);
            }
        }

        public void Start()
        {
            if (Status == DeliveryTripStatus.Planned)
            {
                Status = DeliveryTripStatus.InProgress;
                StartedAt = DateTime.Now;
            }
        }

        public void Complete()
        {
            if (Status == DeliveryTripStatus.InProgress)
            {
                Status = DeliveryTripStatus.Completed;
                CompletedAt = DateTime.Now;
            }
        }

        public void Cancel()
        {
            if (Status == DeliveryTripStatus.Planned || Status == DeliveryTripStatus.InProgress)
            {
                Status = DeliveryTripStatus.Cancelled;
                CompletedAt = DateTime.Now;
            }
        }
    }
}
