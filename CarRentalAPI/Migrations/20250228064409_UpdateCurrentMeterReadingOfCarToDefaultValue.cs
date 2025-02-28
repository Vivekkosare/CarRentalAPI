using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCurrentMeterReadingOfCarToDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
