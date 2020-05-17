namespace ProfileAPI.Data.Interfaces
{
    using Microsoft.EntityFrameworkCore;

    using ProfileAPI.Data.Entities;

    public interface IProfileDBContext
    {
        DbSet<UserProfile> UserProfiles { get; set; }

        DbSet<Language> Languages { get; set; }

        DbSet<GeoLocation> GeoLocations { get; set; }

        DbSet<Image> Images { get; set; }

        int SaveChanges();
    }
}
