#nullable disable

namespace TestPlatform.Database.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveNestedComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestComments_TestComments_ParentCommentId",
                table: "TestComments");

            migrationBuilder.DropIndex(
                name: "IX_TestComments_ParentCommentId",
                table: "TestComments");

            migrationBuilder.DropColumn(
                name: "ParentCommentId",
                table: "TestComments");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "TestComments",
                type: "nvarchar(max)",
                maxLength: 8192,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "TestComments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 8192);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCommentId",
                table: "TestComments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestComments_ParentCommentId",
                table: "TestComments",
                column: "ParentCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestComments_TestComments_ParentCommentId",
                table: "TestComments",
                column: "ParentCommentId",
                principalTable: "TestComments",
                principalColumn: "Id");
        }
    }
}
