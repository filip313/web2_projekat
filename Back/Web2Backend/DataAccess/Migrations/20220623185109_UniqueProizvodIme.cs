using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class UniqueProizvodIme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Proizvodi_Naziv",
                table: "Proizvodi",
                column: "Naziv",
                unique: true,
                filter: "[Naziv] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Proizvodi_Naziv",
                table: "Proizvodi");
        }
    }
}
