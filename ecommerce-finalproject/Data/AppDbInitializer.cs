using ecommerce_finalproject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_finalproject.Data.Static;

namespace ecommerce_finalproject.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Products
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Products>()
                    {
                        new Products()
                        {
                            name = "Knitted Scrunchie Pink",
                            description = @"The completely handmade scrunchies are on sale!
                            I prepare my products carefully for you.
                            The price is valid for one product only.

                            • fits snugly & loosely on the wrist
                            • hand washable or machine washable on low

                            We hope to bring happiness to you with this handmade scrunchie! 💕
                            Thanks for visiting our shop! 💗  ",
                            price = 30.00,
                            imageURL ="/img/asdsad.jpg",
                            stockCode = 5,
                            ProductCategory = ProductCategory.KnittedScrunchies,

                        },
                        new Products()
                        {
                            name = "Handmade Scrunchie Hermes",
                            description = @"
                            The completely handmade scrunchies are on sale!
                            I prepare my products carefully for you.
                            The price is valid for one product only.

                            • fits snugly & loosely on the wrist
                            • hand washable or machine washable on low

                            We hope to bring happiness to you with this handmade scrunchie! 💕
                            Thanks for visiting our shop! 💗 ",
                            price = 30.00,
                            imageURL ="/img/ekose1-2.jpg",
                            stockCode = 5,
                            ProductCategory = ProductCategory.HandmadeScrunchies,

                        },
                        new Products()
                        {
                            name = "Knitted Scrunchie Athena",
                            description = @"The completely handmade scrunchies are on sale!
                            I prepare my products carefully for you.
                            The price is valid for one product only.

                            • fits snugly & loosely on the wrist
                            • hand washable or machine washable on low

                            We hope to bring happiness to you with this handmade scrunchie! 💕
                            Thanks for visiting our shop! 💗",
                            price = 30.00,
                            imageURL ="/img/ekose2.jpg",
                            stockCode = 5,
                            ProductCategory = ProductCategory.HandmadeScrunchies,

                        }
                    });
                    context.SaveChanges();
                }

            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles Section
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@lugi.com";
                var adminUser = await userManager.FindByNameAsync(adminUserEmail);
                if(adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234!");
                    await userManager.AddToRoleAsync(newAdminUser,UserRoles.Admin);
                }

                
                string appUserEmail = "user@lugi.com";
                var appUser = await userManager.FindByNameAsync(appUserEmail);
                if (adminUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234!");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }
    }
}

