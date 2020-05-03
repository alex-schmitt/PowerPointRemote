using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Channels",
                table => new
                {
                    Id = table.Column<string>("char(9)", nullable: false),
                    HostConnectionId = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Channels", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    ChannelId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        "FK_Users_Channels_ChannelId",
                        x => x.ChannelId,
                        "Channels",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserConnections",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ChannelId = table.Column<string>(nullable: false)
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

            migrationBuilder.CreateIndex(
                "IX_Users_ChannelId",
                "Users",
                "ChannelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "UserConnections");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "Channels");
        }
    }
}