using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitoringStations.DB.Migrations
{
    public partial class AddedFieldsToHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrinterNotify",
                schema: "dbo",
                table: "StationHistory");

            migrationBuilder.DropColumn(
                name: "PrinterStatus",
                schema: "dbo",
                table: "StationHistory");

            migrationBuilder.AddColumn<string>(
                name: "PrinterInfo",
                schema: "dbo",
                table: "StationHistory",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrinterMsg",
                schema: "dbo",
                table: "StationHistory",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrinterName",
                schema: "dbo",
                table: "StationHistory",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrinterState",
                schema: "dbo",
                table: "StationHistory",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrinterInfo",
                schema: "dbo",
                table: "StationHistory");

            migrationBuilder.DropColumn(
                name: "PrinterMsg",
                schema: "dbo",
                table: "StationHistory");

            migrationBuilder.DropColumn(
                name: "PrinterName",
                schema: "dbo",
                table: "StationHistory");

            migrationBuilder.DropColumn(
                name: "PrinterState",
                schema: "dbo",
                table: "StationHistory");

            migrationBuilder.AddColumn<string>(
                name: "PrinterNotify",
                schema: "dbo",
                table: "StationHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrinterStatus",
                schema: "dbo",
                table: "StationHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
