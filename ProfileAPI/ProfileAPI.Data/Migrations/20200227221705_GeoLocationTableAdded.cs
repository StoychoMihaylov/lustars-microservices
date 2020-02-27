using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProfileAPI.Data.Migrations
{
    public partial class GeoLocationTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeRange",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "Biography",
                table: "UserProfiles",
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2500)",
                oldMaxLength: 2500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgeRangeFrom",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AgeRangeTo",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GeoLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    UserProfileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeoLocations_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoLocations_UserProfileId",
                table: "GeoLocations",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "AgeRangeFrom",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "AgeRangeTo",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "Biography",
                table: "UserProfiles",
                type: "character varying(2500)",
                maxLength: 2500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgeRange",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
