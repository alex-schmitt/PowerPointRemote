using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class SeparateSlideShowDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CurrentSlide",
                "Channels");

            migrationBuilder.DropColumn(
                "SlideShowEnabled",
                "Channels");

            migrationBuilder.DropColumn(
                "SlideShowName",
                "Channels");

            migrationBuilder.DropColumn(
                "TotalSlides",
                "Channels");

            migrationBuilder.CreateTable(
                "SlideShowDetail",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CurrentSlide = table.Column<int>(nullable: false),
                    TotalSlides = table.Column<int>(nullable: false),
                    ChannelId = table.Column<string>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "SlideShowDetail");

            migrationBuilder.AddColumn<int>(
                "CurrentSlide",
                "Channels",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                "SlideShowEnabled",
                "Channels",
                "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                "SlideShowName",
                "Channels",
                "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "TotalSlides",
                "Channels",
                "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}