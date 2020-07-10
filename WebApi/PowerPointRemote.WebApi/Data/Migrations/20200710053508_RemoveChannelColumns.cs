using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class RemoveChannelColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "HostConnectionId",
                "Channels");

            migrationBuilder.DropColumn(
                "Name",
                "Channels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "HostConnectionId",
                "Channels",
                "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "Name",
                "Channels",
                "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}