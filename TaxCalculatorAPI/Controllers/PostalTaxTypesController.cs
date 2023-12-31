﻿using System;
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
    public class PostalTaxTypesController : ControllerBase
    {
        private TaxCalculatorAPIContext _context;

        public PostalTaxTypesController(TaxCalculatorAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostalTaxTypes>>> GetPostalTaxTypes()
        {
            if (_context.PostalTaxTypes == null)
            {
                return NotFound();
            }
            return await _context.PostalTaxTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostalTaxTypes>> GetPostalTaxTypes(int id)
        {
            if (_context.PostalTaxTypes == null)
            {
                return NotFound();
            }
            var postalTaxTypes = await _context.PostalTaxTypes.FindAsync(id);

            if (postalTaxTypes == null)
            {
                return NotFound();
            }

            return postalTaxTypes;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostalTaxTypes(int id, PostalTaxTypes postalTaxTypes)
        {
            _context.Entry(postalTaxTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostalTaxTypesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PostalTaxTypes>> PostPostalTaxTypes(PostalTaxTypes postalTaxTypes)
        {
            if (_context.PostalTaxTypes == null)
            {
                return Problem("Entity set 'TaxCalculatorAPIContext.PostalTaxTypes'  is null.");
            }

            var checkExist = _context.PostalTaxTypes.Where(x => x.PostalCode == postalTaxTypes.PostalCode).FirstOrDefault();
            if (checkExist != null)
            {
                return BadRequest("Postal Code already exists: " + checkExist.ID);
            }

            _context.PostalTaxTypes.Add(postalTaxTypes);
            var id = await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostalTaxTypes", new { id }, postalTaxTypes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostalTaxTypes(int id)
        {
            if (_context.PostalTaxTypes == null)
            {
                return NotFound();
            }
            var postalTaxTypes = await _context.PostalTaxTypes.FindAsync(id);
            if (postalTaxTypes == null)
            {
                return NotFound();
            }

            _context.PostalTaxTypes.Remove(postalTaxTypes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostalTaxTypesExists(int id)
        {
            return (_context.PostalTaxTypes?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        [HttpGet]
        [Route("GetTaxableAmount/{postalCode}/{income}")]
        public async Task<ActionResult<decimal>> GetTaxQuery(int postalCode, decimal income)
        {
            if (_context.TaxQuery == null)
            {
                return NotFound();
            }
            if (postalCode < 0 || income <= 0)
            {
                return BadRequest("Invalid Parameters. Income must be more than 0 and postal code must have a value.");
            }

            var postalTaxType = _context.PostalTaxTypes.Where(x => x.ID == postalCode).FirstOrDefault();
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

            return TaxCalculations.TaxCalculations.CalculateTax(taxQuery.Income, (TaxTypes)postalTaxType.TaxType);
        }
    }
}
