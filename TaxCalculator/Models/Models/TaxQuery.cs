namespace TaxCalculatorAPI.Models
{
    public class TaxQuery
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal Income { get; set; }
    }
}
