using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalTestProject.Migrations
{
    /// <inheritdoc />
    public partial class AddDersNotuColums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AraSinavNot",
                table: "DersNotu",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AraSinavYuzde",
                table: "DersNotu",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FinalSinavNot",
                table: "DersNotu",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FinalSinavYuzde",
                table: "DersNotu",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HarfNotu",
                table: "DersNotu",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "SonucNotu",
                table: "DersNotu",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "YoklamaDurumu",
                table: "DersNotu",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AraSinavNot",
                table: "DersNotu");

            migrationBuilder.DropColumn(
                name: "AraSinavYuzde",
                table: "DersNotu");

            migrationBuilder.DropColumn(
                name: "FinalSinavNot",
                table: "DersNotu");

            migrationBuilder.DropColumn(
                name: "FinalSinavYuzde",
                table: "DersNotu");

            migrationBuilder.DropColumn(
                name: "HarfNotu",
                table: "DersNotu");

            migrationBuilder.DropColumn(
                name: "SonucNotu",
                table: "DersNotu");

            migrationBuilder.DropColumn(
                name: "YoklamaDurumu",
                table: "DersNotu");
        }
    }
}
