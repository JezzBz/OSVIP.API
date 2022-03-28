using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Users_Tests_TestId",
                schema: "public",
                table: "Users",
                column: "TestId",
                principalSchema: "public",
                principalTable: "Tests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tests_TestId",
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
        }
    }
}
