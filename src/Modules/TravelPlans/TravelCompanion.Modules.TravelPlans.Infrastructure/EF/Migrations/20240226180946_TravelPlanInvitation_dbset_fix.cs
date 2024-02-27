using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPlanInvitation_dbset_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelPlanInvitation",
                schema: "travelPlans",
                table: "TravelPlanInvitation");

            migrationBuilder.RenameTable(
                name: "TravelPlanInvitation",
                schema: "travelPlans",
                newName: "TravelPlanInvitations",
                newSchema: "travelPlans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelPlanInvitations",
                schema: "travelPlans",
                table: "TravelPlanInvitations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelPlanInvitations",
                schema: "travelPlans",
                table: "TravelPlanInvitations");

            migrationBuilder.RenameTable(
                name: "TravelPlanInvitations",
                schema: "travelPlans",
                newName: "TravelPlanInvitation",
                newSchema: "travelPlans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelPlanInvitation",
                schema: "travelPlans",
                table: "TravelPlanInvitation",
                column: "Id");
        }
    }
}
