using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Blog.Data;
using Microsoft.AspNetCore.Identity;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            try { 
            var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.EnsureCreated();

            var adminRole = new IdentityRole("Admin");

            if(!context.Roles.Any())
            {
                //Create a Role
                roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var adminUser = new IdentityUser
                {
                    //Create a User 
                    UserName = "admin",
                    Email = "admin@test.com"
                };
                var result = userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                //Add Role
                userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
            }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
