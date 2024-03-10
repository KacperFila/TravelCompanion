using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Travels.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TravelPoints_travelId_pointId_optional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Travels_TravelId",
                schema: "travels",
                table: "Receipts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TravelId",
                schema: "travels",
                table: "Receipts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Travels_TravelId",
                schema: "travels",
                table: "Receipts",
                column: "TravelId",
                principalSchema: "travels",
                principalTable: "Travels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Travels_TravelId",
                schema: "travels",
                table: "Receipts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TravelId",
                schema: "travels",
                table: "Receipts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Travels_TravelId",
                schema: "travels",
                table: "Receipts",
                column: "TravelId",
                principalSchema: "travels",
                principalTable: "Travels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
