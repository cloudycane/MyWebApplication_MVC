using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories2",
                table: "Categories2");

            migrationBuilder.RenameTable(
                name: "Categories2",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories2",
                table: "Categories2",
                column: "Id");
        }
    }
}
