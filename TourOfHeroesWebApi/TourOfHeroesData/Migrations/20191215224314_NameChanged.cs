using Microsoft.EntityFrameworkCore.Migrations;

namespace TourOfHeroesData.Migrations
{
    public partial class NameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBlogDisLikes_Blogs_BlogId",
                table: "UserBlogDisLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBlogDisLikes_AspNetUsers_UserId",
                table: "UserBlogDisLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBlogDisLikes",
                table: "UserBlogDisLikes");

            migrationBuilder.RenameTable(
                name: "UserBlogDisLikes",
                newName: "UserBlogDislikes");

            migrationBuilder.RenameIndex(
                name: "IX_UserBlogDisLikes_BlogId",
                table: "UserBlogDislikes",
                newName: "IX_UserBlogDislikes_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBlogDislikes",
                table: "UserBlogDislikes",
                columns: new[] { "UserId", "BlogId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlogDislikes_Blogs_BlogId",
                table: "UserBlogDislikes",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlogDislikes_AspNetUsers_UserId",
                table: "UserBlogDislikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBlogDislikes_Blogs_BlogId",
                table: "UserBlogDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBlogDislikes_AspNetUsers_UserId",
                table: "UserBlogDislikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBlogDislikes",
                table: "UserBlogDislikes");

            migrationBuilder.RenameTable(
                name: "UserBlogDislikes",
                newName: "UserBlogDisLikes");

            migrationBuilder.RenameIndex(
                name: "IX_UserBlogDislikes_BlogId",
                table: "UserBlogDisLikes",
                newName: "IX_UserBlogDisLikes_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBlogDisLikes",
                table: "UserBlogDisLikes",
                columns: new[] { "UserId", "BlogId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlogDisLikes_Blogs_BlogId",
                table: "UserBlogDisLikes",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlogDisLikes_AspNetUsers_UserId",
                table: "UserBlogDisLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
