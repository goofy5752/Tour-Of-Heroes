using Microsoft.EntityFrameworkCore.Migrations;

namespace TourOfHeroesData.Migrations
{
    public partial class ImagePropertyForUserAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "ApplicationUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "ApplicationUsers");
        }
    }
}
