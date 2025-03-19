using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Auditable_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "TravelPointUpdateRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "TravelPointUpdateRequests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "TravelPoints",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "TravelPoints",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "TravelPointRemoveRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "TravelPointRemoveRequests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "Receipts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "Receipts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "Plans",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "Plans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "PlanAcceptRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "PlanAcceptRequests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "Invitations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "Invitations",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "TravelPointUpdateRequests");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "TravelPointUpdateRequests");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "TravelPointRemoveRequests");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "TravelPointRemoveRequests");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "PlanAcceptRequests");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "PlanAcceptRequests");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travelPlans",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travelPlans",
                table: "Invitations");
        }
    }
}
