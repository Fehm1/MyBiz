using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBiz.Migrations
{
    public partial class TeamsTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinkedInmUrl",
                table: "Teams",
                newName: "LinkedInUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinkedInUrl",
                table: "Teams",
                newName: "LinkedInmUrl");
        }
    }
}
