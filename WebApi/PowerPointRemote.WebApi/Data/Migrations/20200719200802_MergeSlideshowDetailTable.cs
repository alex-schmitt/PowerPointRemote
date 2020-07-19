using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class MergeSlideshowDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "SlideShowDetail");

            migrationBuilder.AddColumn<string>(
                "CurrentSlideNotes",
                "Channels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "CurrentSlidePosition",
                "Channels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "SlideCount",
                "Channels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                "SlideShowStarted",
                "Channels",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CurrentSlideNotes",
                "Channels");

            migrationBuilder.DropColumn(
                "CurrentSlidePosition",
                "Channels");

            migrationBuilder.DropColumn(
                "SlideCount",
                "Channels");

            migrationBuilder.DropColumn(
                "SlideShowStarted",
                "Channels");

            migrationBuilder.CreateTable(
                "SlideShowDetail",
                table => new
                {
                    Id = table.Column<Guid>("char(36)", nullable: false),
                    ChannelId = table.Column<string>("char(9)", nullable: true),
                    CurrentSlide = table.Column<int>("int", nullable: false),
                    Enabled = table.Column<bool>("tinyint(1)", nullable: false),
                    LastUpdate = table.Column<DateTime>("datetime(6)", nullable: false),
                    Name = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: true),
                    TotalSlides = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideShowDetail", x => x.Id);
                    table.ForeignKey(
                        "FK_SlideShowDetail_Channels_ChannelId",
                        x => x.ChannelId,
                        "Channels",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_SlideShowDetail_ChannelId",
                "SlideShowDetail",
                "ChannelId",
                unique: true);
        }
    }
}