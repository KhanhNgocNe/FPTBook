using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FPTBook.DB;
using FPTBook.Models;

namespace FPTBook.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.username == user.username);
                if (check == null)
                {
                    user.password = GetMD5(user.password);
                    user.confirmPassword = GetMD5(user.confirmPassword);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    user.state = 0;
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    ViewBag.error = "User already exists";
                    return View();
                }


            }
            return View();

        }
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var _Password = GetMD5(password);
                var data = _db.Users.Where(s => s.username.Equals(username) && s.password.Equals(_Password)).ToList();
                if (data.Count() > 0)
                {
                    if (data.FirstOrDefault().state == 0)
                    {
                        Session["username"] = data.FirstOrDefault().username;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //add session
                        Session["useradmin"] = data.FirstOrDefault().username;
                        return RedirectToAction("Index", "Admin");
                    }

                }
                else
                {
                    //ViewBag.Error = "User name and Password wrong";
                    ViewBag.ErrorMessage = "User name and Password wrong";
                    //return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditInfor()
        {
            var username = Session["username"];
            
            User obj = _db.Users.ToList().Find(s => s.username.Equals(username));
           
            if (obj == null)
            {
                return RedirectToAction("Login");
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfor(User obj)
        {
            if (ModelState.IsValid)
            {
                User tmp = _db.Users.ToList().Find(s => s.username == obj.username);
                tmp.username = obj.username;
                tmp.fullname = obj.fullname;
                tmp.telephone = obj.telephone;
                tmp.email = obj.email;
                tmp.gender = obj.gender;
                tmp.birthday = obj.birthday;
                tmp.address = obj.address;
                tmp.state = obj.state = 0;
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View("EditInfor");
            
        }
        public ActionResult ChangePassword()
        {
            var user = Session["username"];
            if (user == null)
            {
                Response.Write("<script>alert('Please sign in to continue!'); window.location='/Users/Login'</script>");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(User _user)
        {
            var user = Session["username"];

            User objAccount = _db.Users.ToList().Find(p => p.username.Equals(user) && p.password.Equals(GetMD5(_user.currentPassword)));
            if (objAccount == null)
            {
                ViewBag.Error = "Current Password is incorrect";
                return View();
            }
            if (_user.newPassword != _user.confirmNewPassword)
            {
                ViewBag.Confirm = "The new password and confirmation new password do not match.";
            }
            else
            {
                objAccount.password = GetMD5(_user.newPassword);
                objAccount.confirmPassword = objAccount.password;
                _db.Users.Attach(objAccount);
                _db.Entry(objAccount).Property(a => a.password).IsModified = true;
                _db.SaveChanges();

                ViewBag.Success = "Password Change successfully";
            }
            return View();
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


    }
}
