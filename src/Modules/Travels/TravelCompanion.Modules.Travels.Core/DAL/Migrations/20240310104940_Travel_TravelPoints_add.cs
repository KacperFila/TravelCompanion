using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Travels.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Travel_TravelPoints_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelPoint",
                schema: "travels",
                columns: table => new
                {
                    TravelPointId = table.Column<Guid>(type: "uuid", nullable: false),
                    TravelId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlaceName = table.Column<string>(type: "text", nullable: false),
                    TotalCost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPoint", x => x.TravelPointId);
                    table.ForeignKey(
                        name: "FK_TravelPoint_Travels_TravelId",
                        column: x => x.TravelId,
                        principalSchema: "travels",
                        principalTable: "Travels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelPoint_TravelId",
                schema: "travels",
                table: "TravelPoint",
                column: "TravelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPoint_TravelId",
                schema: "travels",
                table: "Receipts",
                column: "TravelId",
                principalSchema: "travels",
                principalTable: "TravelPoint",
                principalColumn: "TravelPointId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoint_TravelId",
                schema: "travels",
                table: "Receipts");

            migrationBuilder.DropTable(
                name: "TravelPoint",
                schema: "travels");
        }
    }
}
