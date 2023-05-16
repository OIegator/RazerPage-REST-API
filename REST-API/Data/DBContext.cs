using Microsoft.EntityFrameworkCore;
using REST_API.Models;

namespace REST_API.Data
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<GamePlatform> GamePlatform { get; set; }
        public DbSet<GamePublisher> GamePublishers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionSales> RegionSales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("game");
            modelBuilder.Entity<GamePlatform>().ToTable("game_platform");
            modelBuilder.Entity<GamePublisher>().ToTable("game_publisher");
            modelBuilder.Entity<Genre>().ToTable("genre");
            modelBuilder.Entity<Platform>().ToTable("platform");
            modelBuilder.Entity<Publisher>().ToTable("publisher");
            modelBuilder.Entity<Region>().ToTable("region");
            modelBuilder.Entity<RegionSales>().ToTable("region_sales");
        }
    }
}
