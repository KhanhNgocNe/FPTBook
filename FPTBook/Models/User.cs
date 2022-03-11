using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        //[Required(ErrorMessage = "Enter password, please")]
        [MinLength(8)]
        public string password { get; set; }
        [MinLength(8)]
        //[Required(ErrorMessage = "Confirming password, please")]
        [Compare("password")]
        public string confirmPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "Enter current password, please")]
        [Display(Name ="Current Password")]
        public string currentPassword { get; set; }


        [NotMapped]
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "Enter new password, please")]
        [Display(Name = "New Password")]
        public string newPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "Confirming password, please")]
        [Compare("newPassword",ErrorMessage = "The new password and confirm password do not match")]
        [Display(Name ="Confirm new Password")]
        public string confirmNewPassword { get; set; }

        [Required(ErrorMessage = "Enter Name, please")]
        public string fullname { get; set; }
        [Required(ErrorMessage = "Enter Telephone, please")]
        public int telephone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email address")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@1-1000000\s]+$", ErrorMessage = "Invalid email ")]
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