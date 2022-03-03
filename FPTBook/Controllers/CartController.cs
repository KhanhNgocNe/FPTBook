using FPTBook.DB;
using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private ApplicationDbContext _db = new ApplicationDbContext();
        //GET: ShoppingCart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;

        }

        public ActionResult AddtoCart(int id)
        {
            var book = _db.Books.SingleOrDefault(c => c.bookID == id);
            if (book != null)
            {
                GetCart().Add(book);
            }
            return RedirectToAction("ViewCart", "Cart");
        }

        public ActionResult UpdateQuantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int idbook = int.Parse(form["bookID"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.UpdateQuantity(idbook, quantity);
            return RedirectToAction("ViewCart", "MyCarts");
        }

        public ActionResult ViewCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ViewCart", "Cart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);

        }
        public ActionResult Delete(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.DeleteCart(id);
            return RedirectToAction("ViewCart", "Cart");
        }
        public PartialViewResult BagCart()
        {
            int totalitem = 0;

            Cart cart = Session["Cart"] as Cart;

            if (cart != null)
                totalitem = cart.Total();
            ViewBag.TotalItem = totalitem;

            return PartialView("BagCart");
        }
        public ActionResult Checkout(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Orders _orders = new Orders();
                _orders.orderDate = DateTime.Now;
                _orders.username = form["username"];
                _orders.addressOrder = form["username"];
                _orders.phoneOrders = Convert.ToInt32(form["phoneOrders"]);
                _orders.total = Convert.ToInt32(form["total"]);
                _db.Orders.Add(_orders);

                foreach (var item in cart.Items)
                {
                    OrdersDetail orderDetail = new OrdersDetail();
                    orderDetail.ordersID = _orders.orderID;
                    orderDetail.bookID = item._cartBook.bookID;
                    orderDetail.quantity = item._cartQuantity;
                    orderDetail.amount = item._cartBook.price * item._cartQuantity;
                    var book = _db.Books.SingleOrDefault(s => s.bookID == orderDetail.bookID);

                    book.stock_quantity -= orderDetail.quantity;
                    _db.Books.Attach(book);
                    _db.Entry(book).Property(c => c.stock_quantity).IsModified = true;

                    _db.OrdersDetails.Add(orderDetail);
                }

                _db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckoutSuccess", "Cart", new { id = _orders.orderID });
            }
            catch
            {
                return Content("Error checkout, Check information again");
            }
        }
        public ActionResult CheckoutSuccess(int? id)
        {
            if (Session["username"] != null)
            {
                var order = _db.Orders.Find(id);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            return View("ErrorCart");
        }

        public ActionResult OrderHistory(string id)
        {
            if (Session["username"] != null)
            {
                var orderHis = _db.Orders.ToList().Where(c => c.username == id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (orderHis == null)
                {
                    return HttpNotFound();
                }
                return View(orderHis);
            }
            return View("ErrorCart");
        }
    }
}


