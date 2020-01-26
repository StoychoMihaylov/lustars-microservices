namespace AuthAPI.Data.Context
{
    using AuthAPI.Data.Entities;
    using AuthAPI.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class AuthDBContext : DbContext, IAuthDBContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<TokenManager> Tokens { get; set; }
    }
}
