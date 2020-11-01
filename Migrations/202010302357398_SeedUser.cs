namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'203b0733-4463-468e-a94b-85f1981d4ae8', N'admin@vidly.com', 0, N'AGxobyYJRgU3mhY0SXk/Eqlo18guC1Us8IqVUVPedtIf0nrqNTyjYAF/GGYupLFWEA==', N'68ba113f-e238-48e5-a4e1-d63ecfcfa586', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd729a9de-9afd-437a-a22c-06d69aab7721', N'guest@vidly.com', 0, N'AJ+QYPw6G1yjX5QozmfNyZmz1vFFLlL0nOPIado7uYVa54PEWy/4z91eFLbWuUG+8Q==', N'1b1b2928-178e-4807-97fd-653c0080a19c', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c3829193-c01f-497f-a33c-f1f89cb38f18', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'203b0733-4463-468e-a94b-85f1981d4ae8', N'c3829193-c01f-497f-a33c-f1f89cb38f18')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
