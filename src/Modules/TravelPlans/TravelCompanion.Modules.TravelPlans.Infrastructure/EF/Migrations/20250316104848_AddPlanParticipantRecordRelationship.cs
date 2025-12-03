using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanParticipantRecordRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanParticipantRecords_Plans_PlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords");

            migrationBuilder.DropIndex(
                name: "IX_PlanParticipantRecords_PlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords");

            migrationBuilder.DropColumn(
                name: "PlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords");

            migrationBuilder.CreateIndex(
                name: "IX_PlanParticipantRecords_TravelPlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords",
                column: "TravelPlanId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanParticipantRecords_Plans_TravelPlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords");

            migrationBuilder.DropIndex(
                name: "IX_PlanParticipantRecords_TravelPlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanParticipantRecords_PlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanParticipantRecords_Plans_PlanId",
                schema: "travelPlans",
                table: "PlanParticipantRecords",
                column: "PlanId",
                principalSchema: "travelPlans",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
