using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItemsMicroservice.Infrastructure.Migrations
{
    public partial class AddSystemDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb45aff9-61a8-4299-aa81-08ad8666db11");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Items",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Items",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a42c4d25-0710-4999-a6ad-7d65e8627a6a", "9b8dda28-5ca1-410d-bde6-b105ffffe2bb", "ItemsAdmin", "ITEMSADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a42c4d25-0710-4999-a6ad-7d65e8627a6a");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Items");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb45aff9-61a8-4299-aa81-08ad8666db11", "735444f1-ed1e-4c22-87c2-5e2dada7884f", "ItemsAdmin", "ITEMSADMIN" });
        }
    }
}
