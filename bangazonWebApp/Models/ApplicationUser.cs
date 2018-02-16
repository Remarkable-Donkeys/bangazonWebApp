//Author: Max Wolf
//Purpose: Model for Application Users

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace bangazonWebApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Phone { get; set; }

        //AppUserId is a foreign key in the products table, this collection is for lazy loading of the Products
        public virtual ICollection<Products> Products { get; set; }

        //AppUserId is a foreign key in the payments table, this collection is for lazy loading of the Payments
        public virtual ICollection<Payments> Payments { get; set; }

        //AppUserId is a foreign key in the orders table, this collection is for lazy loading of the Orders
        public virtual ICollection<Orders> Orders { get; set; }

        //Lazy loading Likes and Ratings from ProductsCustomers
        public virtual ICollection<ProductsCustomers> LikesAndRatings { get; set; }

        //Lazy loading recommenders and recommendees
        public virtual ICollection<Recommendations> Recommenders { get; set; }

        public virtual ICollection<Recommendations> Recommendees { get; set; }


    }
}
