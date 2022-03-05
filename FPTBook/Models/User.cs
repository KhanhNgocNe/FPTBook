using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Required(ErrorMessage = "Enter username, please")]
        public string username { get; set; }
        [Required(ErrorMessage = "Enter password, please")]
        [MinLength(8)]
        public string password { get; set; }
        [Required(ErrorMessage = "Confirming password, please")]
        [Compare("password")]
        public string confirmPassword { get; set; }
        [Required(ErrorMessage = "Enter Name, please")]
        public string fullname { get; set; }
        [Required(ErrorMessage = "Enter Telephone, please")]
        public int telephone { get; set; }
        [Required(ErrorMessage = "Enter email, please")]
        [Display(Name ="Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Enter address, please")]
        [MaxLength(200)]
        public string address { get; set; }
        [Required(ErrorMessage = "Enter gender, please")]
        public string gender { get; set; }
        [Required(ErrorMessage = "Enter Birthday, please")]
        public DateTime birthday { get; set; }
        public int state { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    
}