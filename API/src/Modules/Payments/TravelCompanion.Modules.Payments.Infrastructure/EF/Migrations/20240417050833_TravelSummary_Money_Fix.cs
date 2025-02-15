using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelSummary_Money_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TravelAdditionalCost",
                schema: "payments",
                table: "TravelSummaries",
                newName: "TravelAdditionalCostValue");

            migrationBuilder.RenameColumn(
                name: "TotalCost",
                schema: "payments",
                table: "TravelSummaries",
                newName: "TotalCostValue");

            migrationBuilder.RenameColumn(
                name: "PointsAdditionalCost",
                schema: "payments",
                table: "TravelSummaries",
                newName: "PointsAdditionalCostValue");

            migrationBuilder.AddColumn<string>(
                name: "PointsAdditionalCostCurrency",
                schema: "payments",
                table: "TravelSummaries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TotalCostCurrency",
                schema: "payments",
                table: "TravelSummaries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TravelAdditionalCostCurrency",
                schema: "payments",
                table: "TravelSummaries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsAdditionalCostCurrency",
                schema: "payments",
                table: "TravelSummaries");

            migrationBuilder.DropColumn(
                name: "TotalCostCurrency",
                schema: "payments",
                table: "TravelSummaries");

            migrationBuilder.DropColumn(
                name: "TravelAdditionalCostCurrency",
                schema: "payments",
                table: "TravelSummaries");

            migrationBuilder.RenameColumn(
                name: "TravelAdditionalCostValue",
                schema: "payments",
                table: "TravelSummaries",
                newName: "TravelAdditionalCost");

            migrationBuilder.RenameColumn(
                name: "TotalCostValue",
                schema: "payments",
                table: "TravelSummaries",
                newName: "TotalCost");

            migrationBuilder.RenameColumn(
                name: "PointsAdditionalCostValue",
                schema: "payments",
                table: "TravelSummaries",
                newName: "PointsAdditionalCost");
        }
    }
}
