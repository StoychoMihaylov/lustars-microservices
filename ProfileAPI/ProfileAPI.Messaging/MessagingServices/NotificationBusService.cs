namespace ProfileAPI.Messaging.MessagingServices
{
    using System;
    using MassTransit;
    using MessageExchangeContract;
    using ProfileAPI.Messaging.Interfaces;

    public class NotificationBusService : INotificationBusService
    {
        private readonly IBus bus;

        public NotificationBusService(IBus bus)
        {
            this.bus = bus;
        }

        public async void SendMessageToNotificationAPI(string message)
        {
            var endpoint = await this.bus.GetSendEndpoint(new Uri("queue:event-notification-queue"));
            await endpoint.Send<EventNotificationMessage>(new 
            { 
                MessageType = MessageType.Success,
                Message = message 
            });
        }
    }
}
