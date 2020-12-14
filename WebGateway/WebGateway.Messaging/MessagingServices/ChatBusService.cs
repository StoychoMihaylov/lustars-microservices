namespace WebGateway.Messaging.MessagingServices
{
    using System;
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using WebGateway.Models.HubsModels;
    using WebGateway.Messaging.Interfaces;

    public class ChatBusService : IChatBusService
    {
        private readonly IBus bus;

        public ChatBusService(IBus bus)
        {
            this.bus = bus;
        }

        public async Task MessageChatAPI_SaveChatConversationMessage(MessageData messageData)
        {
            var endpoint = await this.bus.GetSendEndpoint(new Uri("queue:save-new-chat-message-queue"));
            await endpoint.Send<ISaveChatMessage>(messageData);
        }
    }
}
