using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Travels.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TravelRating_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                schema: "travels",
                table: "Travels",
                newName: "RatingValue");

            migrationBuilder.CreateTable(
                name: "TravelRating",
                schema: "travels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TravelId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelRating_Travels_TravelId",
                        column: x => x.TravelId,
                        principalSchema: "travels",
                        principalTable: "Travels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelRating_TravelId",
                schema: "travels",
                table: "TravelRating",
                column: "TravelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelRating",
                schema: "travels");

            migrationBuilder.RenameColumn(
                name: "RatingValue",
                schema: "travels",
                table: "Travels",
                newName: "Rating");
        }
    }
}
