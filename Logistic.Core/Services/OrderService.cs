using System;
using System.Collections.Generic;
using Logistic.Core.Models.Business;
using Logistic.Core.Models.Common;
using Logistic.Core.Interfaces;
using Logistic.Core.Mappings;
using Logistic.Core.Validations;

namespace Logistic.Core.Services
{
    // ============================================================
    // Dich vu quan ly don hang (Order Service)
    // Implement IOrderService: xu ly toan bo logic lien quan don hang.
    // Su dung:
    // - Strategy Pattern: thay doi cach tinh phi ship linh hoat
    // - Singleton (InMemoryDataStore): luu tru du lieu in-memory
    // - Dependency Injection: nhan IShippingFeeStrategy qua constructor
    // ============================================================
    public class OrderService : IOrderService
    {
        // ===== DEPENDENCIES =====

        // Kho du lieu in-memory (Singleton)
        private InMemoryDataStore _dataStore;

        // Chien luoc tinh phi van chuyen (Strategy Pattern)
        private IShippingFeeStrategy _shippingFeeStrategy;

        // Validator kiem tra don hang truoc khi xu ly
        private OrderValidator _orderValidator;

        // ===== CONSTRUCTOR =====
        // Nhan dependency qua constructor (Dependency Injection / Dependency Inversion Principle)
        public OrderService(IShippingFeeStrategy shippingFeeStrategy)
        {
            _dataStore = InMemoryDataStore.GetInstance();
            _shippingFeeStrategy = shippingFeeStrategy;
            _orderValidator = new OrderValidator();
        }

        // ===== IORDESERVICE IMPLEMENTATION =====

        // Tao don hang moi va luu vao kho du lieu
        public Order CreateOrder(string trackingNumber, string senderId, string receiverId,
                                 string pickupAddress, string deliveryAddress, ServiceType serviceType)
        {
            // Kiem tra ma tracking da ton tai chua
            Order existingOrder = _dataStore.GetOrderByTracking(trackingNumber);
            if (existingOrder != null)
            {
                return null; // Da ton tai, khong tao moi
            }

            // Tao don hang moi
            Order newOrder = new Order(trackingNumber, senderId, receiverId,
                                       pickupAddress, deliveryAddress, serviceType);

            // Luu vao kho du lieu
            _dataStore.AddOrder(newOrder);
            return newOrder;
        }

        // Tim don hang theo ma tracking
        public Order GetOrderByTracking(string trackingNumber)
        {
            return _dataStore.GetOrderByTracking(trackingNumber);
        }

        // Cap nhat trang thai don hang
        public bool UpdateOrderStatus(string trackingNumber, OrderStatus newStatus,
                                       string changedBy, string note)
        {
            Order order = _dataStore.GetOrderByTracking(trackingNumber);
            if (order == null)
            {
                return false;
            }

            // Kiem tra chuyen trang thai hop le
            if (!IsValidStatusTransition(order.CurrentStatus, newStatus))
            {
                return false;
            }

            order.UpdateStatus(newStatus, changedBy, note);
            return true;
        }

        // Lay toan bo danh sach don hang
        public List<Order> GetAllOrders()
        {
            return _dataStore.GetAllOrders();
        }

        // Loc danh sach don hang theo trang thai
        public List<Order> GetOrdersByStatus(OrderStatus status)
        {
            List<Order> allOrders = _dataStore.GetAllOrders();
            List<Order> filteredOrders = new List<Order>();
            for (int i = 0; i < allOrders.Count; i++)
            {
                if (allOrders[i].CurrentStatus == status)
                {
                    filteredOrders.Add(allOrders[i]);
                }
            }
            return filteredOrders;
        }

        // Huy don hang (chi cho phep khi trang thai la Pending)
        public bool CancelOrder(string trackingNumber, string cancelledBy, string reason)
        {
            Order order = _dataStore.GetOrderByTracking(trackingNumber);
            if (order == null)
            {
                return false;
            }

            // Chi cho phep huy don o trang thai Pending
            if (order.CurrentStatus != OrderStatus.Pending)
            {
                return false;
            }

            order.UpdateStatus(OrderStatus.Cancelled, cancelledBy, "Huy don: " + reason);
            return true;
        }

        // Tinh phi van chuyen cho don hang (su dung Strategy Pattern)
        public decimal CalculateShippingFee(Order order, double distance)
        {
            if (order == null || distance <= 0)
            {
                return 0;
            }
            return _shippingFeeStrategy.CalculateFee(order.TotalWeight, distance, order.ServiceType);
        }

        // ===== PRIVATE HELPERS =====

        // Kiem tra chuyen trang thai co hop le khong
        // VD: Khong the tu Delivered chuyen sang Pending
        private bool IsValidStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {
            // Cancelled va Returned la trang thai cuoi, khong the chuyen tiep
            if (currentStatus == OrderStatus.Cancelled || currentStatus == OrderStatus.Returned)
            {
                return false;
            }

            // Delivered chi co the chuyen sang Returned
            if (currentStatus == OrderStatus.Delivered && newStatus != OrderStatus.Returned)
            {
                return false;
            }

            return true;
        }

        // ===== ADDITIONAL METHODS =====

        // Thay doi chien luoc tinh phi van chuyen tai runtime (Strategy Pattern)
        public void SetShippingFeeStrategy(IShippingFeeStrategy newStrategy)
        {
            _shippingFeeStrategy = newStrategy;
        }

        // Lay ten chien luoc tinh phi hien tai
        public string GetCurrentStrategyName()
        {
            return _shippingFeeStrategy.GetStrategyName();
        }

        // Validate don hang truoc khi tao
        public ValidationResult ValidateOrder(Order order)
        {
            return _orderValidator.Validate(order);
        }
    }
}
