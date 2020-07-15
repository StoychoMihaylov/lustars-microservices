using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileAPI.Data.Migrations
{
    public partial class userprofileupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExploreNewCulturesAndLanguages",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LongGoodConversation",
                table: "UserProfiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ExploreNewCulturesAndLanguages",
                table: "UserProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LongGoodConversation",
                table: "UserProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
