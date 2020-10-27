namespace AuthAPI.App.Infrastructure
{
    using GreenPipes;
    using MassTransit;
    using Message.Contract;
    using AuthAPI.Messaging.Consumers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceBusConfigExtensions
    {
        public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services)
        {
            return services.AddMassTransit(mt =>
            {
                mt.AddConsumer<RegisterNewAccountConsumer>();

                mt.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(rmq =>
                {
                    rmq.Host("rabbitmq", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    rmq.UseHealthCheck(provider);

                    rmq.ReceiveEndpoint("register-new-account", endpoint =>
                    {
                        endpoint.PrefetchCount = 6;

                        endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                        endpoint.Bind<IRegisterNewAccountMessage>();

                        endpoint.ConfigureConsumer<RegisterNewAccountConsumer>(provider);
                    });
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}
