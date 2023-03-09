using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PakketService.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackAndTraceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionPointId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    RouteFinished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToDoLocationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByPCN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishedAt = table.Column<double>(type: "float", nullable: false),
                    FinishedByPCN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    NextTicketId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketAction = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Package_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Package",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PackageId",
                table: "Ticket",
                column: "PackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Package");
        }
    }
}
