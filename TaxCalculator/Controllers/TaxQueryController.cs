using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaxCalculator.Common.Interfaces;
using TaxCalculatorAPI.Models;

namespace TaxCalculator.Controllers
{
    public class TaxQueryController : Controller
    {
        private ITaxCalculatorInterface _taxCalculatorInterface;
        public TaxQueryController(ITaxCalculatorInterface taxCalculatorInterface)
        {
            _taxCalculatorInterface = taxCalculatorInterface;
        }
        public async Task<ActionResult> Index()
        {
            var PostalCodes = await _taxCalculatorInterface.GetPostalTaxTypes();
            ViewBag.PostalCodes = PostalCodes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(IFormCollection collection)
        {
            try
            {
                TaxQuery query = new TaxQuery();
                var check = TryUpdateModelAsync(query).Result;
                if (check)
                {
                    query.TaxAmount = Math.Round(await _taxCalculatorInterface.CalculateTax(query), 2);
                }
                var PostalCodes = await _taxCalculatorInterface.GetPostalTaxTypes();
                ViewBag.PostalCodes = PostalCodes;
                return View(query);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
