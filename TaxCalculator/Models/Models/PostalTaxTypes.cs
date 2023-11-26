using System.ComponentModel;
using System;
using TaxCalculatorAPI.Models.Enums;

namespace TaxCalculatorAPI.Models
{
    public class PostalTaxTypes
    {
        public int ID { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [DisplayName("Tax Type")]
        public TaxTypes TaxType { get; set; }
    }
}
