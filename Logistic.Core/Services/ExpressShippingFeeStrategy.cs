using Logistic.Core.Models.Common;

namespace Logistic.Core.Services
{
    // ============================================================
    // STRATEGY PATTERN: Chien luoc tinh phi van chuyen nhanh (Express)
    // Cong thuc: Phi = StandardFee x 1.5 + phu phi uu tien 30,000 VND
    // Su dung Composition: goi StandardShippingFeeStrategy ben trong
    // de tai su dung logic tinh phi co ban (DRY - Don't Repeat Yourself).
    // ============================================================
    public class ExpressShippingFeeStrategy : IShippingFeeStrategy
    {
        // ===== HANG SO =====

        // He so nhan phi nhanh so voi phi tieu chuan
        private const decimal EXPRESS_MULTIPLIER = 1.5m;

        // Phu phi uu tien co dinh (VND)
        private const decimal PRIORITY_SURCHARGE = 30000m;

        // Composition: su dung strategy tieu chuan de tinh phi co ban
        private StandardShippingFeeStrategy _standardStrategy;

        // Constructor: khoi tao strategy tieu chuan ben trong
        public ExpressShippingFeeStrategy()
        {
            _standardStrategy = new StandardShippingFeeStrategy();
        }

        // Tinh phi van chuyen nhanh = Phi tieu chuan x 1.5 + phu phi uu tien
        public decimal CalculateFee(double weight, double distance, ServiceType serviceType)
        {
            // Lay phi co ban tu strategy tieu chuan
            decimal baseFee = _standardStrategy.CalculateFee(weight, distance, serviceType);

            // Ap dung he so nhanh va phu phi uu tien
            decimal expressFee = baseFee * EXPRESS_MULTIPLIER + PRIORITY_SURCHARGE;
            return expressFee;
        }

        // Lay ten chien luoc
        public string GetStrategyName()
        {
            return "Express Shipping Fee (x1.5 + Priority Surcharge)";
        }
    }
}
