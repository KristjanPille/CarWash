﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Data;

namespace WebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("Domain.Campaign", b =>
                {
                    b.Property<int>("campaignID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("nameOfCampaign")
                        .HasColumnType("TEXT");

                    b.Property<int>("serviceID")
                        .HasColumnType("INTEGER");

                    b.HasKey("campaignID");

                    b.HasIndex("serviceID");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("Domain.Car", b =>
                {
                    b.Property<int>("carID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("carTypeID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("licenceNr")
                        .HasColumnType("INTEGER");

                    b.Property<int>("personID")
                        .HasColumnType("INTEGER");

                    b.HasKey("carID");

                    b.HasIndex("carTypeID");

                    b.HasIndex("personID");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Domain.CarType", b =>
                {
                    b.Property<int>("carTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.HasKey("carTypeID");

                    b.ToTable("CarTypes");
                });

            modelBuilder.Entity("Domain.Check", b =>
                {
                    b.Property<int>("checkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("amountExcludeVat")
                        .HasColumnType("INTEGER");

                    b.Property<int>("amountWithVat")
                        .HasColumnType("INTEGER");

                    b.Property<string>("comment")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("dateTimeCheck")
                        .HasColumnType("TEXT");

                    b.Property<int>("personID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("vat")
                        .HasColumnType("INTEGER");

                    b.Property<int>("washID")
                        .HasColumnType("INTEGER");

                    b.HasKey("checkID");

                    b.HasIndex("personID");

                    b.HasIndex("washID");

                    b.ToTable("Checks");
                });

            modelBuilder.Entity("Domain.Discount", b =>
                {
                    b.Property<int>("discountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("checkID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("washID")
                        .HasColumnType("INTEGER");

                    b.HasKey("discountID");

                    b.HasIndex("checkID");

                    b.HasIndex("washID");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Domain.IsInWash", b =>
                {
                    b.Property<int>("isInWashID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("carID")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("from")
                        .HasColumnType("TEXT");

                    b.Property<int>("personID")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("to")
                        .HasColumnType("TEXT");

                    b.Property<int>("washID")
                        .HasColumnType("INTEGER");

                    b.HasKey("isInWashID");

                    b.HasIndex("carID");

                    b.HasIndex("personID");

                    b.HasIndex("washID");

                    b.ToTable("IsInWashes");
                });

            modelBuilder.Entity("Domain.ModelMark", b =>
                {
                    b.Property<int>("ModelMarkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("mark")
                        .HasColumnType("TEXT");

                    b.Property<string>("model")
                        .HasColumnType("TEXT");

                    b.HasKey("ModelMarkID");

                    b.ToTable("ModelMarks");
                });

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.Property<int>("oderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("comment")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("dateAndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("washID")
                        .HasColumnType("INTEGER");

                    b.HasKey("oderID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Payment", b =>
                {
                    b.Property<int>("paymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("checkID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("paymentAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("paymentMethodID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("personID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("timeOfPayment")
                        .HasColumnType("TEXT");

                    b.HasKey("paymentID");

                    b.HasIndex("checkID");

                    b.HasIndex("paymentMethodID");

                    b.HasIndex("personID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Domain.PaymentMethod", b =>
                {
                    b.Property<int>("paymentMethodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("paymentMethodName")
                        .HasColumnType("INTEGER");

                    b.HasKey("paymentMethodID");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("Domain.Person", b =>
                {
                    b.Property<int>("personID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.Property<string>("name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.Property<int>("personTypeID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("phoneNr")
                        .HasColumnType("INTEGER");

                    b.HasKey("personID");

                    b.HasIndex("personTypeID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Domain.PersonType", b =>
                {
                    b.Property<int>("personTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.HasKey("personTypeID");

                    b.ToTable("PersonTypes");
                });

            modelBuilder.Entity("Domain.Service", b =>
                {
                    b.Property<int>("serviceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("campaignID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("nameOfService")
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.HasKey("serviceID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Domain.Wash", b =>
                {
                    b.Property<int>("washID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("checkID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("nameOfWashType")
                        .HasColumnType("TEXT");

                    b.Property<int>("orderID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("washTypeID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("washTypeID1")
                        .HasColumnType("INTEGER");

                    b.HasKey("washID");

                    b.HasIndex("orderID");

                    b.HasIndex("washTypeID1");

                    b.ToTable("Washes");
                });

            modelBuilder.Entity("Domain.WashType", b =>
                {
                    b.Property<int>("washTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("nameOfWash")
                        .HasColumnType("TEXT");

                    b.Property<int>("washID")
                        .HasColumnType("INTEGER");

                    b.HasKey("washTypeID");

                    b.ToTable("WashTypes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Domain.Campaign", b =>
                {
                    b.HasOne("Domain.Service", "Service")
                        .WithMany("Campaign")
                        .HasForeignKey("serviceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Car", b =>
                {
                    b.HasOne("Domain.CarType", "CarType")
                        .WithMany()
                        .HasForeignKey("carTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Person", "Person")
                        .WithMany()
                        .HasForeignKey("personID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Check", b =>
                {
                    b.HasOne("Domain.Person", "Person")
                        .WithMany()
                        .HasForeignKey("personID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Wash", "Wash")
                        .WithMany("Check")
                        .HasForeignKey("washID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Discount", b =>
                {
                    b.HasOne("Domain.Check", "Check")
                        .WithMany()
                        .HasForeignKey("checkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Wash", "Wash")
                        .WithMany()
                        .HasForeignKey("washID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.IsInWash", b =>
                {
                    b.HasOne("Domain.Car", "Car")
                        .WithMany()
                        .HasForeignKey("carID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Person", "Person")
                        .WithMany()
                        .HasForeignKey("personID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Wash", "Wash")
                        .WithMany()
                        .HasForeignKey("washID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Payment", b =>
                {
                    b.HasOne("Domain.Check", "Check")
                        .WithMany()
                        .HasForeignKey("checkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("paymentMethodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Person", "Person")
                        .WithMany()
                        .HasForeignKey("personID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Person", b =>
                {
                    b.HasOne("Domain.PersonType", "PersonType")
                        .WithMany()
                        .HasForeignKey("personTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Wash", b =>
                {
                    b.HasOne("Domain.Order", "order")
                        .WithMany("Wash")
                        .HasForeignKey("orderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.WashType", "washType")
                        .WithMany()
                        .HasForeignKey("washTypeID1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
