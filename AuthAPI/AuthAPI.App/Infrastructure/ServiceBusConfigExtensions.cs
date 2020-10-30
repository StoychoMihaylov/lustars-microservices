namespace AuthAPI.App.Infrastructure
{
    using GreenPipes;
    using MassTransit;
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

                mt.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(rmq =>
                {
                    rmq.Host("rabbitmq", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    rmq.UseHealthCheck(provider);

                    rmq.Message<IRegisterAccountProfile>(m => m.SetEntityName("register-account-profile-exchange"));

                    rmq.ReceiveEndpoint("register-account-profile-queue", endpoint =>
                    {
                        endpoint.PrefetchCount = 20;

                        endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                        endpoint.Bind<IRegisterAccountProfile>();

                        endpoint.ConfigureConsumer<RegisterAccountConsumer>(provider);
                    });
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}
