using Logistics.Core.Configuration;
using Logistics.Core.Models.Business;
using Logistics.Core.Services.Interfaces;

namespace Logistics.Core.Services.Implementations
{
    public class ExpressShippingFeeStrategy : IShippingFeeStrategy
    {
        private readonly BusinessRules _businessRules;

        public ExpressShippingFeeStrategy()
            : this(new BusinessRules())
        {
        }

        public ExpressShippingFeeStrategy(BusinessRules businessRules)
        {
            _businessRules = businessRules;
        }

        public decimal CalculateFee(Order order, decimal baseRatePerKg)
        {
            // Express/Instant: tinh theo khoi luong tinh cuoc, co he so uu tien va phu phi kien nang.
            decimal totalFee = 0m;
            
            // Phu phi cho cac kien hang tren 10kg
            for (int i = 0; i < order.Packages.Count; i++)
            {
                if (order.Packages[i] == null)
                {
                    continue;
                }

                totalFee += (decimal)order.Packages[i].GetChargeableWeight() * baseRatePerKg * 1.5m;
                if (order.Packages[i].ActualWeight > _businessRules.HeavyPackageThresholdKg)
                {
                    totalFee += _businessRules.HeavyPackageSurcharge;
                }
            }

            return totalFee;
        }
    }
}
