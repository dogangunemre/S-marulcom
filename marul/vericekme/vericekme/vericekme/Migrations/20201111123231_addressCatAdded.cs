using Microsoft.EntityFrameworkCore.Migrations;

namespace vericekme.Migrations
{
    public partial class addressCatAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Source",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Brands");
        }
    }
}
