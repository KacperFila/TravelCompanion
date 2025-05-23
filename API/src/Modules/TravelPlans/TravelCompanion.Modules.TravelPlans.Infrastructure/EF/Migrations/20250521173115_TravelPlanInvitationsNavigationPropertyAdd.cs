using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPlanInvitationsNavigationPropertyAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                schema: "travelPlans",
                table: "Invitations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_PlanId",
                schema: "travelPlans",
                table: "Invitations",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_TravelPlanId",
                schema: "travelPlans",
                table: "Invitations",
                column: "TravelPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Plans_PlanId",
                schema: "travelPlans",
                table: "Invitations",
                column: "PlanId",
                principalSchema: "travelPlans",
                principalTable: "Plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Plans_TravelPlanId",
                schema: "travelPlans",
                table: "Invitations",
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
                name: "FK_Invitations_Plans_PlanId",
                schema: "travelPlans",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Plans_TravelPlanId",
                schema: "travelPlans",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_PlanId",
                schema: "travelPlans",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_TravelPlanId",
                schema: "travelPlans",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "PlanId",
                schema: "travelPlans",
                table: "Invitations");
        }
    }
}
