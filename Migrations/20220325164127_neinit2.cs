using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestResult",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TestStartTime",
                schema: "public",
                table: "Users");
        }
    }
}
