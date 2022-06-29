using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class IspravljenConfigPorudzbina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Porudzbine_Users_DostavljacId1",
                table: "Porudzbine");

            migrationBuilder.DropIndex(
                name: "IX_Porudzbine_DostavljacId1",
                table: "Porudzbine");

            migrationBuilder.DropColumn(
                name: "DostavljacId1",
                table: "Porudzbine");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_NarucilacId",
                table: "Porudzbine",
                column: "NarucilacId");

            migrationBuilder.AddForeignKey(
                name: "FK_Porudzbine_Users_NarucilacId",
                table: "Porudzbine",
                column: "NarucilacId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Porudzbine_Users_NarucilacId",
                table: "Porudzbine");

            migrationBuilder.DropIndex(
                name: "IX_Porudzbine_NarucilacId",
                table: "Porudzbine");

            migrationBuilder.AddColumn<int>(
                name: "DostavljacId1",
                table: "Porudzbine",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_DostavljacId1",
                table: "Porudzbine",
                column: "DostavljacId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Porudzbine_Users_DostavljacId1",
                table: "Porudzbine",
                column: "DostavljacId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
