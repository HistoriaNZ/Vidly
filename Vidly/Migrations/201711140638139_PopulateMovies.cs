namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovies : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Movies ON");
            Sql("INSERT INTO Movies (Id, Name, GenreId, ReleaseDate," +
                "DateAdded, NumberInStock) VALUES (1, 'The Hangover', 5, '20090605', '20171114', 5)");
            Sql("INSERT INTO Movies (Id, Name, GenreId, ReleaseDate," +
                "DateAdded, NumberInStock) VALUES (2, 'Die Hard', 1, '19880712', '20171114', 3)");
            Sql("INSERT INTO Movies (Id, Name, GenreId, ReleaseDate," +
                "DateAdded, NumberInStock) VALUES (3, 'The Terminator', 1, '19841026', '20171114', 6)");
            Sql("INSERT INTO Movies (Id, Name, GenreId, ReleaseDate," +
                "DateAdded, NumberInStock) VALUES (4, 'Toy Story', 3, '19951122', '20171114', 10)");
            Sql("INSERT INTO Movies (Id, Name, GenreId, ReleaseDate," +
                "DateAdded, NumberInStock) VALUES (5, 'Titanic', 4, '19971219', '20171114', 2)");
            Sql("SET IDENTITY_INSERT Movies OFF");
        }
        
        public override void Down()
        {
        }
    }
}
