﻿﻿using System;
using System.Collections.Generic;
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
                    NameOfCampaign =  "Kampaania välispesu",
                    Description = "Auto välispesu 25% hinnast alla",
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
            
            var services = new Service[]
            {
                new Service()
                {
                    NameOfService =  "VäliPesu",
                    PriceOfService = 20,
                    Id = new Guid("00000000-0000-0000-0000-000000000123")
                },
                new Service()
                {
                    NameOfService =  "SisePesu",
                    PriceOfService = 20,
                    Id = new Guid("00000000-0000-0000-0000-000000000124")
                },
                new Service()
                {
                    NameOfService =  "Premium Välipesu",
                    PriceOfService = 40,
                    Id = new Guid("00000000-0000-0000-0000-000000000125")
                },
                new Service()
                {
                    NameOfService =  "Premium Sisepesu",
                    PriceOfService = 40,
                    Id = new Guid("00000000-0000-0000-0000-000000000126")
                },
            };

            foreach (var service in services)
            {
                if (!context.Services.Any(l => l.Id == service.Id))
                {
                    context.Services.Add(service);
                }
            }

            context.SaveChanges();

   
            
            var modelMarks = new ModelMark[]
            {
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A1",
                    Id = new Guid("00000000-0000-0000-0000-000000000127"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A1",
                    Id = new Guid("00000000-0000-0000-0000-000000000128"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A2",
                    Id = new Guid("00000000-0000-0000-0000-000000000129"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A3",
                    Id = new Guid("00000000-0000-0000-0000-000000000130"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A4",
                    Id = new Guid("00000000-0000-0000-0000-000000000131"),
                },
            };
            
            foreach (var modelMark in modelMarks)
            {
                if (!context.ModelMarks.Any(l => l.Id == modelMark.Id))
                {
                    context.ModelMarks.Add(modelMark);
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


            var users = new (string name, string password, string firstName, string lastName, Guid Id)[]
            {
                ("juss@gmail.com", "Password123+", "Juss", "Jussike", new Guid("00000000-0000-0000-0000-000000000134")),
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