using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hamburgercim.Data.Migrations
{
    public partial class ilk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boyutlar",
                columns: table => new
                {
                    BoyutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoyutAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoyutFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boyutlar", x => x.BoyutId);
                });

            migrationBuilder.CreateTable(
                name: "Hamburgerler",
                columns: table => new
                {
                    HamburgerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HamburgerAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HamburgerFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HamburgerFoto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hamburgerler", x => x.HamburgerId);
                });

            migrationBuilder.CreateTable(
                name: "Icecekler",
                columns: table => new
                {
                    IcecekId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IcecekAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IcecekFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icecekler", x => x.IcecekId);
                });

            migrationBuilder.CreateTable(
                name: "Siparisler",
                columns: table => new
                {
                    SiparisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisAdet = table.Column<byte>(type: "tinyint", nullable: false),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IcecekId = table.Column<int>(type: "int", nullable: true),
                    HamburgerId = table.Column<int>(type: "int", nullable: false),
                    BoyutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparisler", x => x.SiparisId);
                    table.ForeignKey(
                        name: "FK_Siparisler_Boyutlar_BoyutId",
                        column: x => x.BoyutId,
                        principalTable: "Boyutlar",
                        principalColumn: "BoyutId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Siparisler_Hamburgerler_HamburgerId",
                        column: x => x.HamburgerId,
                        principalTable: "Hamburgerler",
                        principalColumn: "HamburgerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Siparisler_Icecekler_IcecekId",
                        column: x => x.IcecekId,
                        principalTable: "Icecekler",
                        principalColumn: "IcecekId");
                });

            migrationBuilder.CreateTable(
                name: "EkstraMalzemeler",
                columns: table => new
                {
                    EkstraMalzemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EkstraMalzemeAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EkstraMalzemeFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SiparisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkstraMalzemeler", x => x.EkstraMalzemeId);
                    table.ForeignKey(
                        name: "FK_EkstraMalzemeler_Siparisler_SiparisId",
                        column: x => x.SiparisId,
                        principalTable: "Siparisler",
                        principalColumn: "SiparisId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EkstraMalzemeler_SiparisId",
                table: "EkstraMalzemeler",
                column: "SiparisId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_BoyutId",
                table: "Siparisler",
                column: "BoyutId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_HamburgerId",
                table: "Siparisler",
                column: "HamburgerId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_IcecekId",
                table: "Siparisler",
                column: "IcecekId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EkstraMalzemeler");

            migrationBuilder.DropTable(
                name: "Siparisler");

            migrationBuilder.DropTable(
                name: "Boyutlar");

            migrationBuilder.DropTable(
                name: "Hamburgerler");

            migrationBuilder.DropTable(
                name: "Icecekler");
        }
    }
}
