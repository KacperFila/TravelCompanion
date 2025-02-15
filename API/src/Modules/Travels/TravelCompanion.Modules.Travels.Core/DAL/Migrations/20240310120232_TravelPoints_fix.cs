using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Travels.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TravelPoints_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoint_TravelId",
                schema: "travels",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelPoint_Travels_TravelId",
                schema: "travels",
                table: "TravelPoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelPoint",
                schema: "travels",
                table: "TravelPoint");

            migrationBuilder.RenameTable(
                name: "TravelPoint",
                schema: "travels",
                newName: "TravelPoints",
                newSchema: "travels");

            migrationBuilder.RenameIndex(
                name: "IX_TravelPoint_TravelId",
                schema: "travels",
                table: "TravelPoints",
                newName: "IX_TravelPoints_TravelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelPoints",
                schema: "travels",
                table: "TravelPoints",
                column: "TravelPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPoints_TravelId",
                schema: "travels",
                table: "Receipts",
                column: "TravelId",
                principalSchema: "travels",
                principalTable: "TravelPoints",
                principalColumn: "TravelPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPoints_Travels_TravelId",
                schema: "travels",
                table: "TravelPoints",
                column: "TravelId",
                principalSchema: "travels",
                principalTable: "Travels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoints_TravelId",
                schema: "travels",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelPoints_Travels_TravelId",
                schema: "travels",
                table: "TravelPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TravelPoints",
                schema: "travels",
                table: "TravelPoints");

            migrationBuilder.RenameTable(
                name: "TravelPoints",
                schema: "travels",
                newName: "TravelPoint",
                newSchema: "travels");

            migrationBuilder.RenameIndex(
                name: "IX_TravelPoints_TravelId",
                schema: "travels",
                table: "TravelPoint",
                newName: "IX_TravelPoint_TravelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TravelPoint",
                schema: "travels",
                table: "TravelPoint",
                column: "TravelPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPoint_TravelId",
                schema: "travels",
                table: "Receipts",
                column: "TravelId",
                principalSchema: "travels",
                principalTable: "TravelPoint",
                principalColumn: "TravelPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPoint_Travels_TravelId",
                schema: "travels",
                table: "TravelPoint",
                column: "TravelId",
                principalSchema: "travels",
                principalTable: "Travels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
