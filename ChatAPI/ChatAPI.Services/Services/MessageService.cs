namespace ChatAPI.Services.Services
{
    using ChatAPI.Data.Entities;
    using ChatAPI.Data.Interfaces;
    using ChatAPI.Services.Interfaces;
    using System.Threading.Tasks;

    public class MessageService : Service, IMessageService
    {
        public MessageService(IChatDBContext context) : 
            base(context)
        { }

        public async Task CreateNewMessage(ChatMessage message)
        {
            await this.Context.ChatMessages.AddAsync(message);
            await this.Context.SaveChangesAsync();
        }
    }
}
