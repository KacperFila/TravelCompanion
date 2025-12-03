using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPointChangeSuggestion_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelPointChangeSuggestions",
                schema: "travelPlans",
                columns: table => new
                {
                    SuggestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TravelPlanPointId = table.Column<Guid>(type: "uuid", nullable: false),
                    SuggestedById = table.Column<Guid>(type: "uuid", nullable: false),
                    PlaceName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPointChangeSuggestions", x => x.SuggestionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelPointChangeSuggestions",
                schema: "travelPlans");
        }
    }
}
