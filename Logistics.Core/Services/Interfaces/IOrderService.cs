using System.Collections.Generic;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(string senderId, string receiverId, string pickupAddress, string deliveryAddress, ServiceType serviceType);
        Package CreatePackage(string trackingNumber, string description, double actualWeight, string dimensions, bool isFragile, decimal value, string itemCategory, string handlingInstructions);
        bool AddPackageToOrder(string trackingNumber, Package package);
        bool UpdatePackage(string trackingNumber, string packageId, string description, double actualWeight, string dimensions, bool isFragile, decimal value, string itemCategory, string handlingInstructions);
        bool RemovePackageFromOrder(string trackingNumber, string packageId);
        bool UpdateOrderStatus(string trackingNumber, OrderStatus newStatus);
        bool CancelOrder(string trackingNumber);
        decimal CalculateOrderCost(string trackingNumber);
        decimal CalculateOrderCost(string trackingNumber, decimal costPerKg);
        List<Package> GetPackagesByOrder(string trackingNumber);
        Order GetOrderById(string trackingNumber);
        List<Order> GetOrdersByCustomer(string customerId);
        List<Order> GetSentOrdersForCurrentCustomer();
        List<Order> GetReceivedOrdersForCurrentCustomer();
        List<Order> GetOrdersByStatus(OrderStatus status);
        List<Order> GetAllOrders();
    }
}
