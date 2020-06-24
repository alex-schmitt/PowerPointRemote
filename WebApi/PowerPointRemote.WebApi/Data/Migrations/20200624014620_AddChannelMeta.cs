using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class AddChannelMeta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "CurrentSlide",
                "Channels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                "LastUpdate",
                "Channels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                "SlideShowTitle",
                "Channels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "TotalSlides",
                "Channels",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CurrentSlide",
                "Channels");

            migrationBuilder.DropColumn(
                "LastUpdate",
                "Channels");

            migrationBuilder.DropColumn(
                "SlideShowTitle",
                "Channels");

            migrationBuilder.DropColumn(
                "TotalSlides",
                "Channels");
        }
    }
}