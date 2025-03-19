using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Payments_module_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Payments");

            migrationBuilder.CreateTable(
                name: "TravelSummaries",
                schema: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TravelId = table.Column<Guid>(type: "uuid", nullable: false),
                    From = table.Column<DateOnly>(type: "date", nullable: false),
                    To = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TravelAdditionalCost = table.Column<decimal>(type: "numeric", nullable: false),
                    PointsAdditionalCost = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelSummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantsCosts",
                schema: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SummaryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantsCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantsCosts_TravelSummaries_SummaryId",
                        column: x => x.SummaryId,
                        principalSchema: "Payments",
                        principalTable: "TravelSummaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantsCosts_SummaryId",
                schema: "Payments",
                table: "ParticipantsCosts",
                column: "SummaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantsCosts",
                schema: "Payments");

            migrationBuilder.DropTable(
                name: "TravelSummaries",
                schema: "Payments");
        }
    }
}
