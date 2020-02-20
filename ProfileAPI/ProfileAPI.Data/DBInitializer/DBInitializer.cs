namespace ProfileAPI.Data.DBInitializer
{
    using ProfileAPI.Data.Context;
    using Microsoft.EntityFrameworkCore;

    public class DBInitializer
    {
        public static void SeedDb(ProfileDBContext context)
        {
            //context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}
