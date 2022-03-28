using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tests_TestId",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TestResult",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TestStartTime",
                schema: "public",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UsersTests",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestId = table.Column<int>(type: "integer", nullable: true),
                    TestStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Result = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersTests_Tests_TestId",
                        column: x => x.TestId,
                        principalSchema: "public",
                        principalTable: "Tests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersTests_TestId",
                schema: "public",
                table: "UsersTests",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersTests_TestId",
                schema: "public",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UsersTests",
                schema: "public");

            migrationBuilder.AddColumn<int>(
                name: "TestResult",
                schema: "public",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TestStartTime",
                schema: "public",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tests_TestId",
                schema: "public",
                table: "Users",
                column: "TestId",
                principalSchema: "public",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
