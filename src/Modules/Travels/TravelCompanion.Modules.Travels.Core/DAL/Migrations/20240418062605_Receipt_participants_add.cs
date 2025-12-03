using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Travels.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Receipt_participants_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<Guid>>(
                name: "ReceiptParticipants",
                schema: "travels",
                table: "Receipts",
                type: "uuid[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptParticipants",
                schema: "travels",
                table: "Receipts");
        }
    }
}
