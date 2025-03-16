using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class PlanParticipantRecordPlanIdRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanParticipantRecords_Plans_TravelPlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords");

            migrationBuilder.RenameColumn(
                name: "TravelPlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords",
                newName: "PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanParticipantRecords_TravelPlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords",
                newName: "IX_PlanParticipantRecords_PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanParticipantRecords_Plans_PlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords",
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
                name: "FK_PlanParticipantRecords_Plans_PlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords",
                newName: "TravelPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanParticipantRecords_PlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords",
                newName: "IX_PlanParticipantRecords_TravelPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanParticipantRecords_Plans_TravelPlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords",
                column: "TravelPlanId",
                principalSchema: "travelPlans",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
