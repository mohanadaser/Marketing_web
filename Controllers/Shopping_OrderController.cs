using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketing.Controllers
{
    public class Shopping_OrderController : Controller
    {

        private MyDbcontext _context;

        public Shopping_OrderController(MyDbcontext context)
        {
            _context = context;

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Shopping_OrderController
        public ActionResult Index()
        {
            return View(_context.shopping_Orders.ToList());
        }

        // GET: Shopping_OrderController/Details/5
        public ActionResult Details(int id)
        {
            var tbl = _context.shopping_Orders.Where(c => c.shopping_id == id);
            foreach (var item in tbl)
            {
                ViewBag.ordercode = item.code_order;
                ViewBag.name = item.name;
                ViewBag.address = item.address;
                ViewBag.tel = item.phone;

            }
            return View(tbl);
        }

        // GET: Shopping_OrderController/Create
        public ActionResult Create()
        {
          
            return View();
        }

        // POST: Shopping_OrderController/Create
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

        // GET: Shopping_OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shopping_OrderController/Edit/5
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

        // GET: Shopping_OrderController/Delete/5
      
        // POST: Shopping_OrderController/Delete/5
       
        public ActionResult Delete(int id)
        {
            try
            {
                var tb = _context.shopping_Orders.Where(c => c.shopping_id == id).SingleOrDefault();
                _context.shopping_Orders.Remove(tb);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
