using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculatorAPI.Data;
using TaxCalculatorAPI.Models;
using TaxCalculatorAPI.Models.Enums;

namespace TaxCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxQueriesController : ControllerBase
    {
        private readonly TaxCalculatorAPIContext _context;

        public TaxQueriesController(TaxCalculatorAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<decimal>> GetTaxQuery(string postalCode, decimal income)
        {
            if (_context.TaxQuery == null)
            {
                return NotFound();
            }
            if (string.IsNullOrWhiteSpace(postalCode) || income <= 0)
            {
                return BadRequest("Invalid Parameters. Income must be more than 0 and postal code must have a value.");
            }

            var postalTaxType = _context.PostalTaxTypes.Where(x => x.PostalCode == postalCode).FirstOrDefault();
            if (postalTaxType == null)
            {
                return NotFound("Postal code not available for tax calculation");
            }
            var taxQuery = new TaxQuery();
            taxQuery.Income = income;
            taxQuery.PostalCode = postalCode;
            taxQuery.RequestDate = DateTime.Now;
            _context.TaxQuery.Add(taxQuery);
            await _context.SaveChangesAsync();

            return TaxCalculations.TaxCalculations.CalculateTax(income, (TaxTypes)postalTaxType.TaxType);
        }
    }
}
