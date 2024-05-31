using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EccomerceApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntryType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LossReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LossReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Peoples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peoples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    PeopleId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EntryTypeId = table.Column<int>(type: "int", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_EntryType_EntryTypeId",
                        column: x => x.EntryTypeId,
                        principalTable: "EntryType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entries_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Losses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Losses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Losses_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Existence = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InitialQuantity = table.Column<int>(type: "int", nullable: false),
                    RemainingQuantity = table.Column<int>(type: "int", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batches_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LostDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LossId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    LossReasonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LostDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LostDetails_LossReasons_LossReasonId",
                        column: x => x.LossReasonId,
                        principalTable: "LossReasons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LostDetails_Losses_LossId",
                        column: x => x.LossId,
                        principalTable: "Losses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LostDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPhotos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sensor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessorSpeed = table.Column<double>(type: "float", nullable: true),
                    ScreenSize = table.Column<double>(type: "float", nullable: true),
                    ScreenResolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenTechnology = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RearCameraResolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontCameraResolution = table.Column<int>(type: "int", nullable: true),
                    RAM = table.Column<int>(type: "int", nullable: true),
                    InternalStorage = table.Column<int>(type: "int", nullable: true),
                    SimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimCount = table.Column<int>(type: "int", nullable: true),
                    NFC = table.Column<bool>(type: "bit", nullable: true),
                    BluetoothVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsbInterface = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatingSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatteryCapacity = table.Column<int>(type: "int", nullable: true),
                    Waterproof = table.Column<bool>(type: "bit", nullable: true),
                    WaterResistanceRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SplashResistant = table.Column<bool>(type: "bit", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSpecifications_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    EntryTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryDetails_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryDetails_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntryDetails_EntryType_EntryTypeId",
                        column: x => x.EntryTypeId,
                        principalTable: "EntryType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntryDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    SaleId = table.Column<int>(type: "int", nullable: true),
                    BatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleDetails_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "admin", "ADMIN" },
                    { "2", null, "user", "USER" },
                    { "3", null, "managed", "MANAGED" }
                });

            migrationBuilder.InsertData(
                table: "EntryType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Compra" },
                    { 2, "Transferencia de inventario" },
                    { 3, "Devolución de cliente" },
                    { 4, "Donación recibida" },
                    { 5, "Muestra gratuita" }
                });

            migrationBuilder.InsertData(
                table: "LossReasons",
                columns: new[] { "Id", "Reason" },
                values: new object[,]
                {
                    { 1, "Daño durante el transporte" },
                    { 2, "Fecha de caducidad vencida" },
                    { 3, "Robo" },
                    { 4, "Producto dañado en el almacén" },
                    { 5, "Devolución del cliente" },
                    { 6, "Muestra gratuita" }
                });

            migrationBuilder.InsertData(
                table: "Peoples",
                columns: new[] { "Id", "Address", "LastName", "Name" },
                values: new object[,]
                {
                    { 1, "123 Elm St", "Lopez", "Luis" },
                    { 2, "431 Elm St", "Muñoz", "Jeampierre" },
                    { 3, "233 Elm St", "benedicto", "Jean" },
                    { 4, "432 Elm St", "Zambrano", "Fabrizzio" },
                    { 5, "32 Elm St", "Ambrosio", "Fabian" }
                });

            migrationBuilder.InsertData(
                table: "ProductBrands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Samsung" },
                    { 2, "Apple" },
                    { 3, "Huawei" },
                    { 4, "Xiaomi" },
                    { 5, "Motorola" },
                    { 6, "Lg" },
                    { 7, "Sony" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Gama Baja" },
                    { 2, "Gama Media" },
                    { 3, "Gama Alta" },
                    { 4, "Gama Top" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Activo" },
                    { 2, "Inactivo" },
                    { 3, "En espera" },
                    { 4, "En proceso" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PeopleId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StateId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "9ceb7150-33a2-442b-a620-75d12dcd9fc5", "AppUser", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEBAUaKxFIqeqZ04cmAmythvZZ+vAbWKbENXf7Ods3FkEprZcNf3jwQUXzF10lxDHvA==", 1, null, false, "", 1, false, "admin" },
                    { "2", 0, "5064c4d2-420d-4b3f-81da-7910cf0efe3c", "AppUser", "user@example.com", true, false, null, "USER@EXAMPLE.COM", "USER", "AQAAAAIAAYagAAAAEDMyDe4SJ0Nuv2VOUmmAdukVQf6QH4i0EgETIcZB++Gs+y0xOd6U+e5Sd82Mm25LoQ==", 2, null, false, "", 1, false, "user" },
                    { "3", 0, "a08d9a32-52cd-4a75-8e90-994fd809adbb", "AppUser", "managed@example.com", true, false, null, "MANAGED@EXAMPLE.COM", "MANAGED", "AQAAAAIAAYagAAAAEGHRA+KpPk3ibw8dbTf5hyJJxgMh5KUzpkgxggzUUSiDXyLQnmHJhrb1JCTTPoI5cg==", 3, null, false, "", 1, false, "managed" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" },
                    { "3", "3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "IX_AspNetUsers_PeopleId",
                table: "AspNetUsers",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StateId",
                table: "AspNetUsers",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ProductId",
                table: "Batches",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_EntryTypeId",
                table: "Entries",
                column: "EntryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_StateId",
                table: "Entries",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryDetails_BatchId",
                table: "EntryDetails",
                column: "BatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntryDetails_EntryId",
                table: "EntryDetails",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryDetails_EntryTypeId",
                table: "EntryDetails",
                column: "EntryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryDetails_ProductId",
                table: "EntryDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Losses_StateId",
                table: "Losses",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_LostDetails_LossId",
                table: "LostDetails",
                column: "LossId");

            migrationBuilder.CreateIndex(
                name: "IX_LostDetails_LossReasonId",
                table: "LostDetails",
                column: "LossReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_LostDetails_ProductId",
                table: "LostDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhotos_ProductId",
                table: "ProductPhotos",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBrandId",
                table: "Products",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StateId",
                table: "Products",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecifications_ProductId",
                table: "ProductSpecifications",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_BatchId",
                table: "SaleDetails",
                column: "BatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_ProductId",
                table: "SaleDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_SaleId",
                table: "SaleDetails",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_StateId",
                table: "Sales",
                column: "StateId");
        }

        /// <inheritdoc />
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
                name: "EntryDetails");

            migrationBuilder.DropTable(
                name: "LostDetails");

            migrationBuilder.DropTable(
                name: "ProductPhotos");

            migrationBuilder.DropTable(
                name: "ProductSpecifications");

            migrationBuilder.DropTable(
                name: "SaleDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "LossReasons");

            migrationBuilder.DropTable(
                name: "Losses");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Peoples");

            migrationBuilder.DropTable(
                name: "EntryType");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductBrands");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
