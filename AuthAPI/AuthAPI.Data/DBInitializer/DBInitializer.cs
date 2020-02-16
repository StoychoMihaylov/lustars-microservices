namespace AuthAPI.Data.DBInitializer
{
    using AuthAPI.Data.Context;

    public class DBInitializer
    {
        public static void SeedDb(AuthDBContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
