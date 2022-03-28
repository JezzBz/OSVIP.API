using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResponses_Questions_TestQuestionId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.DropIndex(
                name: "IX_Tests_QuestionId",
                schema: "public",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_TestResponses_TestQuestionId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.DropColumn(
                name: "TestQuestionId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                schema: "public",
                table: "TestResponses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tests_QuestionId",
                schema: "public",
                table: "Tests",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestResponses_QuestionId",
                schema: "public",
                table: "TestResponses",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResponses_Questions_QuestionId",
                schema: "public",
                table: "TestResponses",
                column: "QuestionId",
                principalSchema: "public",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResponses_Questions_QuestionId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.DropIndex(
                name: "IX_Tests_QuestionId",
                schema: "public",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_TestResponses_QuestionId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.AddColumn<Guid>(
                name: "TestQuestionId",
                schema: "public",
                table: "TestResponses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_QuestionId",
                schema: "public",
                table: "Tests",
                column: "QuestionId");

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
    }
}
