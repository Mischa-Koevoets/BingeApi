using Microsoft.EntityFrameworkCore;

namespace BingeApi.Data
{
    public class BingeDbContext : DbContext
    {
        public BingeDbContext(DbContextOptions<BingeDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<SeriesGenre> SeriesGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.Entity<SeriesGenre>().HasKey(sg => new { sg.SeriesId, sg.GenreId });
        }
    }
}
