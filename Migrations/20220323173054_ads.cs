using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class ads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telehone",
                schema: "public",
                table: "Transfers",
                newName: "Telephone");

            migrationBuilder.RenameColumn(
                name: "RequsetType",
                schema: "public",
                table: "Transfers",
                newName: "RequestType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telephone",
                schema: "public",
                table: "Transfers",
                newName: "Telehone");

            migrationBuilder.RenameColumn(
                name: "RequestType",
                schema: "public",
                table: "Transfers",
                newName: "RequsetType");
        }
    }
}
