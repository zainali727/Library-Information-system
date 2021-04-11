using Microsoft.EntityFrameworkCore.Migrations;

namespace LibrarySystem.Data.Migrations
{
    public partial class add_admin_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO LibrarySystem.dbo.AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES (N'9a6ed935-5edc-4c01-b2e3-2575479bf6b3', N'admin@admin.com', N'ADMIN@ADMIN.COM', N'admin@admin.com', N'ADMIN@ADMIN.COM', 0, N'AQAAAAEAACcQAAAAEGChw/ijZiL+NAP4SEwwFbtV7xz+9glblxdVc068OsuyXGmNrFdtZDwmIP3HnRqBLQ==', N'AS3ELUM3N7XLXTDVKF2PL27MC7S2GAZS', N'8dd2acde-2a4b-49a7-996b-f4e8e6d46450', null, 0, 0, null, 1, 0);");
            migrationBuilder.Sql("INSERT INTO LibrarySystem.dbo.AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES (N'8fbe64a7-d9ce-4380-a31a-42ea8babd7bc', N'manager@manager.com', N'MANAGER@MANAGER.COM', N'manager@manager.com', N'MANAGER@MANAGER.COM', 0, N'AQAAAAEAACcQAAAAEGC6RWu5o+6D7btnyTzChMDXSn4a5xSsP3X5YlotALr0EPlaZDfGUnIpG4J6O4i/QQ==', N'AOQAOMKTOZQCXTLWI3YQPBSXF7GNHEA5', N'e0b13808-3f75-4d1f-ba52-e4907796fb7f', null, 0, 0, null, 1, 0);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM LibrarySystem.dbo.AspNetUsers where UserName = N'admin@admin.com'");
            migrationBuilder.Sql("DELETE FROM LibrarySystem.dbo.AspNetUsers where UserName = N'manager@manager.com'");
        }
    }
}
