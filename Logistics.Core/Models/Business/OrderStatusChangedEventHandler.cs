using System;

namespace Logistics.Core.Models.Business
{
    // Delegate theo chuan Observer Pattern
    public delegate void OrderStatusChangedEventHandler(Order sender, Common.OrderStatus oldStatus, Common.OrderStatus newStatus);
}
