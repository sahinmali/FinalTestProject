using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalTestProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Danisman",
                columns: table => new
                {
                    TCKimlikNo = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TanimliOgrenciler = table.Column<string>(type: "TEXT", nullable: true),
                    Ad = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    UyelikType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Danisman", x => x.TCKimlikNo);
                });

            migrationBuilder.CreateTable(
                name: "Ders",
                columns: table => new
                {
                    DersKodu = table.Column<string>(type: "TEXT", nullable: false),
                    DersAdi = table.Column<string>(type: "TEXT", nullable: true),
                    SaatCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Yariyil = table.Column<int>(type: "INTEGER", nullable: false),
                    AKTS = table.Column<float>(type: "REAL", nullable: false),
                    Kredi = table.Column<float>(type: "REAL", nullable: false),
                    OgrenciList = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ders", x => x.DersKodu);
                });

            migrationBuilder.CreateTable(
                name: "Idareci",
                columns: table => new
                {
                    TCKimlikNo = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OgretimElemaniList = table.Column<string>(type: "TEXT", nullable: true),
                    DersListe = table.Column<string>(type: "TEXT", nullable: true),
                    AcilanDersler = table.Column<string>(type: "TEXT", nullable: true),
                    Ad = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    UyelikType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idareci", x => x.TCKimlikNo);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenci",
                columns: table => new
                {
                    TCKimlikNo = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SecilmisDersler = table.Column<string>(type: "TEXT", nullable: true),
                    OgrenciDanismani = table.Column<string>(type: "TEXT", nullable: true),
                    Sinif = table.Column<int>(type: "INTEGER", nullable: true),
                    Donem = table.Column<int>(type: "INTEGER", nullable: true),
                    Ad = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    UyelikType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenci", x => x.TCKimlikNo);
                });

            migrationBuilder.CreateTable(
                name: "OgretimElemani",
                columns: table => new
                {
                    TCKimlikNo = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SecilmisDersler = table.Column<string>(type: "TEXT", nullable: true),
                    Ad = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    UyelikType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgretimElemani", x => x.TCKimlikNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Danisman");

            migrationBuilder.DropTable(
                name: "Ders");

            migrationBuilder.DropTable(
                name: "Idareci");

            migrationBuilder.DropTable(
                name: "Ogrenci");

            migrationBuilder.DropTable(
                name: "OgretimElemani");
        }
    }
}
