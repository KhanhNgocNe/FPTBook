using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["username"] == Session["username"] && Session["useradmin"] != null)
            {
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