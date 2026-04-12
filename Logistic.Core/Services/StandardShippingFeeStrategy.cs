using Logistic.Core.Models.Common;

namespace Logistic.Core.Services
{
    // ============================================================
    // STRATEGY PATTERN: Chien luoc tinh phi van chuyen tieu chuan
    // Cong thuc: Phi = (weight x donGia/kg) + (distance x donGia/km)
    // Don gia/kg: 15,000 VND | Don gia/km: 2,000 VND
    // He so nhan them theo loai dich vu (Express x1.5, Instant x2.0)
    // ============================================================
    public class StandardShippingFeeStrategy : IShippingFeeStrategy
    {
        // ===== HANG SO DON GIA =====

        // Don gia van chuyen theo khoi luong (VND/kg)
        private const decimal RATE_PER_KG = 15000m;

        // Don gia van chuyen theo khoang cach (VND/km)
        private const decimal RATE_PER_KM = 2000m;

        // Tinh phi van chuyen theo cong thuc tieu chuan
        public decimal CalculateFee(double weight, double distance, ServiceType serviceType)
        {
            // Tinh phi co ban = (khoi luong x don gia/kg) + (khoang cach x don gia/km)
            decimal baseFee = (decimal)weight * RATE_PER_KG + (decimal)distance * RATE_PER_KM;

            // Ap dung he so nhan theo loai dich vu
            decimal multiplier = 1.0m;
            if (serviceType == ServiceType.Express)
            {
                multiplier = 1.5m;
            }
            else if (serviceType == ServiceType.Instant)
            {
                multiplier = 2.0m;
            }

            decimal totalFee = baseFee * multiplier;
            return totalFee;
        }

        // Lay ten chien luoc
        public string GetStrategyName()
        {
            return "Standard Shipping Fee";
        }
    }
}
