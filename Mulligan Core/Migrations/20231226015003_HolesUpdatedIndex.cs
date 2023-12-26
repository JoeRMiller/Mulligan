using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mulligan.Core.Migrations
{
    /// <inheritdoc />
    public partial class HolesUpdatedIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hole_Id_TeeSetId",
                table: "Hole");

            migrationBuilder.CreateIndex(
                name: "IX_Hole_Id_TeeSetId_Number",
                table: "Hole",
                columns: new[] { "Id", "TeeSetId", "Number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hole_Id_TeeSetId_Number",
                table: "Hole");

            migrationBuilder.CreateIndex(
                name: "IX_Hole_Id_TeeSetId",
                table: "Hole",
                columns: new[] { "Id", "TeeSetId" },
                unique: true);
        }
    }
}
