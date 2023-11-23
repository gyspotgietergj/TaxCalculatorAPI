using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxCalculatorAPI.Models;

namespace TaxCalculatorAPI.Data
{
    public class TaxCalculatorAPIContext : DbContext
    {
        public TaxCalculatorAPIContext (DbContextOptions<TaxCalculatorAPIContext> options)
            : base(options)
        {
        }

        public DbSet<TaxCalculatorAPI.Models.PostalTaxTypes> PostalTaxTypes { get; set; } = default!;

        public DbSet<TaxCalculatorAPI.Models.TaxQuery> TaxQuery { get; set; } = default!;
    }
}
