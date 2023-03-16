using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logic.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalNumber",
                table: "AspNetUsers",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NationalNumber",
                table: "AspNetUsers",
                column: "NationalNumber",
                unique: true,
                filter: "[NationalNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NationalNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NationalNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AspNetUsers");
        }
    }
}
