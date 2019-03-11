using Microsoft.EntityFrameworkCore.Migrations;

namespace StARKS.Migrations
{
    public partial class InitialCreate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "firstname",
                table: "Mark");

            migrationBuilder.DropColumn(
                name: "lastname",
                table: "Mark");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Mark");

            migrationBuilder.AddColumn<int>(
                name: "mark",
                table: "Mark",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mark",
                table: "Mark");

            migrationBuilder.AddColumn<string>(
                name: "firstname",
                table: "Mark",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastname",
                table: "Mark",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Mark",
                maxLength: 100,
                nullable: true);
        }
    }
}
