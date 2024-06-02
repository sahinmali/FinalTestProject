using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalTestProject.Migrations
{
    /// <inheritdoc />
    public partial class AddKeyAndForeignKeyToDersNotu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersNotu_Ogrenci_OgrenciTCKimlikNo",
                table: "DersNotu");

            migrationBuilder.RenameColumn(
                name: "OgrenciTCKimlikNo",
                table: "DersNotu",
                newName: "OgrenciTc");

            migrationBuilder.RenameIndex(
                name: "IX_DersNotu_OgrenciTCKimlikNo",
                table: "DersNotu",
                newName: "IX_DersNotu_OgrenciTc");

            //migrationBuilder.AddColumn<int>(
            //    name: "Id",
            //    table: "DersNotu",
            //    type: "INTEGER",
            //    nullable: false,
            //    defaultValue: 0)
            //    .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "DersKodu",
                table: "DersNotu",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DersNotu",
                table: "DersNotu",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DersNotu_DersKodu",
                table: "DersNotu",
                column: "DersKodu");

            migrationBuilder.AddForeignKey(
                name: "FK_DersNotu_Ders_DersKodu",
                table: "DersNotu",
                column: "DersKodu",
                principalTable: "Ders",
                principalColumn: "DersKodu",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DersNotu_Ogrenci_OgrenciTc",
                table: "DersNotu",
                column: "OgrenciTc",
                principalTable: "Ogrenci",
                principalColumn: "TCKimlikNo",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DersNotu_Ders_DersKodu",
                table: "DersNotu");

            migrationBuilder.DropForeignKey(
                name: "FK_DersNotu_Ogrenci_OgrenciTc",
                table: "DersNotu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DersNotu",
                table: "DersNotu");

            migrationBuilder.DropIndex(
                name: "IX_DersNotu_DersKodu",
                table: "DersNotu");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DersNotu");

            migrationBuilder.DropColumn(
                name: "DersKodu",
                table: "DersNotu");

            migrationBuilder.RenameColumn(
                name: "OgrenciTc",
                table: "DersNotu",
                newName: "OgrenciTCKimlikNo");

            migrationBuilder.RenameIndex(
                name: "IX_DersNotu_OgrenciTc",
                table: "DersNotu",
                newName: "IX_DersNotu_OgrenciTCKimlikNo");

            migrationBuilder.AddForeignKey(
                name: "FK_DersNotu_Ogrenci_OgrenciTCKimlikNo",
                table: "DersNotu",
                column: "OgrenciTCKimlikNo",
                principalTable: "Ogrenci",
                principalColumn: "TCKimlikNo");
        }
    }
}
