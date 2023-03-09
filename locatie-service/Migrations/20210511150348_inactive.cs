using Microsoft.EntityFrameworkCore.Migrations;

namespace LocatieService.Migrations
{
    public partial class inactive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "rooms",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "cities",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "buildings",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "cities");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "buildings");
        }
    }
}
