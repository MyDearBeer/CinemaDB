using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaDB.Migrations
{
    /// <inheritdoc />
    public partial class DelComp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "WorkersCount",
                table: "Cinema");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cinema");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Color",
                table: "Cinema",
                type: "smallint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cinema",
                keyColumn: "ID",
                keyValue: 1,
                column: "Color",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cinema",
                keyColumn: "ID",
                keyValue: 2,
                column: "Color",
                value: null);

            migrationBuilder.AddCheckConstraint(
                name: "WorkersCount",
                table: "Cinema",
                sql: "WorkersCount>0");
        }
    }
}
