using Microsoft.EntityFrameworkCore.Migrations;

namespace LibrarySystem.Data.Migrations
{
    public partial class address3_not_required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AddressLine3",
                table: "Members",
                type: "nvarchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AddressLine3",
                table: "Members",
                type: "nvarchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldNullable: true);
        }
    }
}
