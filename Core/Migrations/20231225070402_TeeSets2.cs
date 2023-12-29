using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mulligan.Core.Migrations
{
    /// <inheritdoc />
    public partial class TeeSets2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tees_CourseId",
                table: "Tees");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tees_CourseId_Name_Gender",
                table: "Tees",
                columns: new[] { "CourseId", "Name", "Gender" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tees_CourseId_Name_Gender",
                table: "Tees");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tees");

            migrationBuilder.CreateIndex(
                name: "IX_Tees_CourseId",
                table: "Tees",
                column: "CourseId");
        }
    }
}
