#nullable disable

namespace TestPlatform.Database.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddPointsToQuestionTestMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "QuestionsTestsMap",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "QuestionsTestsMap");
        }
    }
}
