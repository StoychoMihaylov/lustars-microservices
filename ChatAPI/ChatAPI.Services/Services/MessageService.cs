namespace ChatAPI.Services.Services
{
    using System;
    using System.Linq;
    using ChatAPI.Data.Entities;
    using System.Threading.Tasks;
    using ChatAPI.Data.Interfaces;
    using System.Collections.Generic;
    using ChatAPI.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

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

        public async Task<ICollection<ChatMessage>> GetAllConversationMessages(Guid userId, Guid conversationId)
        {
            var messages = await this.Context.ChatMessages
                .Where(m => m.ConversationId == conversationId)
                .OrderBy(m => m.SendOn)
                .ToListAsync();

            // check if the same user participates in requested messages(security check)
            if (messages.Count > 0 && (messages.First().Sender.Equals(userId) || messages.First().Recipient.Equals(userId)))
            {
                return messages;
            }

            return null;
        }
    }
}
