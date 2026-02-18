using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathApp.Migrations
{
    /// <inheritdoc />
    public partial class MinorUpdatw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Pages");

            migrationBuilder.RenameColumn(
                name: "link",
                table: "Pages",
                newName: "Link");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "IncorrectDefinitions",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Pages",
                newName: "link");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "IncorrectDefinitions",
                newName: "content");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
