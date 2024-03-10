using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Travels.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TravelPoints_travelId_pointId_key_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoints_TravelId",
                schema: "travels",
                table: "Receipts");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_TravelPointId",
                schema: "travels",
                table: "Receipts",
                column: "TravelPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPoints_TravelPointId",
                schema: "travels",
                table: "Receipts",
                column: "TravelPointId",
                principalSchema: "travels",
                principalTable: "TravelPoints",
                principalColumn: "TravelPointId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoints_TravelPointId",
                schema: "travels",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_TravelPointId",
                schema: "travels",
                table: "Receipts");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPoints_TravelId",
                schema: "travels",
                table: "Receipts",
                column: "TravelId",
                principalSchema: "travels",
                principalTable: "TravelPoints",
                principalColumn: "TravelPointId");
        }
    }
}
