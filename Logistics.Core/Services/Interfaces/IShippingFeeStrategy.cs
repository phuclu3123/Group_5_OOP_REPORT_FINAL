using System;
using Logistics.Core.Models.Business;
using Logistics.Core.Models.Common;

namespace Logistics.Core.Services.Interfaces
{
    public interface IShippingFeeStrategy
    {
        decimal CalculateFee(Order order, decimal baseRatePerKg);
    }
}
