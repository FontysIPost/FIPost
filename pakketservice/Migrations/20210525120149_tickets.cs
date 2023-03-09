using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PakketService.Migrations
{
    public partial class tickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Package_PackageId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "CreatedByPCN",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "FinishedByPCN",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TicketAction",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TrackAndTraceId",
                table: "Package");

            migrationBuilder.RenameColumn(
                name: "ToDoLocationId",
                table: "Ticket",
                newName: "ReceivedByPersonId");

            migrationBuilder.RenameColumn(
                name: "NextTicketId",
                table: "Ticket",
                newName: "CompletedByPersonId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PackageId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Package_PackageId",
                table: "Ticket",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Package_PackageId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "ReceivedByPersonId",
                table: "Ticket",
                newName: "ToDoLocationId");

            migrationBuilder.RenameColumn(
                name: "CompletedByPersonId",
                table: "Ticket",
                newName: "NextTicketId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PackageId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Ticket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedByPCN",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishedByPCN",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Ticket",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TicketAction",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TrackAndTraceId",
                table: "Package",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Package_PackageId",
                table: "Ticket",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
