namespace Notification.App.Hubs.Web
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.SignalR;

    public class WebNotificationsHub : Hub
    {
        public static Dictionary<Guid, List<string>> UserHubConnections = new Dictionary<Guid, List<string>>();

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = this.Context.ConnectionId;
            await this.Groups.RemoveFromGroupAsync(connectionId, "user-web-event-notification");

            var userId = UserHubConnections
                .FirstOrDefault(x => x.Value.Contains(connectionId))
                .Key;

            if (UserHubConnections[userId].Count > 1)
            {
                UserHubConnections[userId].Remove(connectionId);
            }
            else
            {
                UserHubConnections.Remove(userId);
            }
        }

        public async Task SaveUserId(string userId)
        {
            var id = Guid.Parse(userId);

            if (UserHubConnections.ContainsKey(id))
            {
                UserHubConnections[id].Add(this.Context.ConnectionId);
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "user-web-event-notification");

                await Clients.Group("user-connection").SendAsync("PushNotification", $"Connection id:{this.Context.ConnectionId} and user id: {id} saved!");
            }
            else
            {
                UserHubConnections.Add(id, new List<string>());
                UserHubConnections[id].Add(this.Context.ConnectionId);

                await Clients.Group("user-connection").SendAsync("PushNotification", $"New connection id:{this.Context.ConnectionId} added to user id: {id}!");
            }
        }

        public async Task PushServerNotification(string messageToSend)
        {
            await Clients.Caller.SendAsync("PushNotification", messageToSend);
        }
    }
}
