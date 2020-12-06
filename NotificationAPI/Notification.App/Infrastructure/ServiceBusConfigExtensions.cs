namespace Notification.App.Infrastructure
{
    using GreenPipes;
    using MassTransit;
    using System.Diagnostics;
    using MessageExchangeContract;
    using Notification.App.MessageBus.Consumers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceBusConfigExtensions
    {
        public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services)
        {
            return services.AddMassTransit(mt =>
            {
                // Register Consumers
                mt.AddConsumer<EventNotificationsConsumer>();


                mt.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(rmq =>
                {
                    if (Debugger.IsAttached)
                    {
                        rmq.Host("localhost", "/", host =>
                        {
                            host.Username("guest");
                            host.Password("guest");
                        });
                    }
                    else
                    {
                        rmq.Host("rabbitmq", host =>
                        {
                            host.Username("guest");
                            host.Password("guest");
                        });
                    }

                    rmq.UseHealthCheck(provider);

                    // Register Exhanges
                    rmq.Message<IEventNotificationMessage>(m => m.SetEntityName("event-notification-exchange"));

                    // Register Endpoints
                    rmq.ReceiveEndpoint("event-notification-queue", endpoint =>
                    {
                        endpoint.PrefetchCount = 20;
                        endpoint.UseMessageRetry(retry => retry.Interval(5, 200));
                        endpoint.Bind<IEventNotificationMessage>();
                        endpoint.ConfigureConsumer<EventNotificationsConsumer>(provider);
                    });
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}
