using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class CartItem
    {
        public Book _cartBook { get; set; }
        public int _cartQuantity { get; set; }
    }

    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void Add(Book _book, int _quantity = 1)
        {
            var item = items.FirstOrDefault(s => s._cartBook.bookID == _book.bookID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _cartBook = _book,
                    _cartQuantity = _quantity
                });
            }
            else
            {
                item._cartQuantity += _quantity;
            }
        }

        public void UpdateQuantity(int id, int _quantity)
        {
            var item = items.Find(s => s._cartBook.bookID == id);
            if (item != null)
            {

                item._cartQuantity = _quantity;
            }
        }

        public double Amount(Book _book)
        {
            var item = items.FirstOrDefault(c => c._cartBook.bookID == _book.bookID);

            var total = item._cartBook.price * item._cartQuantity;
            return total;

        }
        public double Total()
        {
            var total = items.Sum(s => s._cartBook.price * s._cartQuantity);

            return total;
        }
        public double TotalPrice()
        {
            var total = items.Sum(s => s._cartBook.price * s._cartQuantity);
            return total + 5;
        }

        public int TotalQuantity()
        {
            return items.Sum(s => s._cartQuantity);
        }


        public void DeleteCart(int id)
        {
            items.RemoveAll(s => s._cartBook.bookID == id);
        }

        public void ClearCart()
        {
            items.Clear();
        }
    }
}