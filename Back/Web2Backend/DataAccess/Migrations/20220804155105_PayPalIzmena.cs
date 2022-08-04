using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class PayPalIzmena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NacinPlacanja",
                table: "Porudzbine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PayPalId",
                table: "Porudzbine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayPalStatus",
                table: "Porudzbine",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NacinPlacanja",
                table: "Porudzbine");

            migrationBuilder.DropColumn(
                name: "PayPalId",
                table: "Porudzbine");

            migrationBuilder.DropColumn(
                name: "PayPalStatus",
                table: "Porudzbine");
        }
    }
}
