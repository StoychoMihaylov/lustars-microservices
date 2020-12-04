namespace ChatAPI.Data.Context
{
    using ChatAPI.Data.Entities;
    using ChatAPI.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ChatDBContext : DbContext, IChatDBContext
    {
        public ChatDBContext(DbContextOptions<ChatDBContext> options) : base(options) { }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
