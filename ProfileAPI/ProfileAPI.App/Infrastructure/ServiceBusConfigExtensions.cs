namespace ProfileAPI.App.Infrastructure
{
    using GreenPipes;
    using MassTransit;
    using System.Diagnostics;
    using MassTransit.MessageData;
    using MessageExchangeContract;
    using ProfileAPI.Messaging.Consumers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceBusConfigExtensions
    {
        public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services)
        {
            return services.AddMassTransit(mt =>
            {
                // Register Consumers
                mt.AddConsumer<CreateUserProfileConsumer>();
                mt.AddConsumer<UpdateUserProfileConsumer>();

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

                    // Register Exhanges
                    rmq.Message<ICreateUserProfile>(m => m.SetEntityName("create-user-profile-exchange"));
                    rmq.Message<IUpdateUserProfile>(m => m.SetEntityName("update-user-profile-exchange"));
                    rmq.Message<IEventNotificationMessage>(m => m.SetEntityName("event-notification-exchange"));

                    // Register Endpoints
                    rmq.ReceiveEndpoint("create-user-profile-queue", endpoint =>
                    {
                        endpoint.PrefetchCount = 20;
                        endpoint.UseMessageRetry(retry => retry.Interval(5, 200));
                        endpoint.Bind<ICreateUserProfile>();
                        endpoint.ConfigureConsumer<CreateUserProfileConsumer>(provider);
                    });

                    rmq.ReceiveEndpoint("update-user-profile-queue", endpoint =>
                    {
                        endpoint.PrefetchCount = 20;
                        endpoint.UseMessageRetry(retry => retry.Interval(5, 200));
                        endpoint.Bind<IUpdateUserProfile>();
                        endpoint.ConfigureConsumer<UpdateUserProfileConsumer>(provider);
                    });
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}
