using Microsoft.EntityFrameworkCore;

namespace BingeApi.Data
{
    public class BingeDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BingeDbContext(DbContextOptions<BingeDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

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
