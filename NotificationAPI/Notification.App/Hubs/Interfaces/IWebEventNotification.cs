namespace Notification.App.Hubs.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IWebEventNotification
    {
        Task PushWebEventNotification(Guid userId, string message);
    }
}
