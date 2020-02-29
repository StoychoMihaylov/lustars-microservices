namespace AuthAPI.Data.DBInitializer
{
    using Microsoft.EntityFrameworkCore;

    using AuthAPI.Data.Context;

    public class DBInitializer
    {
        public static void SeedDb(AuthDBContext context)
        {
            context.Database.Migrate();
        }
    }
}
