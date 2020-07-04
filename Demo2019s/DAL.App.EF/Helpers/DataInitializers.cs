﻿using System;
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
            
            var paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod()
                {
                    PaymentMethodName =  "PayPal",
                    Id = new Guid("00000000-0000-0000-0000-000000000140")
                },
                new PaymentMethod()
                {
                    PaymentMethodName =  "Credit Card",
                    Id = new Guid("00000000-0000-0000-0000-000000000141")
                },
            };
            foreach (var paymentMethod in paymentMethods)
            {
                if (!context.PaymentMethods.Any(l => l.Id == paymentMethod.Id))
                {
                    context.PaymentMethods.Add(paymentMethod);
                }
            }

            context.SaveChanges();
            
            var services = new Service[]
            {
                new Service()
                {
                    NameOfService =  "VäliPesu",
                    PriceOfService = 25,
                    Description = "Leotus, käsipesu, porimattide puhastus",
                    Duration = 30,
                    CampaignId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Id = new Guid("00000000-0000-0000-0000-000000000123")
                },
                new Service()
                {
                    NameOfService =  "SisePesu",
                    PriceOfService = 20,
                    Description = "Kuiv puhastus, aknaklaaside puhastus seest, porimattide puhastus",
                    Duration = 60,
                    Id = new Guid("00000000-0000-0000-0000-000000000124")
                },
                new Service()
                {
                    NameOfService =  "Premium Välipesu",
                    Description = "Põhjalik välipesu, Leotus, velgede, rataste ning rattakoobaste pesu, käsipesu, porimattide puhastus, ustevahede pesu ning kuivatamine",
                    PriceOfService = 40,
                    Duration = 60,
                    Id = new Guid("00000000-0000-0000-0000-000000000125")
                },
                new Service()
                {
                    NameOfService =  "Premium Sisepesu",
                    PriceOfService = 45,
                    Description = "Kõikide salongi ja pagasiruumi pindade märgkoristus, Salongi ja pagasiruumi puhastamine tolmuimeja ja suruõhuga, Kõikide klaaside puhastamine seestpoolt, Põrandamattide puhastamine",
                    Duration = 120,
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
                    ModelMarkSize = 1,
                    Id = new Guid("00000000-0000-0000-0000-000000000127"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A2",
                    ModelMarkSize = 1,
                    Id = new Guid("00000000-0000-0000-0000-000000000128"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A3",
                    ModelMarkSize = 1,
                    Id = new Guid("00000000-0000-0000-0000-000000000129"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A4",
                    ModelMarkSize = 2,
                    Id = new Guid("00000000-0000-0000-0000-000000000130"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A5",
                    ModelMarkSize = 2,
                    Id = new Guid("00000000-0000-0000-0000-000000000131"),
                },
                
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "X1",
                    ModelMarkSize = 1,
                    Id = new Guid("00000000-0000-0000-0000-000000000132"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "X2",
                    ModelMarkSize = 1,
                    Id = new Guid("00000000-0000-0000-0000-000000000133"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "3",
                    ModelMarkSize = 1,
                    Id = new Guid("00000000-0000-0000-0000-000000000134"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "4",
                    ModelMarkSize = 2,
                    Id = new Guid("00000000-0000-0000-0000-000000000135"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "X5",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000136"),
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
            
            var cars = new Car[]
            {
                new Car()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000137"),
                    AppUserId = new Guid("00000000-0000-0000-0000-000000000139"),
                    ModelMarkId = modelMarks[0].Id
                },
            };
            
            foreach (var car in cars)
            {
                if (!context.Cars.Any(l => l.Id == car.Id))
                {
                    context.Cars.Add(car);
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