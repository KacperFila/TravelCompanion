using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPlan_name_refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPoints_TravelPlans_TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.DropTable(
                name: "TravelPlans",
                schema: "travelPlans");

            migrationBuilder.DropIndex(
                name: "IX_TravelPoints_TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Plans",
                schema: "travelPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantIds = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    From = table.Column<DateOnly>(type: "date", nullable: false),
                    To = table.Column<DateOnly>(type: "date", nullable: false),
                    ParticipantPaidIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    AllParticipantsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelPoints_PlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPoints_Plans_PlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                column: "PlanId",
                principalSchema: "travelPlans",
                principalTable: "Plans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPoints_Plans_PlanId",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.DropTable(
                name: "Plans",
                schema: "travelPlans");

            migrationBuilder.DropIndex(
                name: "IX_TravelPoints_PlanId",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.DropColumn(
                name: "PlanId",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.CreateTable(
                name: "TravelPlans",
                schema: "travelPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AllParticipantsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    From = table.Column<DateOnly>(type: "date", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantIds = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    ParticipantPaidIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    To = table.Column<DateOnly>(type: "date", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPlans", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelPoints_TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                column: "TravelPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPoints_TravelPlans_TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                column: "TravelPlanId",
                principalSchema: "travelPlans",
                principalTable: "TravelPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
