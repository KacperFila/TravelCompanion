using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPointCostAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPointCosts_TravelPointCostId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "travelPlans",
                table: "TravelPointCosts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TravelPointCostId",
                schema: "travelPlans",
                table: "Receipts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelPointCosts_TravelPointId",
                schema: "travelPlans",
                table: "TravelPointCosts",
                column: "TravelPointId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPointCosts_TravelPointCostId",
                schema: "travelPlans",
                table: "Receipts",
                column: "TravelPointCostId",
                principalSchema: "travelPlans",
                principalTable: "TravelPointCosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPointCosts_TravelPoints_TravelPointId",
                schema: "travelPlans",
                table: "TravelPointCosts",
                column: "TravelPointId",
                principalSchema: "travelPlans",
                principalTable: "TravelPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPointCosts_TravelPointCostId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelPointCosts_TravelPoints_TravelPointId",
                schema: "travelPlans",
                table: "TravelPointCosts");

            migrationBuilder.DropIndex(
                name: "IX_TravelPointCosts_TravelPointId",
                schema: "travelPlans",
                table: "TravelPointCosts");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                schema: "travelPlans",
                table: "TravelPointCosts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "TravelPointCostId",
                schema: "travelPlans",
                table: "Receipts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPointCosts_TravelPointCostId",
                schema: "travelPlans",
                table: "Receipts",
                column: "TravelPointCostId",
                principalSchema: "travelPlans",
                principalTable: "TravelPointCosts",
                principalColumn: "Id");
        }
    }
}
