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
            if (!context.Category.Any())
            {
                context.Category.Add(new Category { CategoryType = "Jewelry & Accessories" });
                context.Category.Add(new Category { CategoryType = "Clothing & Shoes" });
                context.Category.Add(new Category { CategoryType = "Home & Living" });
                context.Category.Add(new Category { CategoryType = "Arts & Collectibles" });

                context.SaveChanges();
            }

            /*************************/
            /* Seeding Product Table */
            /*************************/
            if (!context.Product.Any())
            {
                ApplicationUser user1 = userManager.FindByNameAsync("jsmith@email.com").Result;
                
                int productCategoryId = (from ct in context.Category
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

                productCategoryId = (from ct in context.Category
                                     where ct.CategoryType.Equals("Jewelry & Accessories")
                                     select ct.Id).Single();

                context.Product.Add(new Product
                {
                    User = user1,
                    Name = "Beaded Bracelet",
                    Description = "A beautiful handmade beaded bracelet.",
                    CategoryId = productCategoryId,
                    Status = true,
                    Price = 28.50,
                    DateCreated = DateTime.Now,
                    Quantity = 6,
                    Photo = "",
                    City = "Nashville",
                    State = "TN",
                    DeliverLocal = false,
                });

                context.Product.Add(new Product
                {
                    User = user1,
                    Name = "Charm Bracelet",
                    Description = "A beautiful handmade charm bracelet.",
                    CategoryId = productCategoryId,
                    Status = true,
                    Price = 42.00,
                    DateCreated = DateTime.Now,
                    Quantity = 3,
                    Photo = "",
                    City = "Nashville",
                    State = "TN",
                    DeliverLocal = false,
                });
                
                ApplicationUser user2 = userManager.FindByNameAsync("jdoe@email.com").Result;

                productCategoryId = (from ct in context.Category
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

                productCategoryId = (from ct in context.Category
                                     where ct.CategoryType.Equals("Home & Living")
                                     select ct.Id).Single();

                context.Product.Add(new Product
                {
                    User = user2,
                    Name = "Handmade Quilt",
                    Description = "A very warm beautifully hand crafted quilt.",
                    CategoryId = productCategoryId,
                    Status = true,
                    Price = 155.25,
                    DateCreated = DateTime.Now,
                    Quantity = 1,
                    Photo = "",
                    City = "Nashville",
                    State = "TN",
                    DeliverLocal = false,
                });

                context.SaveChanges();
            }

            /******************************/
            /* Seeding Payment Type Table */
            /******************************/
            if (!context.PaymentType.Any())
            {
                ApplicationUser user1 = userManager.FindByNameAsync("jsmith@email.com").Result;

                //payment on a completed order
                context.PaymentType.Add(new PaymentType
                {
                    User = user1,
                    Name = "Payment 1",
                    AccountNumber = "18203948",
                    Active = true
                });
                //payment not on any order
                context.PaymentType.Add(new PaymentType
                {
                    User = user1,
                    Name = "Payment 2",
                    AccountNumber = "29384059",
                    Active = true
                });
                //payment not on any order
                context.PaymentType.Add(new PaymentType
                {
                    User = user1,
                    Name = "Payment 3",
                    AccountNumber = "37485968",
                    Active = true
                });

                context.SaveChanges();

            }

            /****************************/
            /* Seeding Order Type Table */
            /****************************/
            if (!context.Order.Any())
            {
                ApplicationUser user1 = userManager.FindByNameAsync("jsmith@email.com").Result;
                int payment1Id = (from p in context.PaymentType
                                  where p.Name.Equals("Payment 1")
                                  select p.Id).Single();
                PaymentType payment1 = context.PaymentType.Where(p => p.Id == payment1Id).Single();

                //completed order
                context.Order.Add(new Order
                {
                    User = user1,
                    PaymentId = payment1Id,
                    PaymentType = payment1,
                    DateCreated = DateTime.Now.AddDays(-100),
                    DateClosed = DateTime.Now.AddDays(-99) 
                });
                //incomplete order
                context.Order.Add(new Order
                {
                    User = user1,
                    DateCreated = DateTime.Now.AddDays(-10),
                });

                context.SaveChanges();

            }
            /***********************************/
            /* Seeding OrderProduct Type Table */
            /***********************************/
            if (!context.OrderProduct.Any())
            {
                ApplicationUser user1 = userManager.FindByNameAsync("jsmith@email.com").Result;
                int payment1Id = (from p in context.PaymentType
                                  where p.Name.Equals("Payment 1")
                                  select p.Id).Single();
                int order1Id = (from o in context.Order
                                  where o.User.Equals(user1) && o.PaymentId.Equals(payment1Id)
                                  select o.Id).Single();
                int order2Id = (from o in context.Order
                                where o.User.Equals(user1) && o.PaymentId == null
                                select o.Id).Single();
                int product1Id = (from p in context.Product
                                where p.Name.Equals("Sunset Painting")
                                select p.Id).Single();
                int product2Id = (from p in context.Product
                                  where p.Name.Equals("Paris Cafe Painting")
                                  select p.Id).Single();

                //product on completed order
                context.OrderProduct.Add(new OrderProduct
                {
                    OrderId = order1Id,
                    ProductId = product1Id
                });
                //product on incomplete order
                context.OrderProduct.Add(new OrderProduct
                {
                    OrderId = order2Id,
                    ProductId = product2Id
                });

                context.SaveChanges();

            }
        }
    }
}