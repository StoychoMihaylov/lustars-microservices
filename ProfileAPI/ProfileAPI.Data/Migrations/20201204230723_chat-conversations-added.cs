using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileAPI.Data.Migrations
{
    public partial class chatconversationsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatConversations",
                columns: table => new
                {
                    ChatStarterUserId = table.Column<Guid>(nullable: false),
                    InvitedUserId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    StartedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatConversations", x => new { x.ChatStarterUserId, x.InvitedUserId });
                    table.ForeignKey(
                        name: "FK_ChatConversations_UserProfiles_ChatStarterUserId",
                        column: x => x.ChatStarterUserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatConversations_UserProfiles_InvitedUserId",
                        column: x => x.InvitedUserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatConversations_InvitedUserId",
                table: "ChatConversations",
                column: "InvitedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatConversations");
        }
    }
}
