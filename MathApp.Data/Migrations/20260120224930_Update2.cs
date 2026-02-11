using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathApp.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefinitionID",
                table: "DefIncPair",
                newName: "DefinitionId");

            migrationBuilder.RenameColumn(
               name: "IncorrectID",
               table: "DefIncPair",
               newName: "IncorrectDefinitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.RenameColumn(
                name: "DefinitionId",
                table: "DefIncPair",
                newName: "DefinitionID");

          
        }
    }
}
