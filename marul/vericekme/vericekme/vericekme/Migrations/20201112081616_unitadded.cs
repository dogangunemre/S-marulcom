using Microsoft.EntityFrameworkCore.Migrations;

namespace vericekme.Migrations
{
    public partial class unitadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    Source = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitID",
                table: "Products",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Unit_UnitID",
                table: "Products",
                column: "UnitID",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Unit_UnitID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Products_UnitID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitID",
                table: "Products");
        }
    }
}
