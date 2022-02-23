using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }
        [Key]
        [Required(ErrorMessage="Enter ID, please")]
        public int categoryID { get; set; }
        [Required(ErrorMessage = "Enter Name, please")]
        public string categoryName { get; set; }
        [StringLength(100)]
        public string description { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}