using Microsoft.EntityFrameworkCore.Migrations;

namespace personeel_service.Migrations
{
    public partial class fontysIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FontysId",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FontysId",
                table: "Person");
        }
    }
}
