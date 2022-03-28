using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersTests_TestId",
                schema: "public",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TestId",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TestId",
                schema: "public",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "public",
                table: "UsersTests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UsersTests_UserId",
                schema: "public",
                table: "UsersTests",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTests_Users_UserId",
                schema: "public",
                table: "UsersTests",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersTests_Users_UserId",
                schema: "public",
                table: "UsersTests");

            migrationBuilder.DropIndex(
                name: "IX_UsersTests_UserId",
                schema: "public",
                table: "UsersTests");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "public",
                table: "UsersTests");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                schema: "public",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TestId",
                schema: "public",
                table: "Users",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UsersTests_TestId",
                schema: "public",
                table: "Users",
                column: "TestId",
                principalSchema: "public",
                principalTable: "UsersTests",
                principalColumn: "Id");
        }
    }
}
