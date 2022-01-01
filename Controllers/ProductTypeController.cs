using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketing.Controllers
{
    public class ProductTypeController : Controller
    {

        private MyDbcontext _context;
        public ProductTypeController(MyDbcontext context)
        {
            _context = context;

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: ProductTypeController
        public ActionResult Index()
        {
            ViewBag.protype = _context.productTypes.ToList();
            return View();
        }

        // GET: ProductTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductTypeController/Create
      

        // POST: ProductTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductType productType)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _context.Add(productType);
                    _context.SaveChanges();

                   
                }

               return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ProductTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductTypeController/Edit/5
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

        // GET: ProductTypeController/Delete/5
      
        public ActionResult Delete(int id)
        {
            try
            {
                var tb = _context.productTypes.Where(c => c.typeid == id).FirstOrDefault();
                _context.Remove(tb);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
