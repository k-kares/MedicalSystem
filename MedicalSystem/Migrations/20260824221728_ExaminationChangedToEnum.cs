using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalSystem.Migrations
{
    /// <inheritdoc />
    public partial class ExaminationChangedToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Examinations");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Examinations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Examinations",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
