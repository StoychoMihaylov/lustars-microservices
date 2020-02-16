﻿namespace ProfileAPI.Data.Interfaces
{
    using Microsoft.EntityFrameworkCore;

    using ProfileAPI.Data.Entities;

    public interface IProfileDBContext
    {
        DbSet<UserProfile> UserProfiles { get; set; }

        DbSet<Image> Images { get; set; }

        int SaveChanges();
    }
}
