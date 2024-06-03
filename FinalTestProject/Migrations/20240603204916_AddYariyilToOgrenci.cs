using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalTestProject.Migrations
{
    /// <inheritdoc />
    public partial class AddYariyilToOgrenci : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Yariyil",
                table: "Ogrenci",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateTable(
            //    name: "DersSecimi",
            //    columns: table => new
            //    {
            //        SecilenDersler = table.Column<string>(type: "TEXT", nullable: true),
            //        KimlikNo = table.Column<long>(type: "INTEGER", nullable: false),
            //        DanismanNo = table.Column<string>(type: "TEXT", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DersSecimi");

            migrationBuilder.DropColumn(
                name: "Yariyil",
                table: "Ogrenci");
        }
    }
}
