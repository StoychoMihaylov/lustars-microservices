namespace ProfileAPI.Data.Context
{
    using ProfileAPI.Data.Entities;
    using ProfileAPI.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ProfileDBContext : DbContext, IProfileDBContext
    {
        public ProfileDBContext(DbContextOptions<ProfileDBContext> options) : base(options) { }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<GeoLocation> GeoLocations { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasMany(u => u.Images)
                .WithOne(i => i.UserProfile)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserProfile>()
                .HasMany(u => u.GeoLocations)
                .WithOne(g => g.UserProfile)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserProfile>()
               .HasMany(u => u.Languages)
               .WithOne(l => l.UserProfile)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
