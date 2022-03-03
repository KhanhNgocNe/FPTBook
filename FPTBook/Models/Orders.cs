using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class Orders
    {
       
        public Orders()
        {
            OrderDetails = new HashSet<OrdersDetail>();
        }
        [Key]
        public int orderID { get; set; }
        public string addressOrder { get; set; }
        public int phoneOrders { get; set; }
        public string username { get; set; }
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime orderDate { get; set; }
        public double total { get; set; }
        public ICollection<OrdersDetail> OrderDetails { get; set; }
        public virtual User User { get; set; }
    }
}