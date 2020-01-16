using Microsoft.EntityFrameworkCore.Migrations;

namespace BusAPI.Migrations
{
    public partial class BusesNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusCompany = table.Column<string>(nullable: true),
                    InCountry = table.Column<string>(nullable: true),
                    OutCountry = table.Column<string>(nullable: true),
                    InCity = table.Column<string>(nullable: true),
                    OutCity = table.Column<string>(nullable: true),
                    Price = table.Column<long>(nullable: false),
                    Transit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buses");
        }
    }
}
