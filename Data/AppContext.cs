using Microsoft.EntityFrameworkCore;
using Smart_Utube.Models;

namespace Smart_Utube.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistMovie> PlaylistMovies { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }
        public DbSet<ExternalDescription> ExternalDescriptions { get; set; }
        public DbSet<Profile> Profiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User enum
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            // Playlist movie

            modelBuilder.Entity<PlaylistMovie>()
                .HasKey(x => new { x.PlaylistId, x.MovieId });

            modelBuilder.Entity<PlaylistMovie>()
                .HasOne(pm => pm.Playlist)
                .WithMany(p => p.PlaylistMovies)
                .HasForeignKey(pm => pm.PlaylistId);

            modelBuilder.Entity<PlaylistMovie>()
                .HasOne(pm => pm.Movie)
                .WithMany(m => m.PlaylistMovies)
                .HasForeignKey(pm => pm.MovieId);

            //Movie category

            modelBuilder.Entity<MovieCategory>()
                .HasKey(x => new { x.MovieId, x.CategoryId });

            modelBuilder.Entity<MovieCategory>()
                .HasOne(mc => mc.Movie)
                .WithMany(m => m.MovieCategories)
                .HasForeignKey(mc => mc.MovieId);

            modelBuilder.Entity<MovieCategory>()
                .HasOne(mc => mc.Category)
                .WithMany(c => c.MovieCategories)
                .HasForeignKey(mc => mc.CategoryId);

            // Favourite movies

            modelBuilder.Entity<Favorite>()
                .HasKey(x => new { x.UserId, x.MovieId});

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Movie)
                .WithMany(m => m.Favorites)
                .HasForeignKey(f => f.MovieId);

            // Comment one-to-many
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // Rating
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Ratings)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rating>()
                .HasIndex(r => new { r.UserId, r.MovieId })
                .IsUnique();

            // Watch history
            modelBuilder.Entity<WatchHistory>()
                .HasOne(w => w.User)
                .WithMany(u => u.WatchHistories)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchHistory>()
                .HasOne(w => w.Movie)
                .WithMany(m => m.WatchHistories)
                .HasForeignKey(w => w.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // Profile
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // AI description
            modelBuilder.Entity<ExternalDescription>()
                .HasOne(ed => ed.Movie)
                .WithMany(m => m.ExternalDescriptions)
                .HasForeignKey(ed => ed.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}