using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tests_QuestionId",
                schema: "public",
                table: "Tests");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_QuestionId",
                schema: "public",
                table: "Tests",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tests_QuestionId",
                schema: "public",
                table: "Tests");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_QuestionId",
                schema: "public",
                table: "Tests",
                column: "QuestionId",
                unique: true);
        }
    }
}
