using Microsoft.EntityFrameworkCore.Migrations;

namespace TourOfHeroesData.Migrations
{
    public partial class UserActivityEntityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_AspNetUsers_UserId1",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_UserId1",
                table: "UserActivities");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserActivities");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserActivities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId",
                table: "UserActivities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_AspNetUsers_UserId",
                table: "UserActivities",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_AspNetUsers_UserId",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_UserId",
                table: "UserActivities");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserActivities",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserActivities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId1",
                table: "UserActivities",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_AspNetUsers_UserId1",
                table: "UserActivities",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
