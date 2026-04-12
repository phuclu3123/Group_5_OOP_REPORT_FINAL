using System.Collections.Generic;
using Logistic.Core.Models.Business;

namespace Logistic.Core.Interfaces
{
    // ============================================================
    // Interface dich vu giao hang (Delivery Service)
    // Dinh nghia cac thao tac gan don cho xe, kiem tra suc chua,
    // va quan ly trang thai giao hang.
    // ============================================================
    public interface IDeliveryService
    {
        // Gan don hang vao phuong tien - kiem tra suc chua truoc khi gan
        bool AssignOrderToVehicle(string orderId, string vehicleId, double weight, double volume);

        // Kiem tra phuong tien co du suc chua cho khoi luong va the tich chi dinh
        bool CheckVehicleCapacity(string vehicleId, double weight, double volume);

        // Lay danh sach phuong tien san sang (trang thai Ready)
        List<Vehicle> GetAvailableVehicles();

        // Danh dau don hang da giao thanh cong
        bool CompleteDelivery(string orderId, string vehicleId);

        // Lay trang thai giao hang cua don
        string GetDeliveryStatus(string orderId);
    }
}
