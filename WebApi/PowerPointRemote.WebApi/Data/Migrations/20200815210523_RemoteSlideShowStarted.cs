using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class RemoteSlideShowStarted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "SlideShowStarted",
                "Channels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                "SlideShowStarted",
                "Channels",
                "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}