using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 64, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 64, nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 128, nullable: false),
                    LastName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    CarTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.CarTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ModelMarks",
                columns: table => new
                {
                    ModelMarkId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Mark = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelMarks", x => x.ModelMarkId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OderId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateAndTime = table.Column<DateTime>(nullable: false),
                    WashId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OderId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PaymentMethodName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "PersonTypes",
                columns: table => new
                {
                    PersonTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypes", x => x.PersonTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameOfService = table.Column<string>(maxLength: 64, nullable: false),
                    CampaignId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "WashTypes",
                columns: table => new
                {
                    WashTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WashId = table.Column<int>(nullable: false),
                    NameOfWash = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WashTypes", x => x.WashTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    AppUserId = table.Column<string>(maxLength: 36, nullable: false),
                    PersonTypeId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 64, nullable: false),
                    PhoneNr = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_PersonTypes_PersonTypeId",
                        column: x => x.PersonTypeId,
                        principalTable: "PersonTypes",
                        principalColumn: "PersonTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    CampaignId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServiceId = table.Column<int>(nullable: false),
                    NameOfCampaign = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.CampaignId);
                    table.ForeignKey(
                        name: "FK_Campaigns_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Washes",
                columns: table => new
                {
                    WashId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CheckId = table.Column<int>(nullable: false),
                    WashTypeId = table.Column<int>(nullable: false),
                    WashTypeId1 = table.Column<int>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    NameOfWashType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Washes", x => x.WashId);
                    table.ForeignKey(
                        name: "FK_Washes_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Washes_WashTypes_WashTypeId1",
                        column: x => x.WashTypeId1,
                        principalTable: "WashTypes",
                        principalColumn: "WashTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CarTypeId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    LicenceNr = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Cars_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "CarTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Checks",
                columns: table => new
                {
                    CheckId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    WashId = table.Column<int>(nullable: false),
                    DateTimeCheck = table.Column<DateTime>(nullable: false),
                    AmountExcludeVat = table.Column<int>(nullable: false),
                    AmountWithVat = table.Column<int>(nullable: false),
                    Vat = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checks", x => x.CheckId);
                    table.ForeignKey(
                        name: "FK_Checks_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checks_Washes_WashId",
                        column: x => x.WashId,
                        principalTable: "Washes",
                        principalColumn: "WashId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IsInWashes",
                columns: table => new
                {
                    IsInWashId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CarId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    WashId = table.Column<int>(nullable: false),
                    From = table.Column<TimeSpan>(nullable: false),
                    To = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsInWashes", x => x.IsInWashId);
                    table.ForeignKey(
                        name: "FK_IsInWashes_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IsInWashes_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IsInWashes_Washes_WashId",
                        column: x => x.WashId,
                        principalTable: "Washes",
                        principalColumn: "WashId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CheckId = table.Column<int>(nullable: false),
                    WashId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_Discounts_Checks_CheckId",
                        column: x => x.CheckId,
                        principalTable: "Checks",
                        principalColumn: "CheckId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Discounts_Washes_WashId",
                        column: x => x.WashId,
                        principalTable: "Washes",
                        principalColumn: "WashId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: false),
                    CheckId = table.Column<int>(nullable: false),
                    PaymentAmount = table.Column<int>(nullable: false),
                    TimeOfPayment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Checks_CheckId",
                        column: x => x.CheckId,
                        principalTable: "Checks",
                        principalColumn: "CheckId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ServiceId",
                table: "Campaigns",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarTypeId",
                table: "Cars",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PersonId",
                table: "Cars",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_PersonId",
                table: "Checks",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_WashId",
                table: "Checks",
                column: "WashId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_CheckId",
                table: "Discounts",
                column: "CheckId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_WashId",
                table: "Discounts",
                column: "WashId");

            migrationBuilder.CreateIndex(
                name: "IX_IsInWashes_CarId",
                table: "IsInWashes",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_IsInWashes_PersonId",
                table: "IsInWashes",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_IsInWashes_WashId",
                table: "IsInWashes",
                column: "WashId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CheckId",
                table: "Payments",
                column: "CheckId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PersonId",
                table: "Payments",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_AppUserId",
                table: "Persons",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonTypeId",
                table: "Persons",
                column: "PersonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Washes_OrderId",
                table: "Washes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Washes_WashTypeId1",
                table: "Washes",
                column: "WashTypeId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "IsInWashes");

            migrationBuilder.DropTable(
                name: "ModelMarks");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Checks");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Washes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PersonTypes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "WashTypes");
        }
    }
}
