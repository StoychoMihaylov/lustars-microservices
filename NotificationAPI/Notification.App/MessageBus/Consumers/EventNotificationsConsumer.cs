namespace Notification.App.MessageBus.Consumers
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

            System.Console.WriteLine(message.UserId);
            System.Console.WriteLine(message.MessageType);
            System.Console.WriteLine(message.Message);

            if (message.MessageType == (int)MessageType.Info)
            {
                
            }
            else if (message.MessageType == (int)MessageType.Success)
            {
                await this.webEventNotification.PushWebEventNotification(message.UserId, message.Message);
            }
            else if (message.MessageType == (int)MessageType.Error)
            {

            }
        }
    }
}
