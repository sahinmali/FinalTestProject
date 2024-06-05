using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalTestProject.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultValuesToExams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "FinalSinavYuzde",
                table: "DersNotu",
                type: "REAL",
                nullable: true,
                defaultValue: 0.6f,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AraSinavYuzde",
                table: "DersNotu",
                type: "REAL",
                nullable: true,
                defaultValue: 0.4f,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "FinalSinavYuzde",
                table: "DersNotu",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldNullable: true,
                oldDefaultValue: 0.6f);

            migrationBuilder.AlterColumn<float>(
                name: "AraSinavYuzde",
                table: "DersNotu",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldNullable: true,
                oldDefaultValue: 0.4f);
        }
    }
}
