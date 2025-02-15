using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class PointReceipts_Multiple_Participants_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "ReceiptParticipants",
                schema: "travelPlans",
                table: "Receipts",
                type: "uuid[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptParticipants",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.AddColumn<Guid>(
                name: "ParticipantId",
                schema: "travelPlans",
                table: "Receipts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
