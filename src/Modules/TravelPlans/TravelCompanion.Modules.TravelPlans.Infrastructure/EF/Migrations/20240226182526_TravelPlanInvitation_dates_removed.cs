using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPlanInvitation_dates_removed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TravelFrom",
                schema: "travelPlans",
                table: "TravelPlanInvitations");

            migrationBuilder.DropColumn(
                name: "TravelTo",
                schema: "travelPlans",
                table: "TravelPlanInvitations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "TravelFrom",
                schema: "travelPlans",
                table: "TravelPlanInvitations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "TravelTo",
                schema: "travelPlans",
                table: "TravelPlanInvitations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
