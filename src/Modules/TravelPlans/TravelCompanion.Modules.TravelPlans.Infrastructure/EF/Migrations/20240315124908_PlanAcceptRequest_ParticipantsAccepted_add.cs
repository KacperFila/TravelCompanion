using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class PlanAcceptRequest_ParticipantsAccepted_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantsAccepted",
                schema: "travelPlans",
                table: "Plans");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "ParticipantsAccepted",
                schema: "travelPlans",
                table: "PlanAcceptRequests",
                type: "uuid[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantsAccepted",
                schema: "travelPlans",
                table: "PlanAcceptRequests");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "ParticipantsAccepted",
                schema: "travelPlans",
                table: "Plans",
                type: "uuid[]",
                nullable: true);
        }
    }
}
