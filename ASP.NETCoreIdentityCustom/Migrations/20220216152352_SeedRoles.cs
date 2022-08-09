using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NETCoreIdentityCustom.Migrations
{
    public partial class SeedRoles : Migration
    {
        private string WriterRoleId = Guid.NewGuid().ToString();
        private string UserRoleId = Guid.NewGuid().ToString();
        private string AdminRoleId = Guid.NewGuid().ToString();

        private string User1Id = Guid.NewGuid().ToString();
        private string User2Id = Guid.NewGuid().ToString();

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);

            SeedUser(migrationBuilder);

            SeedUserRoles(migrationBuilder);
        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{AdminRoleId}', 'Administrator', 'ADMINISTRATOR', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{WriterRoleId}', 'Manager', 'MANAGER', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{UserRoleId}', 'User', 'USER', null);");
        }

        private void SeedUser(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @$"INSERT [dbo].[AspNetUsers] ([Id],[FirstName],[LastName],[UserName],[NormalizedUserName],
[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[SecurityStamp],[ConcurrencyStamp],
[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnd],[LockoutEnabled] ,[AccessFailedCount])
VALUES 
(N'{User1Id}', N'Test',N'LastName', N'test22@test.ca', N'Test22@TEST.CA',
N'test22@test.ca', N'Test22@TEST.CA',0,
N'AQAAAAEAACcQAAAAEHV7fjvJHByc8JOtFMcVvJ1hNArI8w7VGk64pHIQy94VZudJzHtvvD2y6hkpcpefOw==',
N'IYXYUQ5O3SN6SN5Z3FSGVXYU364H2L2G', N'6534a119-4ef4-4871-8252-0556de76dbe7', Null,0,0,Null,1,0)");
            migrationBuilder.Sql(
                 @$"INSERT [dbo].[AspNetUsers] ([Id],[FirstName],[LastName],[UserName],[NormalizedUserName],
[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[SecurityStamp],[ConcurrencyStamp],
[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnd],[LockoutEnabled] ,[AccessFailedCount])
VALUES 
(N'{User2Id}', N'Test 3',N'LastName', N'test33@test.ca', N'Test33@TEST.CA',
N'test33@test.ca', N'Test33@TEST.CA',0,
N'AQAAAAEAACcQAAAAEHV7fjvJHByc8JOtFMcVvJ1hNArI8w7VGk64pHIQy94VZudJzHtvvD2y6hkpcpefOw==',
N'IYXYUQ5O3SN6SN5Z3FSGVXYU364H2L2G', N'6534a119-4ef4-4871-8252-0556de76dbe7', Null,0,0,Null,1,0)");
        }

        private void SeedUserRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"
        INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
        VALUES
           ('{User1Id}', '{WriterRoleId}');
        INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
        VALUES
           ('{User1Id}', '{UserRoleId}');");

            migrationBuilder.Sql(@$"
        INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
        VALUES
           ('{User2Id}', '{AdminRoleId}');
        INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
        VALUES
           ('{User2Id}', '{WriterRoleId}');");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
