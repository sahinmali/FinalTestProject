using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalTestProject.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToOgrenci : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "OgrenciDanismani",
                table: "Ogrenci",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenci_OgrenciDanismani",
                table: "Ogrenci",
                column: "OgrenciDanismani");

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
                name: "FK_Ogrenci_Danisman_OgrenciDanismani",
                table: "Ogrenci");

            migrationBuilder.DropIndex(
                name: "IX_Ogrenci_OgrenciDanismani",
                table: "Ogrenci");

            migrationBuilder.AlterColumn<string>(
                name: "OgrenciDanismani",
                table: "Ogrenci",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
