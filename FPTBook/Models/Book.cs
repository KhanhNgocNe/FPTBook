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
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [Required(ErrorMessage = "Enter ID, please")]
        public int bookID { get; set; }
        [Required(ErrorMessage = "Enter Name, please")]
        public string bookName { get; set; }
        public string description { get; set; }
        [Required(ErrorMessage = "Enter Quantity, please")]
        [Range(0,5000)]
        public int stock_quantity { get; set; }
        [Required(ErrorMessage = "Enter price, please")]
        [Range(3,1000)]
        public double price { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]      
        public string Img { get; set; }
        public int categoryID { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}