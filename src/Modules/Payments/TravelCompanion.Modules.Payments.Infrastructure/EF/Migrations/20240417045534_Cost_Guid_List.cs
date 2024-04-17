using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Cost_Guid_List : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantId",
                schema: "payments",
                table: "ParticipantsCosts");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "ParticipantsIds",
                schema: "payments",
                table: "ParticipantsCosts",
                type: "uuid[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Value_Currency",
                schema: "payments",
                table: "ParticipantsCosts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantsIds",
                schema: "payments",
                table: "ParticipantsCosts");

            migrationBuilder.DropColumn(
                name: "Value_Currency",
                schema: "payments",
                table: "ParticipantsCosts");

            migrationBuilder.AddColumn<Guid>(
                name: "ParticipantId",
                schema: "payments",
                table: "ParticipantsCosts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
