using Microsoft.EntityFrameworkCore;
namespace br_H60L02.Models
{
    public class MovieContext: DbContext
    {


        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set; }

        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre() { GenreId = 1, GenreName = "Horror"},
                new Genre() { GenreId = 2, GenreName = "Comedy" },
                new Genre() { GenreId = 3, GenreName = "Romance" },
                new Genre() { GenreId = 4, GenreName = "SitCom" },
                new Genre() { GenreId = 5, GenreName = "Action" }
             );
            modelBuilder.Entity<Movie>().HasData(
                new Movie() { MovieId = 1, Title = "Deadpool", Rating = 99.0m },
                new Movie() { MovieId = 2, Title = "Deadpool 2", Rating = 99.0m },
                new Movie() { MovieId = 3, Title = "Deadpool 3", Rating = 99.0m },
                new Movie() { MovieId = 4, Title = "Fast and Furious", Rating = 99.0m },
                new Movie() { MovieId = 5, Title = "Fast and Furious 2", Rating = 99.0m },
                new Movie() { MovieId = 6, Title = "Fast and Furious 3", Rating = 99.0m },
                new Movie() { MovieId = 7, Title = "Fast and Furious 4", Rating = 99.0m },
                new Movie() { MovieId = 8, Title = "Fast and Furious 5", Rating = 99.0m },
                new Movie() { MovieId = 9, Title = "Fast and Furious 6", Rating = 99.0m },
                new Movie() { MovieId = 10, Title = "Fast and Furious 7", Rating = 99.0m },
                new Movie() { MovieId = 11, Title = "Fast and Furious 8", Rating = 99.0m },
                new Movie() { MovieId = 12, Title = "Fast and Furious 9", Rating = 99.0m }
            );
        }
    }
}
