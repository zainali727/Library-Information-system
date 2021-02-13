using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Data.Migrations
{
    public partial class add_admin_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO LibrarySystem.dbo.AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES (N'9a6ed935-5edc-4c01-b2e3-2575479bf6b3', N'admin@admin.com', N'ADMIN@ADMIN.COM', N'admin@admin.com', N'ADMIN@ADMIN.COM', 0, N'AQAAAAEAACcQAAAAEGChw/ijZiL+NAP4SEwwFbtV7xz+9glblxdVc068OsuyXGmNrFdtZDwmIP3HnRqBLQ==', N'AS3ELUM3N7XLXTDVKF2PL27MC7S2GAZS', N'8dd2acde-2a4b-49a7-996b-f4e8e6d46450', null, 0, 0, null, 1, 0);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM LibrarySystem.dbo.AspNetUsers where UserName = N'admin@admin.com'");
        }
    }
}
