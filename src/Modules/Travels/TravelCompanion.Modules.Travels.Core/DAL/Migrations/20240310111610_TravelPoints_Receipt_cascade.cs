using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Travels.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TravelPoints_Receipt_cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoint_TravelId",
                schema: "travels",
                table: "Receipts");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPoint_TravelId",
                schema: "travels",
                table: "Receipts",
                column: "TravelId",
                principalSchema: "travels",
                principalTable: "TravelPoint",
                principalColumn: "TravelPointId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoint_TravelId",
                schema: "travels",
                table: "Receipts");

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
    }
}
