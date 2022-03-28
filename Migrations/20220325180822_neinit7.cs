using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TestQuestionId",
                schema: "public",
                table: "TestResponses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestResponses_TestQuestionId",
                schema: "public",
                table: "TestResponses",
                column: "TestQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResponses_Questions_TestQuestionId",
                schema: "public",
                table: "TestResponses",
                column: "TestQuestionId",
                principalSchema: "public",
                principalTable: "Questions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResponses_Questions_TestQuestionId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.DropIndex(
                name: "IX_TestResponses_TestQuestionId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.DropColumn(
                name: "TestQuestionId",
                schema: "public",
                table: "TestResponses");
        }
    }
}
