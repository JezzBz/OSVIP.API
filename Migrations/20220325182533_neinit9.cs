using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResponses_Tests_TestId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.DropIndex(
                name: "IX_TestResponses_TestId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.DropColumn(
                name: "TestId",
                schema: "public",
                table: "TestResponses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestId",
                schema: "public",
                table: "TestResponses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestResponses_TestId",
                schema: "public",
                table: "TestResponses",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResponses_Tests_TestId",
                schema: "public",
                table: "TestResponses",
                column: "TestId",
                principalSchema: "public",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
