using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPointCost_refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPointCosts_TravelPointCostId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.DropTable(
                name: "TravelPointCosts",
                schema: "travelPlans");

            migrationBuilder.RenameColumn(
                name: "TravelPointCostId",
                schema: "travelPlans",
                table: "Receipts",
                newName: "TravelPointId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_TravelPointCostId",
                schema: "travelPlans",
                table: "Receipts",
                newName: "IX_Receipts_TravelPointId");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                schema: "travelPlans",
                table: "TravelPoints",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoints_TravelPointId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.RenameColumn(
                name: "TravelPointId",
                schema: "travelPlans",
                table: "Receipts",
                newName: "TravelPointCostId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_TravelPointId",
                schema: "travelPlans",
                table: "Receipts",
                newName: "IX_Receipts_TravelPointCostId");

            migrationBuilder.CreateTable(
                name: "TravelPointCosts",
                schema: "travelPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TravelPointId = table.Column<Guid>(type: "uuid", nullable: false),
                    OverallCost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPointCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelPointCosts_TravelPoints_TravelPointId",
                        column: x => x.TravelPointId,
                        principalSchema: "travelPlans",
                        principalTable: "TravelPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelPointCosts_TravelPointId",
                schema: "travelPlans",
                table: "TravelPointCosts",
                column: "TravelPointId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPointCosts_TravelPointCostId",
                schema: "travelPlans",
                table: "Receipts",
                column: "TravelPointCostId",
                principalSchema: "travelPlans",
                principalTable: "TravelPointCosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
