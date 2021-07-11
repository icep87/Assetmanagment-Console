using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniProject_Console.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchangeRate = table.Column<float>(type: "real", nullable: false),
                    ExchangeRateLatestUpdate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offices_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mobile Device" },
                    { 2, "Laptop Computers" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "ExchangeRate", "ExchangeRateLatestUpdate", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, 0f, new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "United States dollar", "USD" },
                    { 2, 8.49f, new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Swedish Krona", "SEK" },
                    { 3, 3.79f, new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Polish Zloty", "PLN" }
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Country", "CurrencyId", "Name" },
                values: new object[] { 3, "USA", 1, "New York" });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Country", "CurrencyId", "Name" },
                values: new object[] { 1, "Sweden", 2, "Malmoe" });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Country", "CurrencyId", "Name" },
                values: new object[] { 2, "Poland", 3, "Warsaw" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CurrencyId", "Name", "OfficeId", "Price", "PurchaseDate" },
                values: new object[,]
                {
                    { 1, 2, 2, "MacBook Pro", 1, 12500f, new DateTime(2018, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 2, "iPhone 11", 1, 12500f, new DateTime(2021, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, 2, "iPhone 11 Max", 1, 18990f, new DateTime(2020, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 3, "Macbook Air", 2, 4500f, new DateTime(2018, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 3, "iPhone 12", 2, 5000f, new DateTime(2020, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offices_CurrencyId",
                table: "Offices",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CurrencyId",
                table: "Products",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OfficeId",
                table: "Products",
                column: "OfficeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
