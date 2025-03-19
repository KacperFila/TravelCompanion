using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Receipts_IsRequired_PlanId_PointId_To_Optional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Plans_PlanId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoints_PointId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.AlterColumn<Guid>(
                name: "PointId",
                schema: "travelPlans",
                table: "Receipts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanId",
                schema: "travelPlans",
                table: "Receipts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Plans_PlanId",
                schema: "travelPlans",
                table: "Receipts",
                column: "PlanId",
                principalSchema: "travelPlans",
                principalTable: "Plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPoints_PointId",
                schema: "travelPlans",
                table: "Receipts",
                column: "PointId",
                principalSchema: "travelPlans",
                principalTable: "TravelPoints",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Plans_PlanId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_TravelPoints_PointId",
                schema: "travelPlans",
                table: "Receipts");

            migrationBuilder.AlterColumn<Guid>(
                name: "PointId",
                schema: "travelPlans",
                table: "Receipts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanId",
                schema: "travelPlans",
                table: "Receipts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Plans_PlanId",
                schema: "travelPlans",
                table: "Receipts",
                column: "PlanId",
                principalSchema: "travelPlans",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_TravelPoints_PointId",
                schema: "travelPlans",
                table: "Receipts",
                column: "PointId",
                principalSchema: "travelPlans",
                principalTable: "TravelPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
