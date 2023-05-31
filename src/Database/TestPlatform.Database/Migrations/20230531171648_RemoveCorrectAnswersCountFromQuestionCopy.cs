#nullable disable

namespace TestPlatform.Database.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveCorrectAnswersCountFromQuestionCopy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestЕvaluations_ЕvaluationId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_ЕvaluationId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "CorrectAnswersCount",
                table: "QuestionCopies");

            migrationBuilder.CreateIndex(
                name: "IX_TestЕvaluations_TestId",
                table: "TestЕvaluations",
                column: "TestId",
                unique: true,
                filter: "[TestId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TestЕvaluations_Tests_TestId",
                table: "TestЕvaluations",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestЕvaluations_Tests_TestId",
                table: "TestЕvaluations");

            migrationBuilder.DropIndex(
                name: "IX_TestЕvaluations_TestId",
                table: "TestЕvaluations");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersCount",
                table: "QuestionCopies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_ЕvaluationId",
                table: "Tests",
                column: "ЕvaluationId",
                unique: true,
                filter: "[ЕvaluationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestЕvaluations_ЕvaluationId",
                table: "Tests",
                column: "ЕvaluationId",
                principalTable: "TestЕvaluations",
                principalColumn: "Id");
        }
    }
}
