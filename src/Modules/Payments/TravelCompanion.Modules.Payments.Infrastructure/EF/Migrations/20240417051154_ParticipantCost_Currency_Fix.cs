using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantCost_Currency_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value_Currency",
                schema: "payments",
                table: "ParticipantsCosts",
                newName: "Currency");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                schema: "payments",
                table: "ParticipantsCosts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                schema: "payments",
                table: "ParticipantsCosts",
                newName: "Value_Currency");

            migrationBuilder.AlterColumn<string>(
                name: "Value_Currency",
                schema: "payments",
                table: "ParticipantsCosts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
