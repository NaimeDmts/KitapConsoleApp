using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvKitapci.Migrations
{
    public partial class intialCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblKategori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAd = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    KategoriTanitim = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblKategori", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblKullanici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblKullanici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblYazar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YazarAd = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    YazarSoyad = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblYazar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kitaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KitapAdi = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    KitapFiyati = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    KitapStok = table.Column<int>(type: "int", nullable: false),
                    KitapIndirimi = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    KitapStokDurum = table.Column<bool>(type: "bit", nullable: false),
                    KitapYayınTarihi = table.Column<DateTime>(type: "date", nullable: true),
                    YazarId = table.Column<int>(type: "int", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kitaps_TblKategori_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "TblKategori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kitaps_TblYazar_YazarId",
                        column: x => x.YazarId,
                        principalTable: "TblYazar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kitaps_KategoriId",
                table: "Kitaps",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Kitaps_YazarId",
                table: "Kitaps",
                column: "YazarId");

            migrationBuilder.CreateIndex(
                name: "IX_TblKullanici_Email",
                table: "TblKullanici",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kitaps");

            migrationBuilder.DropTable(
                name: "TblKullanici");

            migrationBuilder.DropTable(
                name: "TblKategori");

            migrationBuilder.DropTable(
                name: "TblYazar");
        }
    }
}
