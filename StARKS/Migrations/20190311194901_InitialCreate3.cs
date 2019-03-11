using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StARKS.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarkId",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarkId",
                table: "Courses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Mark",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    firstname = table.Column<string>(maxLength: 256, nullable: true),
                    lastname = table.Column<string>(maxLength: 256, nullable: true),
                    code = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mark", x => x.Id);
                });

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

            migrationBuilder.DropTable(
                name: "Mark");

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
        }
    }
}
