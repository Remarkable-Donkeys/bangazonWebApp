using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bangazonWebApp.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
