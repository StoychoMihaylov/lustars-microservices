namespace ProfileAPI.Messaging.Interfaces
{
    public interface INotificationBusService
    {
        void SendMessageToNotificationAPI(string message);
    }
}
