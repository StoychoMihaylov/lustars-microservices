namespace WebGateway.App.Hubs.Web
{
    using System;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using WebGateway.Models.HubsModels;
    using Microsoft.AspNetCore.SignalR;
    using WebGateway.Messaging.Interfaces;

    public class ChatHub : Hub
    {
        private readonly IChatBusService chatBusService;

        public ChatHub(IChatBusService chatBusService)
        {
            this.chatBusService = chatBusService;
        }

        public async Task OpenChatConversation(string id) 
        {
            var conversationId = Guid.Parse(id);
            var conectionId = this.Context.ConnectionId;

            await this.Groups.AddToGroupAsync(conectionId, conversationId.ToString());  
        }

        public async Task SendMessageToTheHub(string messageDataJSON)
        {
            var messageData = JsonConvert.DeserializeObject<MessageData>(messageDataJSON);
            messageData.SendOn = DateTime.UtcNow;
            await Clients.Group(messageData.ConversationId.ToString()).SendAsync("ReceiveMessage", messageData);

            await this.chatBusService.MessageChatAPI_SaveChatConversationMessage(messageData);
        }
    }
}
