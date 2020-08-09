using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileAPI.Data.Migrations
{
    public partial class profilevisitorsjointableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileVisitor",
                columns: table => new
                {
                    VisitorId = table.Column<Guid>(nullable: false),
                    VisitedId = table.Column<Guid>(nullable: false),
                    onDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileVisitor", x => new { x.VisitorId, x.VisitedId });
                    table.ForeignKey(
                        name: "FK_ProfileVisitor_UserProfiles_VisitedId",
                        column: x => x.VisitedId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileVisitor_UserProfiles_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileVisitor_VisitedId",
                table: "ProfileVisitor",
                column: "VisitedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileVisitor");
        }
    }
}
