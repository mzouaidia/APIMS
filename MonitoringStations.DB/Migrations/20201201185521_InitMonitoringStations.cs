using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitoringStations.DB.Migrations
{
    public partial class InitMonitoringStations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Stations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Hostname = table.Column<string>(nullable: false),
                    AddressIp = table.Column<string>(nullable: false),
                    StoreNumber = table.Column<string>(nullable: false),
                    MacAddress = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastModify = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StationHistory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    PrinterStatus = table.Column<string>(nullable: false),
                    PrinterNotify = table.Column<string>(nullable: false),
                    AddressIp = table.Column<string>(nullable: false),
                    StoreNumber = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    StationId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StationHistory_Stations_StationId",
                        column: x => x.StationId,
                        principalSchema: "dbo",
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StationHistory_StationId",
                schema: "dbo",
                table: "StationHistory",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_Hostname_MacAddress",
                schema: "dbo",
                table: "Stations",
                columns: new[] { "Hostname", "MacAddress" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StationHistory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Stations",
                schema: "dbo");
        }
    }
}
