using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using TaxCalculator.Common.Interfaces;
using TaxCalculatorAPI.Models;

namespace TaxCalculator.Controllers
{
    public class PostalTaxTypesController : Controller
    {
        private ITaxCalculatorInterface _taxCalculatorInterface;
        public PostalTaxTypesController(ITaxCalculatorInterface taxCalculatorInterface)
        {
            _taxCalculatorInterface = taxCalculatorInterface;
        }
        // GET: PostalTaxTypesController
        public async Task<ActionResult> Index()
        {
            var postalTaxTypes = await _taxCalculatorInterface.GetPostalTaxTypes();
            return View(postalTaxTypes);
        }

        // GET: PostalTaxTypesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _taxCalculatorInterface.GetPostalTaxDetail(id);
            return View(model);
        }

        // GET: PostalTaxTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostalTaxTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                PostalTaxTypes postalTaxTypes = new PostalTaxTypes();
                var check = TryUpdateModelAsync(postalTaxTypes).Result;
                if (check)
                {
                    await _taxCalculatorInterface.CreatePostalTaxType(postalTaxTypes);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PostalTaxTypesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _taxCalculatorInterface.GetPostalTaxDetail(id);
            return View(model);
        }

        // POST: PostalTaxTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                PostalTaxTypes postalTaxTypes = new PostalTaxTypes();
                var check = await TryUpdateModelAsync(postalTaxTypes);
                if (check)
                {
                    await _taxCalculatorInterface.UpdatePostalTaxType(id, postalTaxTypes);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PostalTaxTypesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _taxCalculatorInterface.GetPostalTaxDetail(id);
            return View(model);
        }

        // POST: PostalTaxTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _taxCalculatorInterface.DeletePostalTaxType(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
