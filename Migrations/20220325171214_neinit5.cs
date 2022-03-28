using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osvip.Api.Migrations
{
    public partial class neinit5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersTests_Tests_TestId",
                schema: "public",
                table: "UsersTests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TestStartTime",
                schema: "public",
                table: "UsersTests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                schema: "public",
                table: "UsersTests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Result",
                schema: "public",
                table: "UsersTests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTests_Tests_TestId",
                schema: "public",
                table: "UsersTests",
                column: "TestId",
                principalSchema: "public",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersTests_Tests_TestId",
                schema: "public",
                table: "UsersTests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TestStartTime",
                schema: "public",
                table: "UsersTests",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                schema: "public",
                table: "UsersTests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Result",
                schema: "public",
                table: "UsersTests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTests_Tests_TestId",
                schema: "public",
                table: "UsersTests",
                column: "TestId",
                principalSchema: "public",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
