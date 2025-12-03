using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Travels.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Auditable_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travels",
                table: "Travels",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travels",
                table: "Travels",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travels",
                table: "TravelRating",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travels",
                table: "TravelRating",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travels",
                table: "TravelPoints",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travels",
                table: "TravelPoints",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travels",
                table: "Receipts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travels",
                table: "Receipts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "travels",
                table: "Postcards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "travels",
                table: "Postcards",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travels",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travels",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travels",
                table: "TravelRating");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travels",
                table: "TravelRating");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travels",
                table: "TravelPoints");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travels",
                table: "TravelPoints");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travels",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travels",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "travels",
                table: "Postcards");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "travels",
                table: "Postcards");
        }
    }
}
