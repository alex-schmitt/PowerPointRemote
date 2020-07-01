using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    public partial class AddChannelName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Channels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Channels");
        }
    }
}
