using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaDB.Migrations
{
    /// <inheritdoc />
    public partial class FA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Actors__3214EC27FF254FBK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FA",
                columns: table => new
                {
                    ActorsId = table.Column<int>(type: "int", nullable: false),
                    FilmsName = table.Column<int>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FA", x => new { x.ActorsId, x.FilmsName});
                    table.ForeignKey(
                        name: "FK_FA_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FA_Films_FilmsName",
                        column: x => x.FilmsName,
                        principalTable: "Films",
                        principalColumn: "F_Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FA_FilmsName",
                table: "FA",
                column: "FilmsName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FA");

            migrationBuilder.DropTable(
                name: "Actors");
        }
    }
}
