using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Directions_DirectionId",
                schema: "public",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_DirectionId",
                schema: "public",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "DirectionId",
                schema: "public",
                table: "Tests",
                newName: "Course");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Course",
                schema: "public",
                table: "Tests",
                newName: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_DirectionId",
                schema: "public",
                table: "Tests",
                column: "DirectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Directions_DirectionId",
                schema: "public",
                table: "Tests",
                column: "DirectionId",
                principalSchema: "public",
                principalTable: "Directions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
