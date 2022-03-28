using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                schema: "public",
                table: "UsersTests");

            migrationBuilder.AddColumn<int>(
                name: "Result",
                schema: "public",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                schema: "public",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Result",
                schema: "public",
                table: "UsersTests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
