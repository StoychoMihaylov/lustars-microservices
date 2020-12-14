namespace ChatAPI.Messaging.Consumers
{
    using MassTransit;
    using ChatAPI.Data.Entities;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using ChatAPI.Services.Interfaces;

    public class SaveChatMessageConsumer : IConsumer<ISaveChatMessage>
    {
        private readonly IMessageService messageService;

        public SaveChatMessageConsumer(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task Consume(ConsumeContext<ISaveChatMessage> context)
        {
            var message = context.Message;

            var newMessage = new ChatMessage()
            { 
                ConversationId = message.ConversationId,
                Sender = message.Sender,
                Recipient = message.Recipient,
                SendOn = message.SendOn,
                Content = message.Content
            };

            await this.messageService.CreateNewMessage(newMessage);
        }
    }
}
