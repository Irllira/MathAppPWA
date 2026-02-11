using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathApp.Migrations
{
    /// <inheritdoc />
    public partial class IncDefs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefinitionId",
                table: "IncorrectDefinitions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncorrectDefinitions_DefinitionId",
                table: "IncorrectDefinitions",
                column: "DefinitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncorrectDefinitions_Definitions_DefinitionId",
                table: "IncorrectDefinitions",
                column: "DefinitionId",
                principalTable: "Definitions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncorrectDefinitions_Definitions_DefinitionId",
                table: "IncorrectDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_IncorrectDefinitions_DefinitionId",
                table: "IncorrectDefinitions");

            migrationBuilder.DropColumn(
                name: "DefinitionId",
                table: "IncorrectDefinitions");
        }
    }
}
