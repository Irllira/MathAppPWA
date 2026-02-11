using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathApp.Migrations
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "IncorrectID",
            table: "DefIncPair",
            newName: "IncorrectDefinitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "IncorrectID",
            table: "DefIncPair",
            newName: "IncorrectDefinitionId");
        }
    }
}
