namespace ProfileAPI.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class userprofilefieldschanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeRangeFrom",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "AgeRangeTo",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "EmailNotificationsSubscribe",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "AvatarImage",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DrinkAlcohol",
                table: "UserProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailNotificationsSubscribed",
                table: "UserProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Figure",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HaveKids",
                table: "UserProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HowOftenDrinkAlcohol",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HowOftenSmoke",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Income",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsUserProfileActivated",
                table: "UserProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeritalStatus",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartnerAgeRangeFrom",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartnerAgeRangeTo",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PartnerDrinkAlcohol",
                table: "UserProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PartnerFigure",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PartnerHaveKids",
                table: "UserProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PartnerIncomeFrom",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartnerIncomeTo",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PartnerSmoke",
                table: "UserProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Smoker",
                table: "UserProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "UserProfiles",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WantKids",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImage",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "DrinkAlcohol",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "EmailNotificationsSubscribed",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Figure",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "HaveKids",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "HowOftenDrinkAlcohol",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "HowOftenSmoke",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Income",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "IsUserProfileActivated",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "MeritalStatus",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PartnerAgeRangeFrom",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PartnerAgeRangeTo",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PartnerDrinkAlcohol",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PartnerFigure",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PartnerHaveKids",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PartnerIncomeFrom",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PartnerIncomeTo",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PartnerSmoke",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Smoker",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "WantKids",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "UserProfiles");

            migrationBuilder.AddColumn<int>(
                name: "AgeRangeFrom",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AgeRangeTo",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "EmailNotificationsSubscribe",
                table: "UserProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "UserProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
