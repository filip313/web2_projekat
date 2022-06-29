using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class UkljonjenStatusNalogaUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusNaloga",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusNaloga",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "NaCekanju");
        }
    }
}
