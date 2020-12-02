namespace WebGateway.App.Hubs.Web
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public async Task OpenChatConversation(string usersData) 
        {
            // Save the IDs of the users and their connectionIDs (save connection data in memory)
            // Create a group for them
            // If one of the users reconect change his connectionID
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
           // Check which user is disconected and delete its ID and connectionId
           // If both users are disconnected delete the connection data from memory
        }

        public async Task SendMessageToTheHub(string message)
        {
            // Receives message from React and sends the message to the collocutor
            // Sends RabbitMQ message to the ChatService to save the message in MongoDB
        }
    }
}
