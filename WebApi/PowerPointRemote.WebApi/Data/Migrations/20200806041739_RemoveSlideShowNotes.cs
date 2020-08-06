using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class RemoveSlideShowNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CurrentSlideNotes",
                "Channels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "CurrentSlideNotes",
                "Channels",
                "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}