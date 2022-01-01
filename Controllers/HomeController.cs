using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Marketing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace Marketing.Controllers
{
    public class HomeController : Controller
    {
        private MyDbcontext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,MyDbcontext context)
        {
            _context = context;
            _logger = logger;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        
        //public async Task<IActionResult> index(int pageindex=1)
        //{

        //    var pro = _context.products.AsNoTracking().OrderBy(c => c.pro_id);
        //    var model =await PagingList.CreateAsync(pro, 4, pageindex);
        //    return View(model);
        //}

        public IActionResult category(int id)
        {
            var tbl = _context.products.Where(c => c.typeid == id).Include(c => c.productType).ToList();
            return View(tbl);
        }
        public IActionResult index()
        {

            return View(_context.products.ToList());
        }
      
        // get product details
        public IActionResult details(int id)
        {
           
            var tbpro = _context.products.Include(c => c.productType).Where(c => c.pro_id == id).SingleOrDefault();
            ViewBag.typeid = new SelectList(_context.productTypes.ToList(), "typeid", "proytpe");
            return View(tbpro);

        }
        // login//// logout
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Home");
        }

        [HttpGet]
        public IActionResult login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(Users users)

        {
            var tbusers = _context.users.Where(c => c.username == users.username && c.password == users.password).FirstOrDefault();
            if (users.username == null && users.password == null)
            {
                ViewBag.message = "";
                return View();

            }
            else if (tbusers == null)
            {
                ViewBag.message = "يوجد خطا فى اسم المستخدم او كلمة المرور او لم يتم تسجيل الاشتراك فى الموقع";

                return View();
            }

            // عمل session للمستخدمين
            else
            {

                HttpContext.Session.SetString("username", tbusers.username);
                HttpContext.Session.SetString("password", tbusers.password);
                HttpContext.Session.SetString("userid", tbusers.id.ToString());

                return RedirectToAction("index", "Home");
            }


        }
        // Shopping cart

       
        // addtocarts
      

        public IActionResult  addtocart(int id)
        {
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("login", "Home");
            }

                string filefolder = Path.GetTempPath();
            string filepath = filefolder + "cart.txt";
            if (!Directory.Exists(filefolder))
            {
                Directory.CreateDirectory(filefolder);

            }
            if (!System.IO.File.Exists(filepath))
            {

                System.IO.File.Create(filepath).Close();
            }
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.AppendAllText(filepath, id.ToString() + ",");

            }

            return RedirectToAction("viewcarts", "home");
        }

        // delete carts
        public IActionResult deletecart(int id)
        {
            string filefolder = Path.GetTempPath();
            string filepath = filefolder + "cart.txt";
            if (System.IO.File.Exists(filepath))
            {
                string allvalues = System.IO.File.ReadAllText(filepath);
                var elements = allvalues.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                string newvalues = allvalues;
                foreach (var item in elements)
                {
                    if (item.ToString() == id.ToString())
                    {
                        var pos = allvalues.IndexOf(item.ToString());
                        newvalues = newvalues.Remove(pos, item.ToString().Length);

                    }

                }

                if (System.IO.File.Exists(filepath))
                {

                    System.IO.File.WriteAllText(filepath, newvalues);

                }

            }

            return RedirectToAction("viewcarts", "home");


        }

        public IActionResult viewcarts()
        {
            string filefolder = Path.GetTempPath();
            string filepath = filefolder + "cart.txt";
            List<listproducts> listproducts = new List<listproducts>();
            if (System.IO.File.Exists(filepath))
            {
                string getvalues = System.IO.File.ReadAllText(filepath);
                ViewBag.cartscount = "getvalues";
                var elements = getvalues.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in elements)
                {
                    var tbl = _context.products.SingleOrDefault(c => c.pro_id.ToString()== item.ToString());

                    listproducts.Add(new listproducts
                    {
                        proid =int.Parse( item.ToString()),
                        proname=tbl.proname,
                        proimage=tbl.proimg,
                        qty=1,
                        price = (decimal)tbl.price,
                        total= (decimal)(1 * tbl.price)

                    });
  

                }

                //HttpContext.Session.SetString("count", listproducts.Count.ToString());
                TempData["count"] = listproducts.Count;

            }
             

            var model = new ProductsCarts
            {

                listproducts = listproducts

            };


            return View(model);

        }
        //اضافة اوردر المنتجات
        public IActionResult saveorder(ProductsCarts productsCarts)
        {
            //Random r = new Random();
            //int code = r.Next(1, 20000);
            try
            {
   if (ModelState.IsValid)
            {
        if (productsCarts.listproducts!=null)
            {

             foreach (var item in productsCarts.listproducts)
            {
                
              Shopping_Order order = new Shopping_Order();
            order.code_order = getcodeNo();
            order.phone = productsCarts.tel;
            order.name = productsCarts.name;
            order.address = productsCarts.address;
            order.time_order = DateTime.Now;
                order.pr_oname = item.proname;
                order.pro_id = item.proid;
                order.qty = item.qty;
                order.price = item.price;
                //order.pro_image = item.proimage;
                _context.shopping_Orders.Add(order);
                _context.SaveChanges();
              
                }
                TempData["code"] = getcodeNo();
            }
          

            return RedirectToAction("done", "home");
            }
            }

            catch
            {
                return RedirectToAction("NoPage", "home");
            }

            return RedirectToAction("NoPage", "home");

        }
        // للحصول على رقم الطلبيه يوجد ايضا طريقه اخرى لها

        public string getcodeNo()
        {
            int rowcount = _context.shopping_Orders.ToList().Count() + 1;

            return rowcount.ToString("000");
        }


        // end of shopping cart

        public IActionResult done()
        {

            return View();

        }


        // البحث عن المنتجات واصنافها
        [HttpGet]
        public IActionResult search()
        {

            return View();
        }
        [HttpPost]
        public IActionResult search(string searchname)
        {
            if (searchname == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var result = _context.products.Where(c => c.proname.Contains(searchname)

              || c.productType.proytpe.Contains(searchname)
              || c.prodesc.Contains(searchname)).ToList();

            return View(result);
        }

        // edit account users

        public IActionResult editaccount()
        {
            var userid =int.Parse (HttpContext.Session.GetString("userid"));
            var tbuser = _context.users.Where(c => c.id == userid).SingleOrDefault();
            return View(tbuser);
        }
        [HttpPost]
        public IActionResult editaccount(Users users)
        {
            if (ModelState.IsValid)
            {
              _context.Update(users);
            _context.SaveChanges();
                ViewBag.message = "تم تعديل حسابك بنجاح";
            return View();
            }
            ViewBag.error = "يوجد خطأ اعد المحاوله من الممكن ان يكون الرقم السرى محجوز";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult  NoPage()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
