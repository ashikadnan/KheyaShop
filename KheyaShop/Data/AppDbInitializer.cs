using KheyaShop.Data;
using KheyaShop.Data.Static;
using KheyaShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data
{
    public class AppDbInitializer
    {
        
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //User

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string adminUserEmail = "admin@etickets.com";
                string ashikUserEmail = "ashik@kheyashop.com";

                var adminUser = await userManager.FindByEmailAsync("admin@etickets.com");
                if(adminUser == null)
                {
                    var newadminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newadminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newadminUser, UserRoles.Admin);

                    var ashikUser = new ApplicationUser()
                    {
                        FullName = "Ashik Adnan",
                        UserName = "ashik",
                        Email = ashikUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(ashikUser, "ashik123");
                    await userManager.AddToRoleAsync(ashikUser, UserRoles.Admin);
                }


                

                string appUserEmail = "user@etickets.com";
                string sadmanEmail = "sadman@kheyashop.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (adminUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);

                    var appsUser = new ApplicationUser()
                    {
                        FullName = "Sadman Sadiq",
                        UserName = "sadman",
                        Email = sadmanEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(appsUser, "sadman123");
                    await userManager.AddToRoleAsync(appsUser, UserRoles.User);
                }

            }
        }
     }
 }
    

