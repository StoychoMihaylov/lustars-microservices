namespace ChatAPI.App.Infrastructure
{
    using GreenPipes;
    using MassTransit;
    using System.Diagnostics;
    using MessageExchangeContract;
    using MassTransit.MessageData;
    using ChatAPI.Messaging.Consumers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceBusConfigurationExtensions
    {
        public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services)
        {
            return services.AddMassTransit(mt =>
            {
                // Register Consumers
                mt.AddConsumer<SaveChatMessageConsumer>();

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
                    rmq.UseMessageData(new InMemoryMessageDataRepository());

                    // Register Exchanges
                    rmq.Message<ISaveChatMessage>(m => m.SetEntityName("save-new-chat-message-exchange"));

                    // Register Endpoints
                    rmq.ReceiveEndpoint("save-new-chat-message-queue", endpoint =>
                    {
                        endpoint.PrefetchCount = 20;
                        endpoint.UseMessageRetry(retry => retry.Interval(5, 200));
                        endpoint.Bind<ISaveChatMessage>();
                        endpoint.ConfigureConsumer<SaveChatMessageConsumer>(provider);
                    });
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}
