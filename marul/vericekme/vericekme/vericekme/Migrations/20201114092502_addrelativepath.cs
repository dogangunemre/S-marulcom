using Microsoft.EntityFrameworkCore.Migrations;

namespace vericekme.Migrations
{
    public partial class addrelativepath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RelativePath",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelativePath",
                table: "Files");
        }
    }
}
