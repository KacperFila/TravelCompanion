using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Receipt_Cascade_Delete_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Plans_PlanId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Plans_PlanId",
                schema: "travelPlans",
                table: "Receipts",
                column: "PlanId",
                principalSchema: "travelPlans",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Plans_PlanId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Plans_PlanId",
                schema: "travelPlans",
                table: "Receipts",
                column: "PlanId",
                principalSchema: "travelPlans",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
