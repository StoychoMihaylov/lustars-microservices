namespace ProfileAPI.Messaging.Interfaces
{
    using System;

    public interface INotificationBusService
    {
        void SendMessageToNotificationAPI(Guid userId, string message);
    }
}
