using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentMeterReadingToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReturnMeterReading",
                table: "RegisterPickUpReturns",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "CustomerSSN",
                table: "RegisterPickUpReturns",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CurrentMeterReading",
                table: "Cars",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("40092e3e-625e-4262-8568-7c6fde1bb8f8"),
                column: "CurrentMeterReading",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("502c070e-5245-4be8-b3af-18c0ce29298b"),
                column: "CurrentMeterReading",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("8a2637ae-41dd-4625-b347-bbb4ca85546c"),
                column: "CurrentMeterReading",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("a4e0cd0a-c5d9-4f8a-98e1-fc7d0c7d4c6d"),
                column: "CurrentMeterReading",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("ee56398f-38b7-4fce-9eb3-df84ba879f76"),
                column: "CurrentMeterReading",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("fb526a44-f175-4534-9091-2392e0420094"),
                column: "CurrentMeterReading",
                value: 100);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterPickUpReturns_Bookings_BookingId",
                table: "RegisterPickUpReturns");

            migrationBuilder.DropIndex(
                name: "IX_RegisterPickUpReturns_BookingId",
                table: "RegisterPickUpReturns");

            migrationBuilder.DropColumn(
                name: "CustomerSSN",
                table: "RegisterPickUpReturns");

            migrationBuilder.DropColumn(
                name: "CurrentMeterReading",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "ReturnMeterReading",
                table: "RegisterPickUpReturns",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "CurrentMeterReading",
                table: "Cars");
        }
    }
}
