using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPlans_TravelPointCosts_and_Receipts_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelPointCosts",
                schema: "travelPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TravelPointId = table.Column<Guid>(type: "uuid", nullable: false),
                    OverallCost = table.Column<decimal>(type: "numeric", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPointCosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                schema: "travelPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TravelPointCostId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_TravelPointCosts_TravelPointCostId",
                        column: x => x.TravelPointCostId,
                        principalSchema: "travelPlans",
                        principalTable: "TravelPointCosts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_TravelPointCostId",
                schema: "travelPlans",
                table: "Receipts",
                column: "TravelPointCostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receipts",
                schema: "travelPlans");

            migrationBuilder.DropTable(
                name: "TravelPointCosts",
                schema: "travelPlans");
        }
    }
}
