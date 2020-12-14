namespace ChatAPI.Services.Interfaces
{
    using System;
    using ChatAPI.Data.Entities;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IMessageService
    {
        Task CreateNewMessage(ChatMessage message);
        Task<ICollection<ChatMessage>> GetAllConversationMessages(Guid userId, Guid conversationId);
    }
}
