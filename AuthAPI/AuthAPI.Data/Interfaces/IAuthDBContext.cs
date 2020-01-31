namespace AuthAPI.Data.Interfaces
{
    using AuthAPI.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IAuthDBContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<TokenManager> Tokens { get; set; }

        int SaveChanges();
    }
}
