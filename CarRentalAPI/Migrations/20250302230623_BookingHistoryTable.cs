using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalAPI.Migrations
{
    /// <inheritdoc />
    public partial class BookingHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterPickUpReturns_Bookings_BookingId",
                table: "RegisterPickUpReturns");

            migrationBuilder.DropIndex(
                name: "IX_RegisterPickUpReturns_BookingId",
                table: "RegisterPickUpReturns");

            migrationBuilder.AddColumn<decimal>(
                name: "RentalPrice",
                table: "RegisterPickUpReturns",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "BookingHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    StatusChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('UTC', NOW())"),
                    MeterReading = table.Column<int>(type: "integer", nullable: true),
                    RentalPriceCalculated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingHistory", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingHistory");

            migrationBuilder.DropColumn(
                name: "RentalPrice",
                table: "RegisterPickUpReturns");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterPickUpReturns_BookingId",
                table: "RegisterPickUpReturns",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterPickUpReturns_Bookings_BookingId",
                table: "RegisterPickUpReturns",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
