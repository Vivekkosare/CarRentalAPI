using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    SSN = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('UTC', NOW())"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisterPickUpReturns",
                columns: table => new
                {
                    RegistrationId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    PickUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PickUpMeterReading = table.Column<int>(type: "integer", nullable: false),
                    Return = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReturnMeterReading = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('UTC', NOW())"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterPickUpReturns", x => x.RegistrationId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('UTC', NOW())"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CarCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('UTC', NOW())"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarCategory",
                columns: new[] { "Id", "Category" },
                values: new object[,]
                {
                    { new Guid("07e34f61-e90c-47d3-b90c-a94607270864"), "Minivan" },
                    { new Guid("5f6c717c-0f81-43dd-82a1-0fc6543e1607"), "Small" },
                    { new Guid("6c8bfba9-99b4-4000-a54d-a52930a4aa7a"), "Medium" },
                    { new Guid("88a2c05c-5505-4a3b-a04d-fa0b47a0e513"), "Large" },
                    { new Guid("9c614176-ad05-42f5-8b92-60465d1459f1"), "SUV" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "SSN", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0941b04f-e282-40ca-9435-cd10a1a78b4f"), "john.doe@example.com", "John", "Doe", "+46 70 123 4567", "123-45-6789", null },
                    { new Guid("59466a50-6844-41d4-b0d2-05ec732a8afa"), "jane.smith@example.com", "Jane", "Smith", "+46 70 234 5678", "987-65-4321", null },
                    { new Guid("683f140f-9811-4292-97c7-5cd063df6b1b"), "emma.johnson@example.com", "Emma", "Johnson", "+46 70 345 6789", "111-22-3333", null }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CategoryId", "Name", "RegistrationNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("40092e3e-625e-4262-8568-7c6fde1bb8f8"), new Guid("5f6c717c-0f81-43dd-82a1-0fc6543e1607"), "Toyota Yaris - ABC 15", "ABC 15", "Available", null },
                    { new Guid("502c070e-5245-4be8-b3af-18c0ce29298b"), new Guid("9c614176-ad05-42f5-8b92-60465d1459f1"), "Ford Explorer - HGU 45", "HGU 45", "Available", null },
                    { new Guid("8a2637ae-41dd-4625-b347-bbb4ca85546c"), new Guid("9c614176-ad05-42f5-8b92-60465d1459f1"), "Jeep Grand Cherokee - LOE 25", "LOE 25", "Booked", null },
                    { new Guid("a4e0cd0a-c5d9-4f8a-98e1-fc7d0c7d4c6d"), new Guid("6c8bfba9-99b4-4000-a54d-a52930a4aa7a"), "Honda Civic - DEF 23", "DEF 23", "Available", null },
                    { new Guid("ee56398f-38b7-4fce-9eb3-df84ba879f76"), new Guid("6c8bfba9-99b4-4000-a54d-a52930a4aa7a"), "Hyundai Elantra - SUE 09", "SUE 09", "Booked", null },
                    { new Guid("fb526a44-f175-4534-9091-2392e0420094"), new Guid("07e34f61-e90c-47d3-b90c-a94607270864"), "Chrysler Pacifica - OEP 25", "OEP 25", "Available", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CarId",
                table: "Bookings",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "RegisterPickUpReturns");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CarCategory");
        }
    }
}
