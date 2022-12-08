using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Refuntations_App.Data.Migrations
{
    public partial class seedroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0fc66576-f26c-41a1-81b4-2cc4f62ca364", "0382a3b9-981d-4ea5-bbef-2a4abfd7ac0f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b9e5c46d-2c44-4c82-b3a0-d2bcf74357eb", "dfba0811-8d37-459c-9c6c-a7ffc875f2b9", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fc66576-f26c-41a1-81b4-2cc4f62ca364");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9e5c46d-2c44-4c82-b3a0-d2bcf74357eb");
        }
    }
}
