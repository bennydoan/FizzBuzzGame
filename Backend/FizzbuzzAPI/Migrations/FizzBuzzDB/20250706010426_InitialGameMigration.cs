using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FizzbuzzAPI.Migrations.FizzBuzzDB
{
    /// <inheritdoc />
    public partial class InitialGameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeDuration = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<int>(type: "int", nullable: false),
                    End = table.Column<int>(type: "int", nullable: false),
                    Divisor = table.Column<int>(type: "int", nullable: false),
                    Replacement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
