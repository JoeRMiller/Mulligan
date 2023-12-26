using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mulligan.Core.Migrations
{
    /// <inheritdoc />
    public partial class Holes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "HoleSequence");

            migrationBuilder.CreateTable(
                name: "Hole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"HoleSequence\"')"),
                    TeeSetId = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Hdcp = table.Column<int>(type: "integer", nullable: false),
                    Par = table.Column<int>(type: "integer", nullable: false),
                    Yardarge = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hole_Tees_TeeSetId",
                        column: x => x.TeeSetId,
                        principalTable: "Tees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hole_Id_TeeSetId",
                table: "Hole",
                columns: new[] { "Id", "TeeSetId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hole_TeeSetId",
                table: "Hole",
                column: "TeeSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hole");

            migrationBuilder.DropSequence(
                name: "HoleSequence");
        }
    }
}
