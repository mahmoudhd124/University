using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logic.Migrations
{
    /// <inheritdoc />
    public partial class EditConfigurationOfDotctorSubjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSubject_AspNetUsers_DoctorId",
                table: "DoctorSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSubject_Subjects_SubjectId",
                table: "DoctorSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSubject",
                table: "DoctorSubject");

            migrationBuilder.RenameTable(
                name: "DoctorSubject",
                newName: "DoctorSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSubject_SubjectId",
                table: "DoctorSubjects",
                newName: "IX_DoctorSubjects_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSubject_DoctorId",
                table: "DoctorSubjects",
                newName: "IX_DoctorSubjects_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSubjects",
                table: "DoctorSubjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSubjects_AspNetUsers_DoctorId",
                table: "DoctorSubjects",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSubjects_Subjects_SubjectId",
                table: "DoctorSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSubjects_AspNetUsers_DoctorId",
                table: "DoctorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSubjects_Subjects_SubjectId",
                table: "DoctorSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSubjects",
                table: "DoctorSubjects");

            migrationBuilder.RenameTable(
                name: "DoctorSubjects",
                newName: "DoctorSubject");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSubjects_SubjectId",
                table: "DoctorSubject",
                newName: "IX_DoctorSubject_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSubjects_DoctorId",
                table: "DoctorSubject",
                newName: "IX_DoctorSubject_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSubject",
                table: "DoctorSubject",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSubject_AspNetUsers_DoctorId",
                table: "DoctorSubject",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSubject_Subjects_SubjectId",
                table: "DoctorSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
