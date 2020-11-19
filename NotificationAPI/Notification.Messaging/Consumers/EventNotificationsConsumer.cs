namespace Notification.Messaging.Consumers
{
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using Notification.App.Hubs.Interfaces;

    public class EventNotificationsConsumer : IConsumer<IEventNotificationMessage>
    {
        private readonly IWebEventNotification webEventNotification;

        public EventNotificationsConsumer(IWebEventNotification webEventNotification)
        {
            this.webEventNotification = webEventNotification;
        }

        public async Task Consume(ConsumeContext<IEventNotificationMessage> context)
        {
            var message = context.Message;

            if (message.MessageType.Equals(MessageType.Info))
            {
                await this.webEventNotification.PushWebEventNotification(userId, message);
            }
            else if (message.MessageType.Equals(MessageType.Success))
            {

            }
            else if (message.MessageType.Equals(MessageType.Error))
            {

            }
        }
    }
}
