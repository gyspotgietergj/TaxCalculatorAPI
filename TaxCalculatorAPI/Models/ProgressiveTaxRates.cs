namespace TaxCalculatorAPI.Models
{
    public class ProgressiveTaxRates
    {
        public ProgressiveTaxRates(int _Order, decimal _Rate, decimal _Lower, decimal _Higher)
        {
            Order = _Order;
            Rate = _Rate;
            Lower = _Lower;
            Higher = _Higher;
        }
        public int Order { get; set; }
        public decimal Rate { get; set; }
        public decimal Lower { get; set; }
        public decimal Higher { get; set; }
    }
}
