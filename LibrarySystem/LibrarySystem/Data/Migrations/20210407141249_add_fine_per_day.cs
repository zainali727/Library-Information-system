using Microsoft.EntityFrameworkCore.Migrations;

namespace LibrarySystem.Data.Migrations
{
    public partial class add_fine_per_day : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinePerDay",
                table: "Books",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinePerDay",
                table: "Books");
        }
    }
}
