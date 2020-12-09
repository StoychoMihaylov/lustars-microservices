namespace WebGateway.App.Hubs.Web
{
    using System;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using WebGateway.Models.HubsModels;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
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

            // Send the message to the ChatAPI to save it in the DB
        }
    }
}
