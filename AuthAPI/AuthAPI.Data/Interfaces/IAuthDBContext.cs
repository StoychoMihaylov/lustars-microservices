namespace AuthAPI.Data.Interfaces
{
    using AuthAPI.Data.Entities;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;

    public interface IAuthDBContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<TokenManager> Tokens { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
