#nullable disable

namespace TestPlatform.Database.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveUserAgentFromChatConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAgent",
                table: "ChatConnetions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAgent",
                table: "ChatConnetions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
