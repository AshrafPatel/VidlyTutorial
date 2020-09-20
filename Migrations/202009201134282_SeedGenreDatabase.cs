namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedGenreDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) Values (1, 'Comedy')");
            Sql("INSERT INTO Genres (Id, Name) Values (2, 'Action')");
            Sql("INSERT INTO Genres (Id, Name) Values (3, 'Family')");
            Sql("INSERT INTO Genres (Id, Name) Values (4, 'Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
