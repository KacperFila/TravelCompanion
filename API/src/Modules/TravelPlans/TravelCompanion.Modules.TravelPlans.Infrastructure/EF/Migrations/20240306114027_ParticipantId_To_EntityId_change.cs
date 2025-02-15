using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantId_To_EntityId_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParticipantIds",
                schema: "travelPlans",
                table: "Plans",
                newName: "Participants");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                schema: "travelPlans",
                table: "Invitations",
                newName: "InviteeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Participants",
                schema: "travelPlans",
                table: "Plans",
                newName: "ParticipantIds");

            migrationBuilder.RenameColumn(
                name: "InviteeId",
                schema: "travelPlans",
                table: "Invitations",
                newName: "ParticipantId");
        }
    }
}
