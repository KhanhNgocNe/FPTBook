using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class OrdersDetail
    {
       
        [Key]
        [Column(Order = 0)]
        public int ordersID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int bookID { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public double amount { get; set; }
        public virtual Orders Order { get; set; }
        public virtual Book Book { get; set; }

    }
}