#nullable disable

namespace TestPlatform.Database.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveCorrectAnswersCountFromQuestionCopy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestEvaluations_EvaluationId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_EvaluationId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "CorrectAnswersCount",
                table: "QuestionCopies");

            migrationBuilder.CreateIndex(
                name: "IX_TestEvaluations_TestId",
                table: "TestEvaluations",
                column: "TestId",
                unique: true,
                filter: "[TestId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TestEvaluations_Tests_TestId",
                table: "TestEvaluations",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestEvaluations_Tests_TestId",
                table: "TestEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_TestEvaluations_TestId",
                table: "TestEvaluations");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersCount",
                table: "QuestionCopies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_EvaluationId",
                table: "Tests",
                column: "EvaluationId",
                unique: true,
                filter: "[EvaluationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestEvaluations_EvaluationId",
                table: "Tests",
                column: "EvaluationId",
                principalTable: "TestEvaluations",
                principalColumn: "Id");
        }
    }
}
