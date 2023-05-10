using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logic.Migrations
{
    /// <inheritdoc />
    public partial class EditSubjectMaterialToSubjectFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Material",
                table: "SubjectMaterials",
                newName: "FileName");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "SubjectMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "SubjectMaterials");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "SubjectMaterials",
                newName: "Material");
        }
    }
}
