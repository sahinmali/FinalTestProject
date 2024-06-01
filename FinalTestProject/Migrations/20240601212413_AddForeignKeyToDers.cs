using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalTestProject.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToDers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OgretimElemaniTc",
                table: "Ders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ders_OgretimElemaniTc",
                table: "Ders",
                column: "OgretimElemaniTc");

            migrationBuilder.AddForeignKey(
                name: "FK_Ders_OgretimElemani_OgretimElemaniTc",
                table: "Ders",
                column: "OgretimElemaniTc",
                principalTable: "OgretimElemani",
                principalColumn: "TCKimlikNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenci_Danisman_OgrenciDanismani",
                table: "Ogrenci",
                column: "OgrenciDanismani",
                principalTable: "Danisman",
                principalColumn: "TCKimlikNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ders_OgretimElemani_OgretimElemaniTc",
                table: "Ders");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenci_Danisman_OgrenciDanismani",
                table: "Ogrenci");

            migrationBuilder.DropIndex(
                name: "IX_Ders_OgretimElemaniTc",
                table: "Ders");

            migrationBuilder.DropColumn(
                name: "OgretimElemaniTc",
                table: "Ders");
        }
    }
}
