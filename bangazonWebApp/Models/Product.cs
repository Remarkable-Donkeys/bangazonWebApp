//Author: Max Wolf

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bangazonWebApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Photo { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [Display(Name = "Local Delivery Available?")]
        public bool DeliverLocal { get; set; }

        public virtual ICollection<OrderProduct> OrderProduct { get; set; }

        public virtual ICollection<Recommendation> Recommendation { get; set; }

        public virtual ICollection<ProductCustomer> ProductCustomer { get; set; }
    }
}
