using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileAPI.Data.Migrations
{
    public partial class changesonuserprofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Love",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "RespectAndUnderstanding",
                table: "UserProfiles");

            migrationBuilder.AddColumn<bool>(
                name: "CommunicationAndUnderstanding",
                table: "UserProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PartnerVisualAppearance",
                table: "UserProfiles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommunicationAndUnderstanding",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PartnerVisualAppearance",
                table: "UserProfiles");

            migrationBuilder.AddColumn<bool>(
                name: "Love",
                table: "UserProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RespectAndUnderstanding",
                table: "UserProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
