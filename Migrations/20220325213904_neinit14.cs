using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResponses_Questions_QuestionId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Questions_QuestionId",
                schema: "public",
                table: "Tests");

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
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                schema: "public",
                table: "TestResponses");

            migrationBuilder.AddColumn<Guid>(
                name: "ResponsesId",
                schema: "public",
                table: "Questions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                schema: "public",
                table: "Questions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ResponsesId",
                schema: "public",
                table: "Questions",
                column: "ResponsesId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestId",
                schema: "public",
                table: "Questions",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_TestResponses_ResponsesId",
                schema: "public",
                table: "Questions",
                column: "ResponsesId",
                principalSchema: "public",
                principalTable: "TestResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tests_TestId",
                schema: "public",
                table: "Questions",
                column: "TestId",
                principalSchema: "public",
                principalTable: "Tests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_TestResponses_ResponsesId",
                schema: "public",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tests_TestId",
                schema: "public",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ResponsesId",
                schema: "public",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TestId",
                schema: "public",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ResponsesId",
                schema: "public",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TestId",
                schema: "public",
                table: "Questions");

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                schema: "public",
                table: "Tests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                column: "QuestionId");

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
    }
}
