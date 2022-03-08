using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class Order
    {
       
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public int orderID { get; set; }
        public string addressOrder { get; set; }
        public int phoneOrders { get; set; }
        //[DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime orderDate { get; set; }
        public double total { get; set; }
        public string Username { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual User User { get; set; }
    }
}