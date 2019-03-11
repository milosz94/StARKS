using Microsoft.EntityFrameworkCore.Migrations;

namespace StARKS.Migrations
{
    public partial class InitialCreate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Students_StudentId",
                table: "Mark");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Courses_code",
                table: "Mark");

            migrationBuilder.DropIndex(
                name: "IX_Mark_StudentId",
                table: "Mark");

            migrationBuilder.DropIndex(
                name: "IX_Mark_code",
                table: "Mark");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Mark");

            migrationBuilder.DropColumn(
                name: "code",
                table: "Mark");

            migrationBuilder.AddColumn<int>(
                name: "MarkId",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarkId",
                table: "Courses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_MarkId",
                table: "Students",
                column: "MarkId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MarkId",
                table: "Courses",
                column: "MarkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Mark_MarkId",
                table: "Courses",
                column: "MarkId",
                principalTable: "Mark",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Mark_MarkId",
                table: "Students",
                column: "MarkId",
                principalTable: "Mark",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Mark_MarkId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Mark_MarkId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_MarkId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Courses_MarkId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MarkId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MarkId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Mark",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "code",
                table: "Mark",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mark_StudentId",
                table: "Mark",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_code",
                table: "Mark",
                column: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Students_StudentId",
                table: "Mark",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Courses_code",
                table: "Mark",
                column: "code",
                principalTable: "Courses",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
