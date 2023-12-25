using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mulligan.Core.Migrations
{
    /// <inheritdoc />
    public partial class TeeSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "TeeSetSequence");

            migrationBuilder.CreateTable(
                name: "Tees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"TeeSetSequence\"')"),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Par = table.Column<int>(type: "integer", nullable: false),
                    CourseRating = table.Column<decimal>(type: "numeric", nullable: false),
                    BogeyRating = table.Column<decimal>(type: "numeric", nullable: false),
                    Slope = table.Column<int>(type: "integer", nullable: false),
                    FrontRating = table.Column<decimal>(type: "numeric", nullable: false),
                    FrontSlope = table.Column<int>(type: "integer", nullable: false),
                    BackRating = table.Column<decimal>(type: "numeric", nullable: false),
                    BackSlope = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tees_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "NCRDId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tees_CourseId",
                table: "Tees",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tees");

            migrationBuilder.DropSequence(
                name: "TeeSetSequence");
        }
    }
}
