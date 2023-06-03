#nullable disable

namespace TestPlatform.Database.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddGradeColumnToTestUserMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "TestsUsersMap",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "TestsUsersMap");
        }
    }
}
