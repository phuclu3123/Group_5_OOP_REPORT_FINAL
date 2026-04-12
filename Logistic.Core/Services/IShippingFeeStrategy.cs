using Logistic.Core.Models.Common;

namespace Logistic.Core.Services
{
    // ============================================================
    // STRATEGY PATTERN: Interface chien luoc tinh phi van chuyen
    // Cho phep thay doi thuat toan tinh phi linhh hoat tai runtime
    // ma khong can sua doi code cua OrderService.
    // - StandardShippingFeeStrategy: phi thuong
    // - ExpressShippingFeeStrategy: phi nhanh
    // De mo rong: chi can tao class moi implement IShippingFeeStrategy
    // ============================================================
    public interface IShippingFeeStrategy
    {
        // Tinh phi van chuyen dua tren khoi luong, khoang cach va loai dich vu
        // weight: khoi luong (kg)
        // distance: khoang cach (km)
        // serviceType: loai dich vu (Standard, Express, Instant)
        // Tra ve: phi van chuyen (VND)
        decimal CalculateFee(double weight, double distance, ServiceType serviceType);

        // Lay ten chien luoc tinh phi
        string GetStrategyName();
    }
}
