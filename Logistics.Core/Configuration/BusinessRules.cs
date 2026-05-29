using Logistics.Core.Models.Common;

namespace Logistics.Core.Configuration
{
    public class BusinessRules
    {
        public decimal StandardRatePerKg { get; set; }
        public decimal ExpressRatePerKg { get; set; }
        public decimal InstantRatePerKg { get; set; }
        public decimal VatRate { get; set; }
        public int VndPerPoint { get; set; }
        public int VipPointThreshold { get; set; }
        public int EnterprisePointThreshold { get; set; }
        public decimal VipDiscountRate { get; set; }
        public decimal EnterpriseDiscountRate { get; set; }
        public decimal StandardCreditLimit { get; set; }
        public decimal VipCreditLimit { get; set; }
        public decimal EnterpriseCreditLimit { get; set; }
        public decimal HeavyPackageSurcharge { get; set; }
        public double HeavyPackageThresholdKg { get; set; }

        public BusinessRules()
        {
            StandardRatePerKg = 15000m;
            ExpressRatePerKg = 22000m;
            InstantRatePerKg = 30000m;
            VatRate = 0.08m;
            VndPerPoint = 100000;
            VipPointThreshold = 300;
            EnterprisePointThreshold = 1000;
            VipDiscountRate = 0.05m;
            EnterpriseDiscountRate = 0.08m;
            StandardCreditLimit = 5000000m;
            VipCreditLimit = 30000000m;
            EnterpriseCreditLimit = 100000000m;
            HeavyPackageSurcharge = 50000m;
            HeavyPackageThresholdKg = 10;
        }

        public decimal GetRatePerKg(ServiceType serviceType)
        {
            if (serviceType == ServiceType.Express)
            {
                return ExpressRatePerKg;
            }

            if (serviceType == ServiceType.Instant)
            {
                return InstantRatePerKg;
            }

            return StandardRatePerKg;
        }

        public decimal GetDiscountRate(CustomerType customerType)
        {
            if (customerType == CustomerType.Enterprise)
            {
                return EnterpriseDiscountRate;
            }

            if (customerType == CustomerType.VIP)
            {
                return VipDiscountRate;
            }

            return 0m;
        }

        public decimal GetDefaultCreditLimit(CustomerType customerType)
        {
            if (customerType == CustomerType.Enterprise)
            {
                return EnterpriseCreditLimit;
            }

            if (customerType == CustomerType.VIP)
            {
                return VipCreditLimit;
            }

            return StandardCreditLimit;
        }

        public CustomerType SuggestCustomerType(int loyaltyPoints, decimal totalSpending)
        {
            if (loyaltyPoints >= EnterprisePointThreshold || totalSpending >= EnterpriseCreditLimit)
            {
                return CustomerType.Enterprise;
            }

            if (loyaltyPoints >= VipPointThreshold || totalSpending >= VipCreditLimit)
            {
                return CustomerType.VIP;
            }

            return CustomerType.Standard;
        }

        public int CalculateEarnedPoints(decimal payableAmount, CustomerType customerType)
        {
            if (payableAmount <= 0 || VndPerPoint <= 0)
            {
                return 0;
            }

            int basePoints = (int)(payableAmount / VndPerPoint);
            if (customerType == CustomerType.Enterprise)
            {
                return basePoints * 3;
            }

            if (customerType == CustomerType.VIP)
            {
                return basePoints * 2;
            }

            return basePoints;
        }

        public decimal CalculateDiscount(decimal amount, CustomerType customerType)
        {
            if (amount <= 0)
            {
                return 0m;
            }

            return System.Math.Round(amount * GetDiscountRate(customerType), 0);
        }

        public decimal CalculateVat(decimal taxableAmount)
        {
            if (taxableAmount <= 0)
            {
                return 0m;
            }

            return System.Math.Round(taxableAmount * VatRate, 0);
        }

        public decimal CalculatePayableAmount(decimal shippingFee, CustomerType customerType)
        {
            decimal discount = CalculateDiscount(shippingFee, customerType);
            decimal taxableAmount = shippingFee - discount;
            decimal vat = CalculateVat(taxableAmount);
            return taxableAmount + vat;
        }
    }
}
