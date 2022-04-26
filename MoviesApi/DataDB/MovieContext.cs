using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.DataDB
{
    public class MovieContext :DbContext
    {
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Director>? Directors { get; set; }
        public DbSet<Actor>? Actors { get; set; }
        public DbSet<Genre>? Genres { get; set; }


        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfiguration(new ActorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DirectorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GenreEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MovieEntityTypeConfiguration());
        }
    }
}
