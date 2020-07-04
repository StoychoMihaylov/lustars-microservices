using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProfileAPI.Data.Migrations
{
    public partial class userprofiledataupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Credits = table.Column<int>(nullable: false),
                    LustarLikes = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    EmailNotificationsSubscribed = table.Column<bool>(nullable: false),
                    IsUserProfileActivated = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 15, nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    BiographyAndInterests = table.Column<string>(maxLength: 3000, nullable: true),
                    AvatarImage = table.Column<string>(nullable: true),
                    FromCity = table.Column<string>(nullable: true),
                    FromCountry = table.Column<string>(nullable: true),
                    FeelInMood = table.Column<string>(maxLength: 20, nullable: true),
                    LookingFor = table.Column<string>(nullable: true),
                    EducationDegree = table.Column<string>(nullable: true),
                    University = table.Column<string>(nullable: true),
                    Work = table.Column<string>(nullable: true),
                    MeritalStatus = table.Column<string>(nullable: true),
                    WantKids = table.Column<string>(nullable: true),
                    HaveKids = table.Column<bool>(nullable: false),
                    DrinkAlcohol = table.Column<bool>(nullable: false),
                    HowOftenDrinkAlcohol = table.Column<string>(nullable: true),
                    Smoker = table.Column<bool>(nullable: false),
                    HowOftenSmoke = table.Column<string>(nullable: true),
                    DoingSport = table.Column<string>(nullable: true),
                    HowOftenDoSport = table.Column<string>(nullable: true),
                    Height = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Figure = table.Column<string>(nullable: true),
                    PartnerAgeRangeFrom = table.Column<int>(nullable: false),
                    PartnerAgeRangeTo = table.Column<int>(nullable: false),
                    PartnerSmoke = table.Column<bool>(nullable: false),
                    PartnerDrinkAlcohol = table.Column<bool>(nullable: false),
                    PartnerHaveKids = table.Column<bool>(nullable: false),
                    PartnerFigure = table.Column<string>(nullable: true),
                    Love = table.Column<bool>(nullable: false),
                    Trust = table.Column<bool>(nullable: false),
                    Sex = table.Column<bool>(nullable: false),
                    FinancialStability = table.Column<bool>(nullable: false),
                    RespectAndUnderstanding = table.Column<bool>(nullable: false),
                    SameInterests = table.Column<bool>(nullable: false),
                    OppositeAttracs = table.Column<bool>(nullable: false),
                    ExploreNewCulturesAndLanguages = table.Column<bool>(nullable: false),
                    GrowingFamily = table.Column<bool>(nullable: false),
                    LongGoodConversation = table.Column<bool>(nullable: false),
                    LoveForAnimals = table.Column<bool>(nullable: false),
                    ShareSameReligion = table.Column<bool>(nullable: false),
                    KeepTraditions = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoLocations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(nullable: true),
                    UploadedOn = table.Column<DateTime>(nullable: false),
                    UserProfileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UserProfileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoLocations_UserProfileId",
                table: "GeoLocations",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserProfileId",
                table: "Images",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_UserProfileId",
                table: "Languages",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoLocations");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
