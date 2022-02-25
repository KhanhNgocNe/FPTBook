using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class Book
    {
        public Book()
        {
            OrderDetails = new HashSet<OrdersDetail>();
        }
        [Key]
        [Required(ErrorMessage = "Enter ID, please")]
        public int bookID { get; set; }
        [Required(ErrorMessage = "Enter Name, please")]
        public string bookName { get; set; }
        public string description { get; set; }
        [Required(ErrorMessage = "Enter Quantity, please")]
        public int stock_quantity { get; set; }
        [Required(ErrorMessage = "Enter price, please")]
        public double price { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string Img { get; set; }
        public int categoryID { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<OrdersDetail> OrderDetails { get; set; }
    }
}