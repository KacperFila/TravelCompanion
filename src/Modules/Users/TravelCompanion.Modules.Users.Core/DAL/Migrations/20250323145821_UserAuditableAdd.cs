using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompanion.Modules.Users.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserAuditableAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "users",
                table: "Users",
                newName: "CreatedOnUtc");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                schema: "users",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                schema: "users",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                schema: "users",
                table: "Users",
                newName: "CreatedAt");
        }
    }
}
