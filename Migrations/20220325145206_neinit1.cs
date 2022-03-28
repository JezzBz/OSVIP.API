using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question",
                schema: "public",
                table: "Tests");

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                schema: "public",
                table: "Tests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_QuestionId",
                schema: "public",
                table: "Tests",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Questions_QuestionId",
                schema: "public",
                table: "Tests",
                column: "QuestionId",
                principalSchema: "public",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Questions_QuestionId",
                schema: "public",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Tests_QuestionId",
                schema: "public",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                schema: "public",
                table: "Tests");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                schema: "public",
                table: "Tests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
