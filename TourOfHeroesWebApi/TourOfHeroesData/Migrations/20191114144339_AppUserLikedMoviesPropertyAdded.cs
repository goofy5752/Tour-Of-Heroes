using Microsoft.EntityFrameworkCore.Migrations;

namespace TourOfHeroesData.Migrations
{
    public partial class AppUserLikedMoviesPropertyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Movies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ApplicationUserId",
                table: "Movies",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId",
                table: "Movies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ApplicationUserId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Movies");
        }
    }
}
