namespace Notification.App.Hubs.Web
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.SignalR;

    public class WebNotificationHub : Hub
    {
        public static Dictionary<Guid, List<string>> UserAndConnectionIds = new Dictionary<Guid, List<string>>();

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = this.Context.ConnectionId;
            await this.Groups.RemoveFromGroupAsync(connectionId, "user-web-event-notification");

            RemoveFromUserAndConnectionIds(connectionId);
        }

        private void RemoveFromUserAndConnectionIds(string connectionId)
        {
            var userId = UserAndConnectionIds
                .FirstOrDefault(x => x.Value.Contains(connectionId))
                .Key;

            if (UserAndConnectionIds[userId].Count > 1)
            {
                UserAndConnectionIds[userId].Remove(connectionId);
            }
            else
            {
                UserAndConnectionIds.Remove(userId);
            }
        }

        public async Task SaveUserId(string userId)
        {
            var id = Guid.Parse(userId);
            var conectionId = this.Context.ConnectionId;

            if (UserAndConnectionIds.ContainsKey(id))
            {
                UserAndConnectionIds[id].Add(conectionId);
                await this.Groups.AddToGroupAsync(conectionId, "user-web-event-notification");
            }
            else
            {
                UserAndConnectionIds.Add(id, new List<string>());
                UserAndConnectionIds[id].Add(this.Context.ConnectionId);
            }
        }

        public async Task PushWebEventNotification(Guid userId, string message)
        {
            var connectionIds = UserAndConnectionIds.FirstOrDefault(x => x.Key == userId).Value;

            foreach (var id in connectionIds)
            {
                await this.Clients.Client(id).SendAsync("user-web-event-notification", message);
            }
        }
    }
}
