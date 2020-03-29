namespace ProfileAPI.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class emaildeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserProfiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserProfiles",
                type: "text",
                nullable: true);
        }
    }
}
