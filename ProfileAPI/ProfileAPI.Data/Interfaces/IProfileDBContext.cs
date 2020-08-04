namespace ProfileAPI.Data.Interfaces
{
    using ProfileAPI.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IProfileDBContext
    {
        DbSet<UserProfile> UserProfiles { get; set; }

        DbSet<Language> Languages { get; set; }

        DbSet<GeoLocation> GeoLocations { get; set; }

        DbSet<Image> Images { get; set; }

        DbSet<Like> Likes { get; set; }

        int SaveChanges();
    }
}
