namespace WebGateway.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IChatMessangerService
    {
        Task<string> CallProfileAPI_CreateConversationIfUsersLikeEachOther(Guid currentUserId, Guid secondUserId);
        Task<string> CallProfileAPI_GetAllChatConversationForUserById(Guid currentUserId);
        Task<string> CallChatAPI_GetAllConversationMessages(Guid currentUserId, Guid conversationId);
    }
}
