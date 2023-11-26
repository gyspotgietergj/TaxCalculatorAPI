using System.ComponentModel;
using System;
using TaxCalculatorAPI.Models.Enums;

namespace TaxCalculatorAPI.Models
{
    public class PostalTaxTypes
    {
        public int ID { get; set; }
        public string PostalCode { get; set; }
        public TaxTypes TaxType { get; set; }
    }
}
