using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitoringStations.DB.Migrations
{
    public partial class AddRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StationHistory_Stations_StationId",
                table: "StationHistory");

            migrationBuilder.AlterColumn<long>(
                name: "StationId",
                table: "StationHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StationHistory_Stations_StationId",
                table: "StationHistory",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StationHistory_Stations_StationId",
                table: "StationHistory");

            migrationBuilder.AlterColumn<long>(
                name: "StationId",
                table: "StationHistory",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_StationHistory_Stations_StationId",
                table: "StationHistory",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
