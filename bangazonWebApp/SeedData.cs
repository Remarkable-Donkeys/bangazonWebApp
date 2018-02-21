// Author: Jason Figueroa
// Purpose: Seeds products & categories to their respective tables in db

/*****************************************************************/
/* If seeded data is no longer required this file can be deleted */
/*****************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using bangazonWebApp.Data;
using bangazonWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace bangazonWebApp
{
    public class SeedData
    {
        //private IPlanService planService;
        //private ISubscriptionService subscriptionService;
        private UserManager<ApplicationUser> _userManager;
        private IServiceProvider _serviceProvider;

        public SeedData(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._serviceProvider = serviceProvider;
        }

        public async void Initialize()
        {
            var context = this._serviceProvider.GetRequiredService<ApplicationDbContext>();

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

            /*****************/
            /* Seeding Users */
            /*****************/

            //using (var anotherContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            using (context)
            {
                var userstore = new UserStore<ApplicationUser>(context);

                ApplicationUser existingUserInDb;

                ApplicationUser stacyGauger = new ApplicationUser
                {
                    FirstName = "Stacy",
                    LastName = "Gauger",
                    Street = "123 Infinity Way",
                    City = "Nashville",
                    State = "TN",
                    Zip = "32001",
                    Phone = "1234560001",
                    UserName = "stacygauger@email.com",
                    NormalizedUserName = "STACYGAUGER@EMAIL.COM",
                    Email = "stacygauger@email.com",
                    NormalizedEmail = "STACYGAUGER@EMAIL.COM",
                    EmailConfirmed = false,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                existingUserInDb = _userManager.FindByNameAsync(stacyGauger.UserName).Result;

                if (existingUserInDb == null)
                {
                    var passwordHash = new PasswordHasher<ApplicationUser>();
                    string unhashedPassword = (stacyGauger.FirstName + stacyGauger.LastName).ToLower();
                    stacyGauger.PasswordHash = passwordHash.HashPassword(stacyGauger, unhashedPassword);
                    await userstore.CreateAsync(stacyGauger);
                }

                ApplicationUser stephanAdams = new ApplicationUser
                {
                    FirstName = "Stephan",
                    LastName = "Adams",
                    Street = "456 1st St.",
                    City = "Nashville",
                    State = "TN",
                    Zip = "32001",
                    Phone = "1234560002",
                    UserName = "stephanadams@email.com",
                    NormalizedUserName = "STEPHANADAMS@EMAIL.COM",
                    Email = "stephanadams@email.com",
                    NormalizedEmail = "STEPHANADAMS@EMAIL.COM",
                    EmailConfirmed = false,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                existingUserInDb = _userManager.FindByNameAsync(stephanAdams.UserName).Result;

                if (existingUserInDb == null)
                {
                    var passwordHash = new PasswordHasher<ApplicationUser>();
                    string unhashedPassword = (stephanAdams.FirstName + stephanAdams.LastName).ToLower();
                    stephanAdams.PasswordHash = passwordHash.HashPassword(stephanAdams, unhashedPassword);
                    await userstore.CreateAsync(stephanAdams);
                }

                ApplicationUser belleMartin = new ApplicationUser
                {
                    FirstName = "Belle",
                    LastName = "Martin",
                    Street = "789 2st St.",
                    City = "Nashville",
                    State = "TN",
                    Zip = "32001",
                    Phone = "1234560003",
                    UserName = "bellemartin@email.com",
                    NormalizedUserName = "BELLEMARTIN@EMAIL.COM",
                    Email = "bellemartin@email.com",
                    NormalizedEmail = "BELLEMARTIN@EMAIL.COM",
                    EmailConfirmed = false,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                existingUserInDb = _userManager.FindByNameAsync(belleMartin.UserName).Result;

                if (existingUserInDb == null)
                {
                    var passwordHash = new PasswordHasher<ApplicationUser>();
                    string unhashedPassword = (belleMartin.FirstName + belleMartin.LastName).ToLower();
                    belleMartin.PasswordHash = passwordHash.HashPassword(belleMartin, unhashedPassword);
                    await userstore.CreateAsync(belleMartin);
                }

                //    List<ApplicationUser> users = new List<ApplicationUser>() {
                //    stacyGauger,
                //    stephanAdams,
                //    belleMartin
                //};

                //    foreach (ApplicationUser user in users)
                //    {

                //        //ApplicationUser currentUser = userManager.FindByNameAsync(user.UserName).Result;

                //            var passwordHash = new PasswordHasher<ApplicationUser>();
                //            string unhashedPassword = (user.FirstName + user.LastName).ToLower();
                //            user.PasswordHash = passwordHash.HashPassword(user, unhashedPassword);
                //            await userstore.CreateAsync(user);
                //    }
            }            

            /*************************/
            /* Seeding Product Table */
            /*************************/
            if (!context.Product.Any())
            {
                ApplicationUser user1 = _userManager.FindByNameAsync("jsmith@email.com").Result;
                
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

                ApplicationUser user2 = _userManager.FindByNameAsync("jdoe@email.com").Result;

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
                
                context.SaveChanges();
            }
        }
    }
}