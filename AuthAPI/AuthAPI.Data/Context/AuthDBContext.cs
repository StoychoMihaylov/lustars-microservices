namespace AuthAPI.Data.Context
{
    using AuthAPI.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AuthDBContext : DbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<TokenManager> Tokens { get; set; }
    }
}
