using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathApp.Migrations
{
    /// <inheritdoc />
    public partial class IncDefs4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Part1",
                table: "IncorrectDefinitions");

            migrationBuilder.DropColumn(
                name: "Part2",
                table: "IncorrectDefinitions");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "IncorrectDefinitions",
                newName: "content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "content",
                table: "IncorrectDefinitions",
                newName: "type");

            migrationBuilder.AddColumn<string>(
                name: "Part1",
                table: "IncorrectDefinitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Part2",
                table: "IncorrectDefinitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
