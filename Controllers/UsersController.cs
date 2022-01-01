using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketing.Controllers
{
    public class UsersController : Controller
    {

        private MyDbcontext _context;
        public UsersController(MyDbcontext context)
        {
            _context = context;

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: UsersController

        public IActionResult userslist()
        {

            return View(_context.users.ToList());
        }
        public ActionResult contactlist()
        {
            
            return View(_context.contacts.ToList());
        }

        [HttpGet]
        public IActionResult contact_us()

        {

            return View();
        }
        [HttpPost]
        public IActionResult contact_us(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.contacts.Add(contact);
                _context.SaveChanges();
                ViewBag.message = "تم ارسال طلبك الى ادارة الموقع";

            }

            return View();
        }
        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult register(Users users)
        {
            var tbuser = _context.users.Where(c => c.email == users.email && c.password==users.password).ToList();
            if (tbuser.Count != 0)
            {
                ViewBag.error = "  هذا الاسم محجوز من قبل مستخدم اخر او هذا المشترك مسجل من قبل";

                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    users.chkemail = false;
                    _context.Add(users);
                    _context.SaveChanges();
                    //ViewBag.message = "تم تسجيل الاشتراك بنجاح" + users.username;
                  
                   HttpContext.Session.SetString("username",users.username);

                    return RedirectToAction("index", "Home");

                }

                return View();

            }
         
        }



        // POST: UsersController/Create
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

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
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

        // GET: UsersController/Delete/5
      
        public ActionResult DeleteContact(int id)
        {

            try
            {
                var tb = _context.contacts.Where(c => c.id == id).FirstOrDefault();
                _context.contacts.Remove(tb);
                _context.SaveChanges();
                return RedirectToAction("contactlist", "users");
            }
            catch
            {
                return RedirectToAction("contactlist", "users");
            }
        }

        public ActionResult Deleteusers(int id, Users users)
        {
            try
            {
                var tb = _context.users.Where(c => c.id == id).FirstOrDefault();
                _context.users.Remove(tb);
                _context.SaveChanges();
                return RedirectToAction("userslist", "users");
            }
            catch
            {
                return RedirectToAction("userslist", "users");
            }
        }


    }
}
