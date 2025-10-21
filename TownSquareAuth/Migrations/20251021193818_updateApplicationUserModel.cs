using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TownSquareAuth.Migrations
{
    /// <inheritdoc />
    public partial class updateApplicationUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AplicationUserId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AplicationUserId",
                table: "AspNetUsers");
        }
    }
}
