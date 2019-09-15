using Microsoft.EntityFrameworkCore.Migrations;

namespace TourOfHeroesData.Migrations
{
    public partial class EditHistoryEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditHistory_Heroes_HeroId",
                table: "EditHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EditHistory",
                table: "EditHistory");

            migrationBuilder.RenameTable(
                name: "EditHistory",
                newName: "EditHistories");

            migrationBuilder.RenameIndex(
                name: "IX_EditHistory_HeroId",
                table: "EditHistories",
                newName: "IX_EditHistories_HeroId");

            migrationBuilder.AlterColumn<int>(
                name: "HeroId",
                table: "EditHistories",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EditHistories",
                table: "EditHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EditHistories_Heroes_HeroId",
                table: "EditHistories",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditHistories_Heroes_HeroId",
                table: "EditHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EditHistories",
                table: "EditHistories");

            migrationBuilder.RenameTable(
                name: "EditHistories",
                newName: "EditHistory");

            migrationBuilder.RenameIndex(
                name: "IX_EditHistories_HeroId",
                table: "EditHistory",
                newName: "IX_EditHistory_HeroId");

            migrationBuilder.AlterColumn<int>(
                name: "HeroId",
                table: "EditHistory",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_EditHistory",
                table: "EditHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EditHistory_Heroes_HeroId",
                table: "EditHistory",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
