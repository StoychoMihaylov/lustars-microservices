namespace Notification.App.Hubs.Web
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class WebNotificationsHub : Hub
    {
        public async Task PushServerNotification()
        {
            await Clients.Caller.SendAsync("PushNotification", "TEST");
        }
    }
}
