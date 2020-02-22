using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
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
                    Id = table.Column<string>(nullable: false),
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
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    carTypeID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.carTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ModelMarks",
                columns: table => new
                {
                    ModelMarkID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    mark = table.Column<string>(nullable: true),
                    model = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelMarks", x => x.ModelMarkID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    oderID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    dateAndTime = table.Column<DateTime>(nullable: false),
                    washID = table.Column<int>(nullable: false),
                    comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.oderID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    paymentMethodID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    paymentMethodName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.paymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "PersonTypes",
                columns: table => new
                {
                    personTypeID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypes", x => x.personTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    serviceID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nameOfService = table.Column<string>(maxLength: 64, nullable: true),
                    campaignID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.serviceID);
                });

            migrationBuilder.CreateTable(
                name: "WashTypes",
                columns: table => new
                {
                    washTypeID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    washID = table.Column<int>(nullable: false),
                    nameOfWash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WashTypes", x => x.washTypeID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                        .Annotation("Sqlite:Autoincrement", true),
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
                    personID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(maxLength: 64, nullable: true),
                    personTypeID = table.Column<int>(nullable: false),
                    email = table.Column<string>(maxLength: 64, nullable: true),
                    phoneNr = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.personID);
                    table.ForeignKey(
                        name: "FK_Persons_PersonTypes_personTypeID",
                        column: x => x.personTypeID,
                        principalTable: "PersonTypes",
                        principalColumn: "personTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    campaignID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    serviceID = table.Column<int>(nullable: false),
                    nameOfCampaign = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.campaignID);
                    table.ForeignKey(
                        name: "FK_Campaigns_Services_serviceID",
                        column: x => x.serviceID,
                        principalTable: "Services",
                        principalColumn: "serviceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Washes",
                columns: table => new
                {
                    washID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    checkID = table.Column<int>(nullable: false),
                    washTypeID = table.Column<int>(nullable: false),
                    washTypeID1 = table.Column<int>(nullable: true),
                    orderID = table.Column<int>(nullable: false),
                    nameOfWashType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Washes", x => x.washID);
                    table.ForeignKey(
                        name: "FK_Washes_Orders_orderID",
                        column: x => x.orderID,
                        principalTable: "Orders",
                        principalColumn: "oderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Washes_WashTypes_washTypeID1",
                        column: x => x.washTypeID1,
                        principalTable: "WashTypes",
                        principalColumn: "washTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    carID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    carTypeID = table.Column<int>(nullable: false),
                    personID = table.Column<int>(nullable: false),
                    licenceNr = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.carID);
                    table.ForeignKey(
                        name: "FK_Cars_CarTypes_carTypeID",
                        column: x => x.carTypeID,
                        principalTable: "CarTypes",
                        principalColumn: "carTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Persons_personID",
                        column: x => x.personID,
                        principalTable: "Persons",
                        principalColumn: "personID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Checks",
                columns: table => new
                {
                    checkID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    personID = table.Column<int>(nullable: false),
                    washID = table.Column<int>(nullable: false),
                    dateTimeCheck = table.Column<DateTime>(nullable: false),
                    amountExcludeVat = table.Column<int>(nullable: false),
                    amountWithVat = table.Column<int>(nullable: false),
                    vat = table.Column<int>(nullable: false),
                    comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checks", x => x.checkID);
                    table.ForeignKey(
                        name: "FK_Checks_Persons_personID",
                        column: x => x.personID,
                        principalTable: "Persons",
                        principalColumn: "personID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checks_Washes_washID",
                        column: x => x.washID,
                        principalTable: "Washes",
                        principalColumn: "washID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IsInWashes",
                columns: table => new
                {
                    isInWashID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    carID = table.Column<int>(nullable: false),
                    personID = table.Column<int>(nullable: false),
                    washID = table.Column<int>(nullable: false),
                    from = table.Column<TimeSpan>(nullable: false),
                    to = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsInWashes", x => x.isInWashID);
                    table.ForeignKey(
                        name: "FK_IsInWashes_Cars_carID",
                        column: x => x.carID,
                        principalTable: "Cars",
                        principalColumn: "carID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IsInWashes_Persons_personID",
                        column: x => x.personID,
                        principalTable: "Persons",
                        principalColumn: "personID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IsInWashes_Washes_washID",
                        column: x => x.washID,
                        principalTable: "Washes",
                        principalColumn: "washID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    discountID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    checkID = table.Column<int>(nullable: false),
                    washID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.discountID);
                    table.ForeignKey(
                        name: "FK_Discounts_Checks_checkID",
                        column: x => x.checkID,
                        principalTable: "Checks",
                        principalColumn: "checkID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Discounts_Washes_washID",
                        column: x => x.washID,
                        principalTable: "Washes",
                        principalColumn: "washID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    paymentID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    personID = table.Column<int>(nullable: false),
                    paymentMethodID = table.Column<int>(nullable: false),
                    checkID = table.Column<int>(nullable: false),
                    paymentAmount = table.Column<int>(nullable: false),
                    timeOfPayment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.paymentID);
                    table.ForeignKey(
                        name: "FK_Payments_Checks_checkID",
                        column: x => x.checkID,
                        principalTable: "Checks",
                        principalColumn: "checkID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethods_paymentMethodID",
                        column: x => x.paymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "paymentMethodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Persons_personID",
                        column: x => x.personID,
                        principalTable: "Persons",
                        principalColumn: "personID",
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
                name: "IX_Campaigns_serviceID",
                table: "Campaigns",
                column: "serviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_carTypeID",
                table: "Cars",
                column: "carTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_personID",
                table: "Cars",
                column: "personID");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_personID",
                table: "Checks",
                column: "personID");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_washID",
                table: "Checks",
                column: "washID");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_checkID",
                table: "Discounts",
                column: "checkID");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_washID",
                table: "Discounts",
                column: "washID");

            migrationBuilder.CreateIndex(
                name: "IX_IsInWashes_carID",
                table: "IsInWashes",
                column: "carID");

            migrationBuilder.CreateIndex(
                name: "IX_IsInWashes_personID",
                table: "IsInWashes",
                column: "personID");

            migrationBuilder.CreateIndex(
                name: "IX_IsInWashes_washID",
                table: "IsInWashes",
                column: "washID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_checkID",
                table: "Payments",
                column: "checkID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_paymentMethodID",
                table: "Payments",
                column: "paymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_personID",
                table: "Payments",
                column: "personID");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_personTypeID",
                table: "Persons",
                column: "personTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Washes_orderID",
                table: "Washes",
                column: "orderID");

            migrationBuilder.CreateIndex(
                name: "IX_Washes_washTypeID1",
                table: "Washes",
                column: "washTypeID1");
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
                name: "AspNetUsers");

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
                name: "PersonTypes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "WashTypes");
        }
    }
}
