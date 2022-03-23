using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class adsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationFile",
                schema: "public",
                table: "Transfers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MarksFile",
                schema: "public",
                table: "Transfers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telehone",
                schema: "public",
                table: "Transfers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationFile",
                schema: "public",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "MarksFile",
                schema: "public",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "Telehone",
                schema: "public",
                table: "Transfers");
        }
    }
}
