using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<IsInWash> IsInWashes { get; set; }
        public DbSet<ModelMark> ModelMarks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Wash> Washes { get; set; }
        public DbSet<WashType> WashTypes { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}