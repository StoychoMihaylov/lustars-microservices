namespace AuthAPI.Data.Interfaces
{   
    using Microsoft.EntityFrameworkCore;

    using AuthAPI.Data.Entities;

    public interface IAuthDBContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<TokenManager> Tokens { get; set; }

        int SaveChanges();
    }
}
