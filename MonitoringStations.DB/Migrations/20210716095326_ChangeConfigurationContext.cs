using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitoringStations.DB.Migrations
{
    public partial class ChangeConfigurationContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Stations",
                type: "bigint",
                nullable: false,
                defaultValueSql: "637620332055456836",
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Stations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValueSql: "637620332055456836");
        }
    }
}
