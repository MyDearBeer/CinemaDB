using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaDB.Migrations
{
    /// <inheritdoc />
    public partial class FNameAltKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AlternateKey_FName",
                table: "Films",
                column: "F_Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AlternateKey_FName",
                table: "Films");
        }
    }
}
