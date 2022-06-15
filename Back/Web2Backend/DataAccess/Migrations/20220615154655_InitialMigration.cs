using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 320, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Ime = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Prezime = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DatumRodjenja = table.Column<string>(type: "TEXT", nullable: false),
                    Adresa = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    UserType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Slika = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admini", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dostavljaci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StatusNaloga = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "NaCekanju"),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 320, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Ime = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Prezime = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DatumRodjenja = table.Column<string>(type: "TEXT", nullable: false),
                    Adresa = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    UserType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Slika = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dostavljaci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Potrosaci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 320, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Ime = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Prezime = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DatumRodjenja = table.Column<string>(type: "TEXT", nullable: false),
                    Adresa = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    UserType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Slika = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potrosaci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Cena = table.Column<decimal>(type: "TEXT", precision: 8, scale: 2, nullable: false),
                    Sastojci = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Adresa = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Komentar = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Cena = table.Column<decimal>(type: "TEXT", precision: 8, scale: 2, nullable: false),
                    TrajanjeDostave = table.Column<long>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "CekaDostavu"),
                    PotrosacId = table.Column<int>(type: "INTEGER", nullable: false),
                    DostavljacId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Porudzbine_Dostavljaci_DostavljacId",
                        column: x => x.DostavljacId,
                        principalTable: "Dostavljaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Porudzbine_Potrosaci_PotrosacId",
                        column: x => x.PotrosacId,
                        principalTable: "Potrosaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PorudzbinaProizvod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PorudzbinaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProizvodId = table.Column<int>(type: "INTEGER", nullable: false),
                    Kolicina = table.Column<uint>(type: "INTEGER", nullable: false)
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
                name: "IX_Admini_Username",
                table: "Admini",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dostavljaci_Username",
                table: "Dostavljaci",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PorudzbinaProizvod_ProizvodId",
                table: "PorudzbinaProizvod",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_DostavljacId",
                table: "Porudzbine",
                column: "DostavljacId");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_PotrosacId",
                table: "Porudzbine",
                column: "PotrosacId");

            migrationBuilder.CreateIndex(
                name: "IX_Potrosaci_Username",
                table: "Potrosaci",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admini");

            migrationBuilder.DropTable(
                name: "PorudzbinaProizvod");

            migrationBuilder.DropTable(
                name: "Porudzbine");

            migrationBuilder.DropTable(
                name: "Proizvodi");

            migrationBuilder.DropTable(
                name: "Dostavljaci");

            migrationBuilder.DropTable(
                name: "Potrosaci");
        }
    }
}
