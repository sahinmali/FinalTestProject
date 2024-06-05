using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalTestProject.Migrations
{
    /// <inheritdoc />
    public partial class AddSecilenDers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OgrenciKimlikNo",
                table: "DersSecimi",
                newName: "OgrenciKimlikNo");

            migrationBuilder.RenameColumn(
                name: "DanismanKimlikNo",
                table: "DersSecimi",
                newName: "DanismanKimlikNo");

            migrationBuilder.AlterColumn<long>(
                name: "OgrenciKimlikNo",
                table: "DersSecimi",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DersSecimi",
                table: "DersSecimi",
                column: "OgrenciKimlikNo");

            migrationBuilder.CreateTable(
                name: "SecilenDers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TcKimlikNo = table.Column<long>(type: "INTEGER", nullable: false),
                    DersKodu = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecilenDers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecilenDers_DersSecimi_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "DersSecimi",
                        principalColumn: "OgrenciKimlikNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecilenDers_Ders_DersKodu",
                        column: x => x.DersKodu,
                        principalTable: "Ders",
                        principalColumn: "DersKodu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecilenDers_DersKodu",
                table: "SecilenDers",
                column: "DersKodu");

            migrationBuilder.CreateIndex(
                name: "IX_SecilenDers_TcKimlikNo",
                table: "SecilenDers",
                column: "TcKimlikNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecilenDers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DersSecimi",
                table: "DersSecimi");

            migrationBuilder.RenameColumn(
                name: "DanismanKimlikNo",
                table: "DersSecimi",
                newName: "DanismanKimlikNo");

            migrationBuilder.RenameColumn(
                name: "OgrenciKimlikNo",
                table: "DersSecimi",
                newName: "OgrenciKimlikNo");

            migrationBuilder.AlterColumn<long>(
                name: "OgrenciKimlikNo",
                table: "DersSecimi",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
