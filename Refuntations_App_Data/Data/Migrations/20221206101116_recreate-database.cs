using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Refuntations_App.Data.Migrations
{
    public partial class recreatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fc66576-f26c-41a1-81b4-2cc4f62ca364");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9e5c46d-2c44-4c82-b3a0-d2bcf74357eb");

            migrationBuilder.CreateTable(
                name: "finalSettlement",
                columns: table => new
                {
                    id_iznos_stopa_1 = table.Column<int>(type: "int", nullable: true),
                    id_iznos_stopa_2 = table.Column<int>(type: "int", nullable: true),
                    sifra_dob = table.Column<int>(type: "int", nullable: true),
                    Dobavljac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sifra_kat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kategorija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sifra_AA = table.Column<int>(type: "int", nullable: true),
                    Vrsta_AA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iznos_refundacije_stopa_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Iznos_refundacije_stopa_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Iznos_realizovano_stopa_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Iznos_realizovano_stopa_2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Datum_realizovano = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status_stavke_obracuna = table.Column<short>(type: "smallint", nullable: true),
                    I = table.Column<bool>(type: "bit", nullable: true),
                    NP = table.Column<bool>(type: "bit", nullable: false),
                    datum_od_aa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    datum_do_aa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    obrada = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tab_refundacije_sif_interni_dobavljaci",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sifra_kat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_refundacije_sif_interni_dobavljaci", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "691c2361-0d10-40e5-8aa5-827b29be65f7", "1701793a-4bd7-4b5f-a55c-1ad46cc9c572", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a5507fc4-169b-4d7f-adf2-b14ebd4b8f1f", "0fd3a8dc-af5a-441a-ba13-8e6630414d07", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "finalSettlement");

            migrationBuilder.DropTable(
                name: "tab_refundacije_sif_interni_dobavljaci");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "691c2361-0d10-40e5-8aa5-827b29be65f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5507fc4-169b-4d7f-adf2-b14ebd4b8f1f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0fc66576-f26c-41a1-81b4-2cc4f62ca364", "0382a3b9-981d-4ea5-bbef-2a4abfd7ac0f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b9e5c46d-2c44-4c82-b3a0-d2bcf74357eb", "dfba0811-8d37-459c-9c6c-a7ffc875f2b9", "User", "USER" });
        }
    }
}
