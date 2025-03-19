using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantCost_To_Receipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantsCosts",
                schema: "payments");

            migrationBuilder.CreateTable(
                name: "Receipts",
                schema: "payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantsIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    TravelSummaryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_TravelSummaries_TravelSummaryId",
                        column: x => x.TravelSummaryId,
                        principalSchema: "payments",
                        principalTable: "TravelSummaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_TravelSummaryId",
                schema: "payments",
                table: "Receipts",
                column: "TravelSummaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receipts",
                schema: "payments");

            migrationBuilder.CreateTable(
                name: "ParticipantsCosts",
                schema: "payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantsIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    SummaryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantsCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantsCosts_TravelSummaries_SummaryId",
                        column: x => x.SummaryId,
                        principalSchema: "payments",
                        principalTable: "TravelSummaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantsCosts_SummaryId",
                schema: "payments",
                table: "ParticipantsCosts",
                column: "SummaryId");
        }
    }
}
