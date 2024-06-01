using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalTestProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DersNotu",
                columns: table => new
                {
                    OgrenciTCKimlikNo = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_DersNotu_Ogrenci_OgrenciTCKimlikNo",
                        column: x => x.OgrenciTCKimlikNo,
                        principalTable: "Ogrenci",
                        principalColumn: "TCKimlikNo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DersNotu_OgrenciTCKimlikNo",
                table: "DersNotu",
                column: "OgrenciTCKimlikNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DersNotu");
        }
    }
}
