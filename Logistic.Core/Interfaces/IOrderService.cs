using System.Collections.Generic;
using Logistic.Core.Models.Business;
using Logistic.Core.Models.Common;

namespace Logistic.Core.Interfaces
{
    // ============================================================
    // Interface dich vu quan ly don hang (Order Service)
    // Dinh nghia cac thao tac CRUD va nghiep vu lien quan den don hang.
    // Ap dung nguyen tac Abstraction: an chi tiet xu ly, chi lo contract.
    // ============================================================
    public interface IOrderService
    {
        // Tao don hang moi va tra ve doi tuong Order da tao
        Order CreateOrder(string trackingNumber, string senderId, string receiverId,
                          string pickupAddress, string deliveryAddress, ServiceType serviceType);

        // Tim don hang theo ma tracking
        Order GetOrderByTracking(string trackingNumber);

        // Cap nhat trang thai don hang
        bool UpdateOrderStatus(string trackingNumber, OrderStatus newStatus, string changedBy, string note);

        // Lay toan bo danh sach don hang
        List<Order> GetAllOrders();

        // Loc danh sach don hang theo trang thai
        List<Order> GetOrdersByStatus(OrderStatus status);

        // Huy don hang (chi cho phep khi trang thai la Pending)
        bool CancelOrder(string trackingNumber, string cancelledBy, string reason);

        // Tinh phi van chuyen cho don hang
        decimal CalculateShippingFee(Order order, double distance);
    }
}
