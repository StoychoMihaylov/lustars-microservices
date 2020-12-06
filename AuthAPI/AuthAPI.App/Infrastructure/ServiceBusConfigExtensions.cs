namespace AuthAPI.App.Infrastructure
{
    using GreenPipes;
    using MassTransit;
    using System.Diagnostics;
    using MessageExchangeContract;
    using AuthAPI.Messaging.Consumers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceBusConfigExtensions
    {
        public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services)
        {
            return services.AddMassTransit(mt =>
            {
                mt.AddConsumer<RegisterAccountConsumer>();
                mt.AddConsumer<DeleteAccountConsumer>();

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

                    // Register account
                    rmq.Message<IRegisterAccountProfile>(m => m.SetEntityName("register-account-profile-exchange"));

                    rmq.ReceiveEndpoint("register-account-profile-queue", endpoint =>
                    {
                        endpoint.PrefetchCount = 20;

                        endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                        endpoint.Bind<IRegisterAccountProfile>();

                        endpoint.ConfigureConsumer<RegisterAccountConsumer>(provider);
                    });

                    // Delete account
                    rmq.Message<IDeleteAccountProfile>(m => m.SetEntityName("delete-account-profile-exchange"));

                    rmq.ReceiveEndpoint("delete-account-profile-queue", endpoint =>
                    {
                        endpoint.PrefetchCount = 20;

                        endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                        endpoint.Bind<IDeleteAccountProfile>();

                        endpoint.ConfigureConsumer<DeleteAccountConsumer>(provider);
                    });
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}
