using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPoint_TravelPlanId_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPoints_Plans_PlanId",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.DropColumn(
                name: "TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPoints_Plans_PlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                column: "PlanId",
                principalSchema: "travelPlans",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPoints_Plans_PlanId",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPoints_Plans_PlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                column: "PlanId",
                principalSchema: "travelPlans",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
