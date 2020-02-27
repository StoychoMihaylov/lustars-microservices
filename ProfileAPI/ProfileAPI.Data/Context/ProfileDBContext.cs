namespace ProfileAPI.Data.Context
{
    using Microsoft.EntityFrameworkCore;

    using ProfileAPI.Data.Entities;
    using ProfileAPI.Data.Interfaces;

    public class ProfileDBContext : DbContext, IProfileDBContext
    {
        public ProfileDBContext(DbContextOptions<ProfileDBContext> options) : base(options) { }

        public DbSet<UserProfile> UserProfiles { get; set; }

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
        }
    }
}
