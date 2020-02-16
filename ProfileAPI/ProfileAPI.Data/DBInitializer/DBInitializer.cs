namespace ProfileAPI.Data.DBInitializer
{
    using ProfileAPI.Data.Context;

    public class DBInitializer
    {
        public static void SeedDb(ProfileDBContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
