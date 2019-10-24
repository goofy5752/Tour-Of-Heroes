using Microsoft.EntityFrameworkCore.Migrations;

namespace TourOfHeroesData.Migrations
{
    public partial class UsernameAndProfileImageToCommentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Comments",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comments",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comments");
        }
    }
}
