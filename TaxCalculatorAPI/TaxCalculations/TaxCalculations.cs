using Microsoft.Net.Http.Headers;
using TaxCalculatorAPI.Models;
using TaxCalculatorAPI.Models.Enums;

namespace TaxCalculatorAPI.TaxCalculations
{
    public static class TaxCalculations
    {


        public static decimal CalculateTax(decimal income, TaxTypes taxType)
        {
            switch (taxType)
            {
                case TaxTypes.FlatValue:
                    return CalculateFlatValue(income);
                case TaxTypes.FlatRate:
                    return CalculateFlatRate(income);
                case TaxTypes.Progressive:
                    return CalculateProgressive(income);
                default:
                    throw new Exception("Tax Type not supported");
            }
        }

        private static List<ProgressiveTaxRates> BuildProgressiveTaxRates()
        {
            var rates = new List<ProgressiveTaxRates>
            {
                new ProgressiveTaxRates(1,0.1m, 0m, 8350m),
                new ProgressiveTaxRates(2,0.15m, 8351m, 33950m),
                new ProgressiveTaxRates(3,0.25m, 33951m, 82250m),
                new ProgressiveTaxRates(4,0.28m, 82251m, 171550m),
                new ProgressiveTaxRates(5,0.33m, 171551m, 372950m),
                new ProgressiveTaxRates(6,0.35m, 372950m, decimal.MaxValue )
            };
            return rates;
        }

        private static decimal CalculateProgressive(decimal income)
        {
            var ProgressiveTaxRates = BuildProgressiveTaxRates();
            ProgressiveTaxRates = ProgressiveTaxRates.OrderBy(x => x.Order).ToList();
            decimal result = 0m;
            foreach (var rate in ProgressiveTaxRates)
            {
                if (income > rate.Lower)
                {
                    var amountToTax = Math.Min(rate.Higher - rate.Lower, income - rate.Lower);
                    var amountTaxed = amountToTax * rate.Rate;
                    result += amountTaxed;
                }
            }
            return result;
        }

        private static decimal CalculateFlatRate(decimal income)
        {
            return income * 0.175m;
        }

        private static decimal CalculateFlatValue(decimal income)
        {
            if (income >= 200000)
            {
                return 10000;
            }
            return income * 0.05m;
        }
    }
}
