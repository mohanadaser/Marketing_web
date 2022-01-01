using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Marketing.Controllers
{
    public class ProductsController : Controller
    {

        private MyDbcontext _context;
        public ProductsController(MyDbcontext context)
        {
            _context = context;

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: ProductsController
        public ActionResult Index()
        {
            var tb = _context.products.Include(c => c.productType).ToList();
            return View(tb);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            ViewBag.typeid =new SelectList(_context.productTypes.ToList(), "typeid", "proytpe");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products,List<IFormFile>files)
        {
            try
            {
                string imgname = "";
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        imgname = file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/mypro", file.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);

                        }

                    }
                    products.proimg = imgname;
                    _context.products.Add(products);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }


                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            var tbpro = _context.products.Include(c => c.productType).Where(c => c.pro_id == id).SingleOrDefault();
            ViewBag.typeid = new SelectList(_context.productTypes.ToList(), "typeid", "proytpe");
            return View(tbpro);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Products products , List<IFormFile> files)
        {
            try
            {
                var tbproduct = _context.products.Include(c=>c.productType).Where(c=>c.pro_id==id).SingleOrDefault();
                string imgname = tbproduct.proimg;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        imgname = file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/mypro", file.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);

                        }

                    }
                    tbproduct.proimg = imgname;
                    tbproduct.proname = products.proname;
                    tbproduct.prodesc = products.prodesc;
                    tbproduct.price = products.price;
                    tbproduct.typeid = products.typeid;
                   
                    _context.SaveChanges();

                        return RedirectToAction(nameof(Index));
                }
                return View();

            }
            catch
            {
                return View();
            }
        }

      

        // POST: ProductsController/Delete/5
      
        public ActionResult Delete(int id)
        {
            try
            {
                var tb= _context.products.Include(c => c.productType).Where(c => c.pro_id == id).SingleOrDefault();
                _context.products.Remove(tb);
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
