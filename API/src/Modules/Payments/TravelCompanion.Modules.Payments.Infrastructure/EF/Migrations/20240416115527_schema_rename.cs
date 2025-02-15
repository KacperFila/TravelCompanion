using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class schema_rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "payments");

            migrationBuilder.RenameTable(
                name: "TravelSummaries",
                schema: "Payments",
                newName: "TravelSummaries",
                newSchema: "payments");

            migrationBuilder.RenameTable(
                name: "ParticipantsCosts",
                schema: "Payments",
                newName: "ParticipantsCosts",
                newSchema: "payments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Payments");

            migrationBuilder.RenameTable(
                name: "TravelSummaries",
                schema: "payments",
                newName: "TravelSummaries",
                newSchema: "Payments");

            migrationBuilder.RenameTable(
                name: "ParticipantsCosts",
                schema: "payments",
                newName: "ParticipantsCosts",
                newSchema: "Payments");
        }
    }
}
