namespace Notification.App.Hubs.HubRepos
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Notification.App.Hubs.Web;
    using Microsoft.AspNetCore.SignalR;
    using Notification.App.Hubs.Interfaces;

    public class WebEventNotification : IWebEventNotification
    {
        private readonly IHubContext<WebNotificationHub> webNotificationHub;

        public WebEventNotification(IHubContext<WebNotificationHub> webNotificationHub)
        {
            this.webNotificationHub = webNotificationHub;
        }

        public async Task PushWebEventNotification(Guid userId, string message)
        {
            var connections = WebNotificationHub.UserAndConnectionIds;

            if (connections.Count == 0) return;

            var connectionIds = connections.FirstOrDefault(x => x.Key == userId);

            if (userId == Guid.Empty) return;
            
            foreach (var id in connectionIds.Value)
            {
                await this.webNotificationHub
                    .Clients
                    .Client(id)
                    .SendAsync("ServerEventNotification", message);
            };
        }
    }
}
