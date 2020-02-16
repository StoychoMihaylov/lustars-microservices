namespace ProfileAPI.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    public partial class initialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 15, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    AgeRange = table.Column<int>(nullable: false),
                    Biography = table.Column<string>(maxLength: 2500, nullable: true),
                    City = table.Column<string>(nullable: true),
                    Credits = table.Column<int>(nullable: false),
                    Superlikes = table.Column<int>(nullable: false),
                    LookingFor = table.Column<string>(nullable: true),
                    WantToHaveKids = table.Column<bool>(nullable: false),
                    EducationDegree = table.Column<string>(nullable: true),
                    University = table.Column<string>(nullable: true),
                    Work = table.Column<string>(nullable: true),
                    Languages = table.Column<string>(nullable: true),
                    EmailNotificationsSubscribe = table.Column<bool>(nullable: false),
                    IsActivated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserProfileId",
                table: "Images",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
