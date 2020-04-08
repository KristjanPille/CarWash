using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Campaign> Campaigns { get; set; } = default!;
        public DbSet<Car> Cars { get; set; } = default!;
        public DbSet<CarType> CarTypes { get; set; } = default!;
        public DbSet<Check> Checks { get; set; } = default!;
        public DbSet<Discount> Discounts { get; set; } = default!;
        public DbSet<IsInWash> IsInWashes { get; set; } = default!;
        public DbSet<ModelMark> ModelMarks { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;
        
        public DbSet<PersonCar> PersonCars { get; set; } = default!;
        public DbSet<Service> Services { get; set; } = default!;
        public DbSet<Wash> Washes { get; set; } = default!;
        public DbSet<WashType> WashTypes { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    }
}