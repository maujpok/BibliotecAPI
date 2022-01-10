using Microsoft.EntityFrameworkCore.Migrations;

namespace BibliotecAPI.Migrations
{
    public partial class propBirthCityAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BirthCity",
                table: "Author",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthCity",
                table: "Author");
        }
    }
}
