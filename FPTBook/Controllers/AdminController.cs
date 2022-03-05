using FPTBook.DB;
using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        
        // GET: Admin
        public ActionResult Index()
        {
            
            if (Session["username"] == Session["username"] && Session["useradmin"] != null)
            {
                var totalBook = _db.Books.Count();
                var totalCate = _db.Categories.Count();
                var totalUser = _db.Users.Count();
                var totalOrder = _db.Orders.Count();


                ViewBag.TotalBooks = totalBook;
                ViewBag.TotalCategories = totalCate;
                ViewBag.TotalUsers = totalUser;
                ViewBag.TotalOrders = totalOrder;
                return View();
            }
            return RedirectToAction("Error");
            
        }
        
        public ActionResult Error()
        {
            return View();
        }
    }
}