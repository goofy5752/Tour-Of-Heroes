using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourOfHeroesData.Migrations
{
    public partial class EntitiesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "RealName",
                table: "Heroes",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Heroes",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Heroes",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Heroes",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Heroes",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CoverImage",
                table: "Heroes",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Heroes",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OldValue",
                table: "EditHistory",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NewValue",
                table: "EditHistory",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "RealName",
                table: "Heroes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Heroes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Heroes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Heroes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Heroes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "CoverImage",
                table: "Heroes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Birthday",
                table: "Heroes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "OldValue",
                table: "EditHistories",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "NewValue",
                table: "EditHistories",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

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
    }
}
