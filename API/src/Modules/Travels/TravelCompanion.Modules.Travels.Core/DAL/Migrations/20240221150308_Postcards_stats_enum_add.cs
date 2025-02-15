using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Travels.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Postcards_stats_enum_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                schema: "travels",
                table: "Postcards");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "travels",
                table: "Postcards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "travels",
                table: "Postcards");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                schema: "travels",
                table: "Postcards",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
