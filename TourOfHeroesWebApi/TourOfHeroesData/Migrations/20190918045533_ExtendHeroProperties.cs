using Microsoft.EntityFrameworkCore.Migrations;

namespace TourOfHeroesData.Migrations
{
    public partial class ExtendHeroProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Birthday",
                table: "Heroes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Heroes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RealName",
                table: "Heroes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "RealName",
                table: "Heroes");
        }
    }
}
