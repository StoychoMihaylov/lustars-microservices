namespace ProfileAPI.Data.Context
{
    using Microsoft.EntityFrameworkCore;

    using ProfileAPI.Data.Interfaces;

    public class ProfileDBContext : DbContext, IProfileDBContext
    {
        public ProfileDBContext(DbContextOptions<ProfileDBContext> options) : base(options) { }
    }
}
