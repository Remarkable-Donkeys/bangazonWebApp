
/*****************************************************************/
/* If seeded data is no longer required this file can be deleted */
/*****************************************************************/

using System;
using System.Linq;
using bangazonWebApp.Data;
using bangazonWebApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace bangazonWebApp
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
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
            //if (!context.Product.Any())
            //{
            //    //int customerId = (from c in context.Customer
            //    //                  where c.LastName.Equals("Gauger") && c.FirstName.Equals("Stacy")
            //    //                  select c.CustomerId).Single();

            //    int productCategoryId = (from ct in context.CategoryType
            //                             where ct.CategoryType.Equals("Clothing & Shoes")
            //                             select ct.Id).Single();

            //    context.Product.Add(new Product { CustomerId = customerId, Description = "A beautifully knitted hat for a toddler girl.", Name = "Knit Hat", Price = 25.00, CategoryId = productCategoryId, Quantity = 2 });
            //    context.Product.Add(new Product { CustomerId = customerId, Description = "A beautifully knitted scarf for a toddler girl.", Name = "Knit Scarf", Price = 25.00, CategoryId = productCategoryId, Quantity = 4 });
            //    context.Product.Add(new Product { CustomerId = customerId, Description = "A beautifully knitted mittens for a toddler girl.", Name = "Knit Mittens", Price = 25.00, CategoryId = productCategoryId, Quantity = 3 });

            //    customerId = (from c in context.Customer
            //                  where c.LastName.Equals("Adams") && c.FirstName.Equals("Stephan")
            //                  select c.CustomerId).Single();

            //    productCategoryId = (from pc in context.ProductCategory
            //                         where pc.Name.Equals("Arts & Collectibles")
            //                         select pc.ProductCategoryId).Single();

            //    context.Product.Add(new Product { CustomerId = customerId, Description = "A beautiful oil painting of a beach during sunset.", Name = "Sunset Painting", Price = 225.00, ProductCategoryId = productCategoryId, Quantity = 1 });
            //    context.Product.Add(new Product { CustomerId = customerId, Description = "A beautiful oil painting a cafe in Paris.", Name = "Paris Cafe Painting", Price = 350.00, ProductCategoryId = productCategoryId, Quantity = 1 });

            //    customerId = (from c in context.Customer
            //                  where c.LastName.Equals("Martin") && c.FirstName.Equals("Belle")
            //                  select c.CustomerId).Single();

            //    productCategoryId = (from pc in context.ProductCategory
            //                         where pc.Name.Equals("Jewelry & Accessories")
            //                         select pc.ProductCategoryId).Single();

            //    context.Product.Add(new Product { CustomerId = customerId, Description = "A beautiful handmade beaded bracelet.", Name = "Beaded Bracelet", Price = 28.50, ProductCategoryId = productCategoryId, Quantity = 7 });
            //    context.Product.Add(new Product { CustomerId = customerId, Description = "A beautiful handmade charm bracelet.", Name = "Charm Bracelet", Price = 42.00, ProductCategoryId = productCategoryId, Quantity = 3 });

            //    customerId = (from c in context.Customer
            //                  where c.LastName.Equals("Chenard") && c.FirstName.Equals("Jeraldine")
            //                  select c.CustomerId).Single();

            //    productCategoryId = (from pc in context.ProductCategory
            //                         where pc.Name.Equals("Home & Living")
            //                         select pc.ProductCategoryId).Single();

            //    context.Product.Add(new Product { CustomerId = customerId, Description = "A very warm beautifully hand crafted quilt.", Name = "Handmade Quilt", Price = 155.25, ProductCategoryId = productCategoryId, Quantity = 4 });

            //    customerId = (from c in context.Customer
            //                  where c.LastName.Equals("Lone") && c.FirstName.Equals("Mila")
            //                  select c.CustomerId).Single();

            //    productCategoryId = (from pc in context.ProductCategory
            //                         where pc.Name.Equals("Arts & Collectibles")
            //                         select pc.ProductCategoryId).Single();

            //    context.Product.Add(new Product { CustomerId = customerId, Description = "An elephant themed, wooden, hand decorated trinket box.", Name = "Elephant Trinket Box", Price = 21.75, ProductCategoryId = productCategoryId, Quantity = 3 });
            //    context.Product.Add(new Product { CustomerId = customerId, Description = "An owl themed, wooden, hand decorated trinket box.", Name = "Owl Trinket Box", Price = 21.75, ProductCategoryId = productCategoryId, Quantity = 3 });

            //    context.SaveChanges();
            //}
        }
    }
}