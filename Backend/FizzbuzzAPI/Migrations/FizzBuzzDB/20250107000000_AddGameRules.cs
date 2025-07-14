using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FizzbuzzAPI.Migrations.FizzBuzzDB
{
    /// <inheritdoc />
    public partial class AddGameRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create GameRules table
            migrationBuilder.CreateTable(
                name: "GameRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Divisor = table.Column<int>(type: "int", nullable: false),
                    Replacement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRules_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create index on GameId for better performance
            migrationBuilder.CreateIndex(
                name: "IX_GameRules_GameId",
                table: "GameRules",
                column: "GameId");

            // Remove the old Divisor and Replacement columns from Games table
            migrationBuilder.DropColumn(
                name: "Divisor",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Replacement",
                table: "Games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add back the old columns
            migrationBuilder.AddColumn<int>(
                name: "Divisor",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Replacement",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // Drop the GameRules table
            migrationBuilder.DropTable(
                name: "GameRules");
        }
    }
} 