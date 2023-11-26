using System.ComponentModel;

namespace TaxCalculatorAPI.Models
{
    public class TaxQuery
    {
        public int Id { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        public DateTime RequestDate { get; set; }
        [DisplayName("Annual Income")]
        public decimal Income { get; set; }

        [DisplayName("Tax Payable")]
        public decimal TaxAmount { get; set; }
    }
}
