using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class RenameSlidePosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CurrentSlidePosition",
                "Channels");

            migrationBuilder.AddColumn<int>(
                "SlidePosition",
                "Channels",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "SlidePosition",
                "Channels");

            migrationBuilder.AddColumn<int>(
                "CurrentSlidePosition",
                "Channels",
                "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}