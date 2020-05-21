using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileAPI.Data.Migrations
{
    public partial class PartnerIncomeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartnerIncomeFrom",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PartnerIncomeTo",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "PartnerIncome",
                table: "UserProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartnerIncome",
                table: "UserProfiles");

            migrationBuilder.AddColumn<int>(
                name: "PartnerIncomeFrom",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartnerIncomeTo",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
