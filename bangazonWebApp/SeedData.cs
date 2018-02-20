
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

            //if (!context.Users.Any())
            //{
            //    //userManager.crea
            //    var user = new ApplicationUser { UserName = "jdoe@email.com", FirstName = "John",  LastName = "Doe",  Email = "jdoe@email.com", City = "Nashville", Zip = "32001", PhoneNumber = "1234567890", Street = "123 Main St.", State = "TN" };

            //    await userManager.CreateAsync(user, "password");
            //    userManager.FindByEmailAsync

            //}

            ///*************************/
            ///* Seeding Product Table */
            ///*************************/
            //if (!context.Product.Any())
            //{
            //    context.Product.Add(new Product { Name = "Jewelry & Accessories" });
            //    context.Product.Add(new Product { Product = "Clothing & Shoes" });
            //    context.Product.Add(new Product { Product = "Home & Living" });
            //    context.Product.Add(new Product { Product = "Arts & Collectibles" });

            //    context.SaveChanges();
            //}

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
                    Price = 25.00,
                    CategoryId = productCategoryId,
                    Quantity = 2 });
                //context.Product.Add(new Product { CustomerId = customerId, Description = "A beautifully knitted scarf for a toddler girl.", Name = "Knit Scarf", Price = 25.00, CategoryId = productCategoryId, Quantity = 4 });
                //context.Product.Add(new Product { CustomerId = customerId, Description = "A beautifully knitted mittens for a toddler girl.", Name = "Knit Mittens", Price = 25.00, CategoryId = productCategoryId, Quantity = 3 });
                
                //productCategoryId = (from pc in context.ProductCategory
                //                     where pc.Name.Equals("Arts & Collectibles")
                //                     select pc.ProductCategoryId).Single();

                //context.Product.Add(new Product { CustomerId = customerId, Description = "A beautiful oil painting of a beach during sunset.", Name = "Sunset Painting", Price = 225.00, ProductCategoryId = productCategoryId, Quantity = 1 });
                //context.Product.Add(new Product { CustomerId = customerId, Description = "A beautiful oil painting a cafe in Paris.", Name = "Paris Cafe Painting", Price = 350.00, ProductCategoryId = productCategoryId, Quantity = 1 });
                
                context.SaveChanges();
            }
        }
    }
}