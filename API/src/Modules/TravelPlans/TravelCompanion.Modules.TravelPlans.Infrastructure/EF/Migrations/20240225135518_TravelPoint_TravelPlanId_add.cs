using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class TravelPoint_TravelPlanId_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPoints_TravelPlans_TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.AlterColumn<Guid>(
                name: "TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPoints_TravelPlans_TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                column: "TravelPlanId",
                principalSchema: "travelPlans",
                principalTable: "TravelPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPoints_TravelPlans_TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints");

            migrationBuilder.AlterColumn<Guid>(
                name: "TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPoints_TravelPlans_TravelPlanId",
                schema: "travelPlans",
                table: "TravelPoints",
                column: "TravelPlanId",
                principalSchema: "travelPlans",
                principalTable: "TravelPlans",
                principalColumn: "Id");
        }
    }
}
