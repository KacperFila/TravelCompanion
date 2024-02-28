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
                table: "Invitation");

            migrationBuilder.RenameTable(
                name: "Invitation",
                schema: "travelPlans",
                newName: "Invitations",
                newSchema: "travelPlans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelPlanInvitations",
                schema: "travelPlans",
                table: "Invitations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelPlanInvitations",
                schema: "travelPlans",
                table: "Invitations");

            migrationBuilder.RenameTable(
                name: "Invitations",
                schema: "travelPlans",
                newName: "Invitation",
                newSchema: "travelPlans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelPlanInvitation",
                schema: "travelPlans",
                table: "Invitation",
                column: "Id");
        }
    }
}
