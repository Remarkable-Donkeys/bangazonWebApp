// Author: Jason Figueroa
// Purpose: Seeds products & categories to their respective tables in db

/*****************************************************************/
/* If seeded data is no longer required this file can be deleted */
/*****************************************************************/

using System;
using System.Linq;
using bangazonWebApp.Data;
using bangazonWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace bangazonWebApp
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            /**************************/
            /* Seeding Category Table */
            /**************************/
            if (!context.CategoryType.Any())
            {
                context.CategoryType.Add(new Category { CategoryType = "Jewelry & Accessories" });
                context.CategoryType.Add(new Category { CategoryType = "Clothing & Shoes" });
                context.CategoryType.Add(new Category { CategoryType = "Home & Living" });
                context.CategoryType.Add(new Category { CategoryType = "Arts & Collectibles" });

                context.SaveChanges();
            }

            /*************************/
            /* Seeding Product Table */
            /*************************/
            if (!context.Product.Any())
            {
                ApplicationUser user1 = userManager.FindByNameAsync("jsmith@email.com").Result;
                
                int productCategoryId = (from ct in context.CategoryType
                                         where ct.CategoryType.Equals("Clothing & Shoes")
                                         select ct.Id).Single();

                context.Product.Add(new Product {
                    User = user1,                    
                    Name = "Knit Hat",
                    Description = "A beautifully knitted hat for a toddler girl.",
                    CategoryId = productCategoryId,
                    Status = true,
                    Price = 25.00,
                    DateCreated = DateTime.Now,
                    Quantity = 2,
                    Photo = "",
                    City = "Nashville",
                    State = "TN",
                    DeliverLocal = false,
                });

                context.Product.Add(new Product
                {
                    User = user1,
                    Name = "Knit Scarf",
                    Description = "A beautifully knitted scarf for a toddler girl.",
                    CategoryId = productCategoryId,
                    Status = true,
                    Price = 25.00,
                    DateCreated = DateTime.Now,
                    Quantity = 4,
                    Photo = "",
                    City = "Nashville",
                    State = "TN",
                    DeliverLocal = false,
                });

                context.Product.Add(new Product
                {
                    User = user1,
                    Name = "Knit Mittens",
                    Description = "Beautifully knitted mittens for a toddler girl.",
                    CategoryId = productCategoryId,
                    Status = true,
                    Price = 25.00,
                    DateCreated = DateTime.Now,
                    Quantity = 3,
                    Photo = "",
                    City = "Nashville",
                    State = "TN",
                    DeliverLocal = false,
                });

                ApplicationUser user2 = userManager.FindByNameAsync("jdoe@email.com").Result;

                productCategoryId = (from ct in context.CategoryType
                                         where ct.CategoryType.Equals("Arts & Collectibles")
                                         select ct.Id).Single();

                context.Product.Add(new Product
                {
                    User = user2,
                    Name = "Sunset Painting",
                    Description = "A beautiful oil painting of a beach during sunset.",
                    CategoryId = productCategoryId,
                    Status = true,
                    Price = 225.00,
                    DateCreated = DateTime.Now,
                    Quantity = 1,
                    Photo = "",
                    City = "Nashville",
                    State = "TN",
                    DeliverLocal = false,
                });

                context.Product.Add(new Product
                {
                    User = user2,
                    Name = "Paris Cafe Painting",
                    Description = "A beautiful oil painting a cafe in Paris.",
                    CategoryId = productCategoryId,
                    Status = true,
                    Price = 350.00,
                    DateCreated = DateTime.Now,
                    Quantity = 1,
                    Photo = "",
                    City = "Nashville",
                    State = "TN",
                    DeliverLocal = false,
                });

                
                //context.Product.Add(new Product { CustomerId = customerId, Description = "A beautiful oil painting a cafe in Paris.", Name = "Paris Cafe Painting", Price = 350.00, ProductCategoryId = productCategoryId, Quantity = 1 });

                context.SaveChanges();
            }
        }
    }
}