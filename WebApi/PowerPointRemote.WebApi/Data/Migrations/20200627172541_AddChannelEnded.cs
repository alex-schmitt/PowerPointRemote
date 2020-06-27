using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class AddChannelEnded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                "ChannelEnded",
                "Channels",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "ChannelEnded",
                "Channels");
        }
    }
}