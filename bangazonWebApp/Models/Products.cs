using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bangazonWebApp.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product Category")]
        public string CategoryId { get; set; }
        public Categories Categories { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

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

        public virtual ICollection<OrdersProducts> OrdersProducts { get; set; }

        public virtual ICollection<Recommendations> Recommendations { get; set; }

        public virtual ICollection<ProductsCustomers> ProductsCustomers { get; set; }
    }
}
