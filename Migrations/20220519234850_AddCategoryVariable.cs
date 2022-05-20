using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodFinderWebApp.Migrations
{
    public partial class AddCategoryVariable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "SavedFoodLocation",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "SavedFoodLocation");
        }
    }
}
