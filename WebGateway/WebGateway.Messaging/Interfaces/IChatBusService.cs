namespace WebGateway.Messaging.Interfaces
{
    using System.Threading.Tasks;
    using WebGateway.Models.HubsModels;

    public interface IChatBusService
    {
        Task MessageChatAPI_SaveChatConversationMessage(MessageData messageData);
    }
}
