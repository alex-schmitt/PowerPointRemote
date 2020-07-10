using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class ChangeUserConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "UserConnections");

            migrationBuilder.AddColumn<int>(
                "Connections",
                "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Connections",
                "Users");

            migrationBuilder.CreateTable(
                "UserConnections",
                table => new
                {
                    Id = table.Column<string>("varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ChannelId = table.Column<string>("char(9)", nullable: false),
                    UserId = table.Column<Guid>("char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnections", x => x.Id);
                    table.ForeignKey(
                        "FK_UserConnections_Channels_ChannelId",
                        x => x.ChannelId,
                        "Channels",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_UserConnections_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_UserConnections_ChannelId",
                "UserConnections",
                "ChannelId");

            migrationBuilder.CreateIndex(
                "IX_UserConnections_UserId",
                "UserConnections",
                "UserId");
        }
    }
}