using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitoringStations.DB.Migrations
{
    public partial class AddUniqueHostnameMacAddressConstrain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Stations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddUniqueConstraint(
                name: "IX_MultipleColumns",
                table: "Stations",
                columns: new[] { "Hostname", "MacAddress" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Stations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "PrinterMsg",
                table: "StationHistory",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_Hostname_MacAddress",
                table: "Stations",
                columns: new[] { "Hostname", "MacAddress" },
                unique: true);
        }
    }
}
