using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Common.Interfaces;

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
        public ActionResult Index()
        {
            var model = _taxCalculatorInterface.GetPostalTaxTypes().Result;
            return View(model);
        }

        // GET: PostalTaxTypesController/Details/5
        public ActionResult Details(int id)
        {
            var model = _taxCalculatorInterface.GetPostalTaxDetail(id).Result;
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostalTaxTypesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostalTaxTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostalTaxTypesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostalTaxTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
