using Microsoft.EntityFrameworkCore.Migrations;

namespace CherepkoLib.Migrations
{
    public partial class AntitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RodGroups",
                columns: table => new
                {
                    RodGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RodGroups", x => x.RodGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Rods",
                columns: table => new
                {
                    RodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RodGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rods", x => x.RodId);
                    table.ForeignKey(
                        name: "FK_Rods_RodGroups_RodGroupId",
                        column: x => x.RodGroupId,
                        principalTable: "RodGroups",
                        principalColumn: "RodGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rods_RodGroupId",
                table: "Rods",
                column: "RodGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rods");

            migrationBuilder.DropTable(
                name: "RodGroups");
        }
    }
}
