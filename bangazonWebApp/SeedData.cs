
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
        }
    }
}