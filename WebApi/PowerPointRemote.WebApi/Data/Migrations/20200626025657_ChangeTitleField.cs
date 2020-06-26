using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class ChangeTitleField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "SlideShowTitle",
                "Channels");

            migrationBuilder.AddColumn<string>(
                "SlideShowName",
                "Channels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "SlideShowName",
                "Channels");

            migrationBuilder.AddColumn<string>(
                "SlideShowTitle",
                "Channels",
                "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}