using Microsoft.EntityFrameworkCore.Migrations;

namespace PlaneAPI.Migrations
{
    public partial class Planes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaneCompany = table.Column<string>(nullable: true),
                    InCountry = table.Column<string>(nullable: true),
                    OutCountry = table.Column<string>(nullable: true),
                    InCity = table.Column<string>(nullable: true),
                    OutCity = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Transit = table.Column<bool>(nullable: false),
                    TravelTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Planes");
        }
    }
}
