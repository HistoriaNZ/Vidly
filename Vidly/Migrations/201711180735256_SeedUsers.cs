namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5084b930-8a86-4a25-b9a9-e197eb166f51', N'guest@vidly.com', 0, N'ACTXZFNdYU0CzRFN9eYich3qpjArmdAvHfNZihJfZ5IhyvORDjzqhoMGcML0t0D1vA==', N'58726a09-08af-473b-b754-a6c3adc707d5', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6c333c34-bb5e-4386-9ea8-9a5dcdafc538', N'admin@vidly.com', 0, N'AO0drQjYckikUdWRXffVb6ftRHi1G4uf6eSOAV7eh+uoCA9YV6/2qC5uUC+/3gS4Yw==', N'a93ef23b-308b-4081-a87b-0fb1e37a6619', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'30e50ded-d87a-472a-b1ec-574958ead577', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6c333c34-bb5e-4386-9ea8-9a5dcdafc538', N'30e50ded-d87a-472a-b1ec-574958ead577')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
