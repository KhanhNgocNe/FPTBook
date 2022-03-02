using FPTBook.DB;
using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var books = db.Books.ToList();
            return View(books);
        }
        [HttpPost]
        public ActionResult Index(string searchstring)
        {
           
            List<Book> data = new List<Book>();
            data = db.Books.Where(x => x.bookName.Contains(searchstring)).ToList();
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}