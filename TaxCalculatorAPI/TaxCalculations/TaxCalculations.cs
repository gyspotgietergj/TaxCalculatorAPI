using Microsoft.Net.Http.Headers;
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

        private static decimal CalculateProgressive(decimal income)
        {
            throw new NotImplementedException();
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
