/*
    Author: Jason Figueroa
    Description: This is the model for the ProductsCustomers Table & Controller
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bangazonWebApp.Models
{
    public class ProductsCustomer
    {
        [Key]
        public int Id { get; set; }

        public bool LikeStatus { get; set; }

        public int Rating { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
