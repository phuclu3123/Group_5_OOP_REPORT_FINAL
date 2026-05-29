using Logistics.Core.Models.Business;
using Logistics.Core.Services.Interfaces;

namespace Logistics.Core.Services.Implementations
{
    public class StandardShippingFeeStrategy : IShippingFeeStrategy
    {
        public decimal CalculateFee(Order order, decimal baseRatePerKg)
        {
            // Standard: tinh theo khoi luong tinh cuoc, lay lon hon giua thuc te va quy doi.
            decimal chargeableWeight = 0m;
            for (int i = 0; i < order.Packages.Count; i++)
            {
                if (order.Packages[i] != null)
                {
                    chargeableWeight += (decimal)order.Packages[i].GetChargeableWeight();
                }
            }

            return chargeableWeight * baseRatePerKg;
        }
    }
}
