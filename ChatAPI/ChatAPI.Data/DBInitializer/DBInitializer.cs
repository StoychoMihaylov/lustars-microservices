namespace ChatAPI.Data.DBInitializer
{
    using ChatAPI.Data.Context;
    using Microsoft.EntityFrameworkCore;

    public class DBInitializer
    {
        public static void SeedDb(ChatDBContext context)
        {
            //context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}
