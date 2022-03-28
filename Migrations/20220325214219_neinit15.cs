using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_TestResponses_ResponsesId",
                schema: "public",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ResponsesId",
                schema: "public",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ResponsesId",
                schema: "public",
                table: "Questions");

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

            migrationBuilder.AddColumn<Guid>(
                name: "ResponsesId",
                schema: "public",
                table: "Questions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ResponsesId",
                schema: "public",
                table: "Questions",
                column: "ResponsesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_TestResponses_ResponsesId",
                schema: "public",
                table: "Questions",
                column: "ResponsesId",
                principalSchema: "public",
                principalTable: "TestResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
