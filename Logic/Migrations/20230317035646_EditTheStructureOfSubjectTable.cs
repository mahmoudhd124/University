using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logic.Migrations
{
    /// <inheritdoc />
    public partial class EditTheStructureOfSubjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_Name",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Subjects",
                type: "int",
                maxLength: 3,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Subjects",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Code",
                table: "Subjects",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Department",
                table: "Subjects",
                column: "Department");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_Code",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_Department",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Subjects");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Subjects",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Name",
                table: "Subjects",
                column: "Name",
                unique: true);
        }
    }
}
