﻿using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

 namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {

        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }

        public static void DeleteDatabase(AppDbContext context)
        {
            context.Database.EnsureDeleted();
        }


        public static void SeedData(AppDbContext context)
        {
            var campaigns = new Campaign[]
            {
                new Campaign()
                {
                    NameOfCampaign = "STANDARD EXTERIOR WASH Campaign",
                    Description = "Discount for standard exterior wash 25%",
                    DiscountAmount = 0.25,
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                }, 
            };
            foreach (var campaign in campaigns)
            {
                if (!context.Campaigns.Any(l => l.Id == campaign.Id))
                {
                    context.Campaigns.Add(campaign);
                }
            }

            context.SaveChanges();
        }
        
        
        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roles = new (string roleName, string roleDisplayName)[]
            {
                ("user", "User"),
                ("admin", "Admin")
            };

            foreach (var (roleName, roleDisplayName) in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole()
                    {
                        Name = roleName,
                        DisplayName = roleDisplayName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }
            }


            var users = new (string name, string password, string firstName, string lastName, string PhoneNumber, Guid Id)[]
            {
                ("juss@gmail.com", "Password123+", "Juss", "Jussike", "1234567890", new Guid("00000000-0000-0000-0000-000000000139")),
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.name).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Id = userInfo.Id,
                        Email = userInfo.name,
                        UserName = userInfo.name,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName,
                        PhoneNumber = userInfo.PhoneNumber,
                        EmailConfirmed = true
                    };
                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");
                    }
                }

                var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
                roleResult = userManager.AddToRoleAsync(user, "user").Result;
            }
        }
    }
}