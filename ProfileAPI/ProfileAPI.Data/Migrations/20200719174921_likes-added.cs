using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileAPI.Data.Migrations
{
    public partial class likesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileId",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileId1",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserProfileId",
                table: "UserProfiles",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserProfileId1",
                table: "UserProfiles",
                column: "UserProfileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_UserProfiles_UserProfileId",
                table: "UserProfiles",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_UserProfiles_UserProfileId1",
                table: "UserProfiles",
                column: "UserProfileId1",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_UserProfiles_UserProfileId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_UserProfiles_UserProfileId1",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UserProfileId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UserProfileId1",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UserProfileId1",
                table: "UserProfiles");
        }
    }
}
