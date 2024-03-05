using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Plan_AdditionalCost_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                schema: "travelPlans",
                table: "Receipts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "AdditionalCostsValue",
                schema: "travelPlans",
                table: "Plans",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_PlanId",
                schema: "travelPlans",
                table: "Receipts",
                column: "PlanId");

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

            migrationBuilder.DropIndex(
                name: "IX_Receipts_PlanId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "PlanId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "AdditionalCostsValue",
                schema: "travelPlans",
                table: "Plans");
        }
    }
}
