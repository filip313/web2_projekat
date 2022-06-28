using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Init_V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proizvodi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cena = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Sastojci = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusNaloga = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "NaCekanju"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DatumRodjenja = table.Column<string>(type: "nvarchar(48)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Komentar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cena = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    TrajanjeDostave = table.Column<long>(type: "bigint", nullable: false),
                    VremePrihvata = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "CekaDostavu"),
                    NarucilacId = table.Column<int>(type: "int", nullable: false),
                    DostavljacId = table.Column<int>(type: "int", nullable: false),
                    DostavljacId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Porudzbine_Users_DostavljacId",
                        column: x => x.DostavljacId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Porudzbine_Users_DostavljacId1",
                        column: x => x.DostavljacId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PorudzbinaProizvod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PorudzbinaId = table.Column<int>(type: "int", nullable: false),
                    ProizvodId = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PorudzbinaProizvod", x => x.Id);
                    table.UniqueConstraint("AK_PorudzbinaProizvod_PorudzbinaId_ProizvodId", x => new { x.PorudzbinaId, x.ProizvodId });
                    table.ForeignKey(
                        name: "FK_PorudzbinaProizvod_Porudzbine_PorudzbinaId",
                        column: x => x.PorudzbinaId,
                        principalTable: "Porudzbine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PorudzbinaProizvod_Proizvodi_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvodi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PorudzbinaProizvod_ProizvodId",
                table: "PorudzbinaProizvod",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_DostavljacId",
                table: "Porudzbine",
                column: "DostavljacId");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_DostavljacId1",
                table: "Porudzbine",
                column: "DostavljacId1");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodi_Naziv",
                table: "Proizvodi",
                column: "Naziv",
                unique: true,
                filter: "[Naziv] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PorudzbinaProizvod");

            migrationBuilder.DropTable(
                name: "Porudzbine");

            migrationBuilder.DropTable(
                name: "Proizvodi");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
