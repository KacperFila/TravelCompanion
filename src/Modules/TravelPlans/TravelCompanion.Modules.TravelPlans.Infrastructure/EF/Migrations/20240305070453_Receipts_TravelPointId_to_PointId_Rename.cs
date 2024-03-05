using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Receipts_TravelPointId_to_PointId_Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoints_TravelPointId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.RenameColumn(
                name: "TravelPointId",
                schema: "travelPlans",
                table: "Receipts",
                newName: "PointId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_TravelPointId",
                schema: "travelPlans",
                table: "Receipts",
                newName: "IX_Receipts_PointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPoints_PointId",
                schema: "travelPlans",
                table: "Receipts",
                column: "PointId",
                principalSchema: "travelPlans",
                principalTable: "TravelPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoints_PointId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.RenameColumn(
                name: "PointId",
                schema: "travelPlans",
                table: "Receipts",
                newName: "TravelPointId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_PointId",
                schema: "travelPlans",
                table: "Receipts",
                newName: "IX_Receipts_TravelPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPoints_TravelPointId",
                schema: "travelPlans",
                table: "Receipts",
                column: "TravelPointId",
                principalSchema: "travelPlans",
                principalTable: "TravelPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
