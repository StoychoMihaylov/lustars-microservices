﻿namespace ProfileAPI.App.Infrastructure
{
    using GreenPipes;
    using MassTransit;
    using MessageExchangeContract;
    using ProfileAPI.Messaging.Consumers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceBusConfigExtensions
    {
        public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services)
        {
            return services.AddMassTransit(mt =>
            {
                mt.AddConsumer<CreateNewUserProfileConsumer>();

                mt.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(rmq =>
                {
                    rmq.Host("rabbitmq", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    rmq.UseHealthCheck(provider);

                    rmq.Message<ICreateNewUserProfile>(m => m.SetEntityName("create-user-profile-exchange"));

                    rmq.ReceiveEndpoint("create-new-user-profile-queue", endpoint =>
                    {
                        endpoint.PrefetchCount = 20;

                        endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                        endpoint.Bind<ICreateNewUserProfile>();

                        endpoint.ConfigureConsumer<CreateNewUserProfileConsumer>(provider);
                    });
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}