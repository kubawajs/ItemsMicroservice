using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItemsMicroservice.Infrastructure.Migrations
{
    public partial class SetStringLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a42c4d25-0710-4999-a6ad-7d65e8627a6a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83b69cbf-79ff-4799-8d0c-406cb033ee21", "fb3b967a-627c-4c94-8544-b4fd4b8926b6", "ItemsAdmin", "ITEMSADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83b69cbf-79ff-4799-8d0c-406cb033ee21");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a42c4d25-0710-4999-a6ad-7d65e8627a6a", "9b8dda28-5ca1-410d-bde6-b105ffffe2bb", "ItemsAdmin", "ITEMSADMIN" });
        }
    }
}
