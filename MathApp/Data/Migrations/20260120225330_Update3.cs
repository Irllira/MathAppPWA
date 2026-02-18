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
            newName: "IncorrectDefinitionId",
            schema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "IncorrectDefinitionId",
            table: "DefIncPair",
            newName: "IncorrectID",
            schema: "dbo");
        }
    }
}
