using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPTBook.DB;
using FPTBook.Models;

namespace FPTBook.Controllers
{
    public class OrdersDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrdersDetails
        public ActionResult Index()
        {
            var ordersDetails = db.OrdersDetails.Include(o => o.Book);
            return View(ordersDetails.ToList());
        }

        // GET: OrdersDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDetail ordersDetail = db.OrdersDetails.Find(id);
            if (ordersDetail == null)
            {
                return HttpNotFound();
            }
            return View(ordersDetail);
        }

        // GET: OrdersDetails/Create
        public ActionResult Create()
        {
            ViewBag.bookID = new SelectList(db.Books, "bookID", "bookName");
            return View();
        }

        // POST: OrdersDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ordersdetailID,ordersID,bookID,price,quantity,amount")] OrdersDetail ordersDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrdersDetails.Add(ordersDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bookID = new SelectList(db.Books, "bookID", "bookName", ordersDetail.bookID);
            return View(ordersDetail);
        }

        // GET: OrdersDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDetail ordersDetail = db.OrdersDetails.Find(id);
            if (ordersDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookID = new SelectList(db.Books, "bookID", "bookName", ordersDetail.bookID);
            return View(ordersDetail);
        }

        // POST: OrdersDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ordersdetailID,ordersID,bookID,price,quantity,amount")] OrdersDetail ordersDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordersDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookID = new SelectList(db.Books, "bookID", "bookName", ordersDetail.bookID);
            return View(ordersDetail);
        }

        // GET: OrdersDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDetail ordersDetail = db.OrdersDetails.Find(id);
            if (ordersDetail == null)
            {
                return HttpNotFound();
            }
            return View(ordersDetail);
        }

        // POST: OrdersDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdersDetail ordersDetail = db.OrdersDetails.Find(id);
            db.OrdersDetails.Remove(ordersDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
