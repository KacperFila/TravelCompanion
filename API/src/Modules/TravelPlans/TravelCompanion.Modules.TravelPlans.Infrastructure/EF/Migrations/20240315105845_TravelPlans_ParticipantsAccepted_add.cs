using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPlans_ParticipantsAccepted_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AllParticipantsPaid",
                schema: "travelPlans",
                table: "Plans",
                newName: "DoesAllParticipantsPaid");

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "Participants",
                schema: "travelPlans",
                table: "Plans",
                type: "uuid[]",
                nullable: false,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "ParticipantPaidIds",
                schema: "travelPlans",
                table: "Plans",
                type: "uuid[]",
                nullable: true,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]");

            migrationBuilder.AddColumn<bool>(
                name: "DoesAllParticipantsAccepted",
                schema: "travelPlans",
                table: "Plans",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<List<Guid>>(
                name: "ParticipantsAccepted",
                schema: "travelPlans",
                table: "Plans",
                type: "uuid[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoesAllParticipantsAccepted",
                schema: "travelPlans",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "ParticipantsAccepted",
                schema: "travelPlans",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "DoesAllParticipantsPaid",
                schema: "travelPlans",
                table: "Plans",
                newName: "AllParticipantsPaid");

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "Participants",
                schema: "travelPlans",
                table: "Plans",
                type: "uuid[]",
                nullable: true,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]");

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "ParticipantPaidIds",
                schema: "travelPlans",
                table: "Plans",
                type: "uuid[]",
                nullable: false,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]",
                oldNullable: true);
        }
    }
}
