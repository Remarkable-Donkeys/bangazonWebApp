using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bangazonWebApp.Data;
using bangazonWebApp.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace bangazonWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // DO NOT REMOVE THE FOLLOWING LINE
            var host = BuildWebHost(args);

            /************************************************************************/
            /* 1. Comment out or remove the Seed Block to stop seeding the database */
            /* 2. Update the database                                               */
            /* 3. Optional - delete the SeedData.cs file                            */
            /************************************************************************/

            /********/
            /* Seed */
            /********/
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                try
                {
                    SeedData.Initialize(services, userManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            /************/
            /* End Seed */
            /************/

            // DO NOT REMOVE THE FOLLOWING LINE
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
