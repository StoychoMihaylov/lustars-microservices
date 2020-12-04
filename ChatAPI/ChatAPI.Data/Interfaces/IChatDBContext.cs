namespace ChatAPI.Data.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using ChatAPI.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IChatDBContext
    {
        DbSet<ChatMessage> ChatMessages { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
