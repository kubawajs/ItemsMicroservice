using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItemsMicroservice.Infrastructure.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb45aff9-61a8-4299-aa81-08ad8666db11", "735444f1-ed1e-4c22-87c2-5e2dada7884f", "ItemsAdmin", "ITEMSADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb45aff9-61a8-4299-aa81-08ad8666db11");
        }
    }
}
