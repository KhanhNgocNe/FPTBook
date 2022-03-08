using FPTBook.DB;
using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        public ActionResult EditInfor()
        {
            var useradmin = Session["useradmin"];

            User obj = _db.Users.ToList().Find(s => s.username.Equals(useradmin));

            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfor(User obj)
        {
            User tmp = _db.Users.ToList().Find(s => s.username == obj.username); //find the customer in a list have the same ID with the ID input
            if (tmp.password != obj.password)  //if find out the customer
            {
                tmp.password = GetMD5(obj.password);
                tmp.confirmPassword = GetMD5(obj.confirmPassword);
            }


            tmp.username = obj.username;
            tmp.fullname = obj.fullname;
            tmp.telephone = obj.telephone;
            tmp.email = obj.email;
            tmp.gender = obj.gender;
            tmp.birthday = obj.birthday;
            tmp.address = obj.address;
            tmp.state = obj.state = 1;
            _db.SaveChanges();
            return RedirectToAction("Index", "Admin");

        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}