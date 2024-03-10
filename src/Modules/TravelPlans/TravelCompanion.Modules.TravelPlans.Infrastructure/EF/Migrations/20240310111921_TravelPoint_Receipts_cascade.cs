using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPoint_Receipts_cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoints_PointId",
                schema: "travelPlans",
                table: "Receipts");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPoints_PointId",
                schema: "travelPlans",
                table: "Receipts",
                column: "PointId",
                principalSchema: "travelPlans",
                principalTable: "TravelPoints",
                principalColumn: "Id");
        }
    }
}
