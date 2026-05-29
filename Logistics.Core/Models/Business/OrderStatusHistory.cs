using System;

namespace Logistics.Core.Models.Business
{
    public class OrderStatusHistory
    {
        public string HistoryId { get; set; } = Guid.NewGuid().ToString();
        
        public Common.OrderStatus PreviousStatus { get; set; }
        public Common.OrderStatus NewStatus { get; set; }
        
        public DateTime ChangedAt { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ChangedBy { get; set; } = string.Empty;
        
        public override string ToString() 
        {
            return "[" + ChangedAt.ToString("dd/MM HH:mm") + "] " + NewStatus.ToString() + " - " + Description;
        }
    }
}
