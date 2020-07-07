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
                    NameOfCampaign =  "Campaign for STANDARD EXTERIOR WASH",
                    Description = "Discount for standard wash 25%",
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
                    NameOfService =  "STANDARD EXTERIOR WASH",
                    PriceOfService = 25,
                    Description = "Snowfoam pre wash, Shampoo wash, Drying with microfiber cloth including door sills",
                    Duration = 30,
                    CampaignId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Id = new Guid("00000000-0000-0000-0000-000000000123")
                },
                new Service()
                {
                    NameOfService =  "INTERIOR CLEANING",
                    PriceOfService = 20,
                    Description = "Vacuum cleaning of the interior and trunk, Wiping of all the interior and trunk surfaces, Cleaning front glasses from inside the car, Cleaning floor mats",
                    Duration = 60,
                    Id = new Guid("00000000-0000-0000-0000-000000000124")
                },
                new Service()
                {
                    NameOfService =  "PREMIUM EXTERIOR WASH",
                    Description = "Snowfoam pre wash, Shampoo wash, Liquid wax, Cleaning floor mats, Drying with pressured air, tire dressing",
                    PriceOfService = 40,
                    Duration = 60,
                    Id = new Guid("00000000-0000-0000-0000-000000000125")
                },
                new Service()
                {
                    NameOfService =  "PREMIUM INTERIOR CLEANING",
                    PriceOfService = 45,
                    Description = "Vacuum cleaning of the interior and the trunk, Wet wiping of all the interior and trunk surfaces, Cleaning of plastic elements of trunk and interior with Meguiars all purpose cleaner, Floor mats cleaning, Cleaning all the glasses from inside and outside the car",
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
                    ModelMarkSize = 2,
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
                    Model = "1",
                    ModelMarkSize = 1,
                    Id = new Guid("00000000-0000-0000-0000-000000000134"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "2",
                    ModelMarkSize = 1,
                    Id = new Guid("00000000-0000-0000-0000-000000000180"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "3",
                    ModelMarkSize = 1,
                    Id = new Guid("00000000-0000-0000-0000-000000000135"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "4",
                    ModelMarkSize = 2,
                    Id = new Guid("00000000-0000-0000-0000-000000000136"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "5",
                    ModelMarkSize = 2,
                    Id = new Guid("00000000-0000-0000-0000-000000000142"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "6",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000146"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "X5",
                    ModelMarkSize = 4,
                    Id = new Guid("00000000-0000-0000-0000-000000000137"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "X6",
                    ModelMarkSize = 4,
                    Id = new Guid("00000000-0000-0000-0000-000000000144"),
                },
                new ModelMark()
                {
                    Mark =  "Bmw",
                    Model = "X7",
                    ModelMarkSize = 5,
                    Id = new Guid("00000000-0000-0000-0000-000000000143"),
                },
                new ModelMark()
                {
                    Mark =  "Mercedes-Benz",
                    Model = "C",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000147"),
                },
                new ModelMark()
                {
                    Mark =  "Mercedes-Benz",
                    Model = "E",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000148"),
                },
                new ModelMark()
                {
                    Mark =  "Mercedes-Benz",
                    Model = "S",
                    ModelMarkSize = 4,
                    Id = new Guid("00000000-0000-0000-0000-000000000149"),
                },
                new ModelMark()
                {
                    Mark =  "Mercedes-Benz",
                    Model = "G",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000150"),
                },
                new ModelMark()
                {
                    Mark =  "Mercedes-Benz",
                    Model = "GLS",
                    ModelMarkSize = 5,
                    Id = new Guid("00000000-0000-0000-0000-000000000151"),
                },
                new ModelMark()
                {
                    Mark =  "Mercedes-Benz",
                    Model = "GLE",
                    ModelMarkSize = 4,
                    Id = new Guid("00000000-0000-0000-0000-000000000152"),
                },
                new ModelMark()
                {
                    Mark =  "Mercedes-Benz",
                    Model = "GLC",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000153"),
                },
                new ModelMark()
                {
                    Mark =  "Mercedes-Benz",
                    Model = "GLE",
                    ModelMarkSize = 4,
                    Id = new Guid("00000000-0000-0000-0000-000000000154"),
                },
                new ModelMark()
                {
                    Mark =  "Mercedes-Benz",
                    Model = "CLA",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000155"),
                },
                new ModelMark()
                {
                    Mark =  "Mercedes-Benz",
                    Model = "V",
                    ModelMarkSize = 5,
                    Id = new Guid("00000000-0000-0000-0000-000000000156"),
                },
                new ModelMark()
                {
                    Mark =  "Volkswagen",
                    Model = "Amarok",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000157"),
                },
                new ModelMark()
                {
                    Mark =  "Volkswagen",
                    Model = "CC",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000158"),
                },
                new ModelMark()
                {
                    Mark =  "Volkswagen",
                    Model = "Golf",
                    ModelMarkSize = 2,
                    Id = new Guid("00000000-0000-0000-0000-000000000159"),
                },
                new ModelMark()
                {
                    Mark =  "Volkswagen",
                    Model = "Golf Sportsvan",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000160"),
                },
                new ModelMark()
                {
                    Mark =  "Volkswagen",
                    Model = "Golf Plus",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000161"),
                },
                new ModelMark()
                {
                    Mark =  "Volkswagen",
                    Model = "Jetta",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000162"),
                },
                new ModelMark()
                {
                    Mark =  "Volkswagen",
                    Model = "Passat",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000163"),
                },
                new ModelMark()
                {
                    Mark =  "Volkswagen",
                    Model = "Tiguan",
                    ModelMarkSize = 4,
                    Id = new Guid("00000000-0000-0000-0000-000000000164"),
                },
                new ModelMark()
                {
                    Mark =  "Volkswagen",
                    Model = "Touareg",
                    ModelMarkSize = 4,
                    Id = new Guid("00000000-0000-0000-0000-000000000165"),
                },
                new ModelMark()
                {
                    Mark =  "Volkswagen",
                    Model = "Transporter",
                    ModelMarkSize = 5,
                    Id = new Guid("00000000-0000-0000-0000-000000000166"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A6",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000167"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A7",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000168"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "A8",
                    ModelMarkSize = 4,
                    Id = new Guid("00000000-0000-0000-0000-000000000169"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "Q2",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000170"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "Q3",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000171"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "Q5",
                    ModelMarkSize = 3,
                    Id = new Guid("00000000-0000-0000-0000-000000000172"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "Q7",
                    ModelMarkSize = 4,
                    Id = new Guid("00000000-0000-0000-0000-000000000173"),
                },
                new ModelMark()
                {
                    Mark =  "Audi",
                    Model = "Q8",
                    ModelMarkSize = 5,
                    Id = new Guid("00000000-0000-0000-0000-000000000174"),
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
                    Id = new Guid("00000000-0000-0000-0000-000000000138"),
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