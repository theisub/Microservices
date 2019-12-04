using Microsoft.EntityFrameworkCore.Migrations;

namespace BusAPI.Migrations
{
    public partial class Buses_AddedTimeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TravelTime",
                table: "Buses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TravelTime",
                table: "Buses");
        }
    }
}
