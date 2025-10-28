using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TownSquareAuth.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AplicationUserId",
                table: "AspNetUsers",
                newName: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "AspNetUsers",
                newName: "AplicationUserId");
        }
    }
}
