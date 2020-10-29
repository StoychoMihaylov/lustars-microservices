namespace WebGateway.App.Infrastructure
{
    using System;
    using MassTransit;
    using MessageExchangeContract;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceBusConfigExtensions
    {
        public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services)
        {
            return services.AddMassTransit(mt =>
            {
                mt.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(rmq =>
                {
                    rmq.Host("rabbitmq", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    rmq.UseHealthCheck(provider);

                    rmq.Message<IRegisterNewAccountMessage>(m => m.SetEntityName("register-account-exchange"));
                }));

                //mt.AddRequestClient<IRegisterNewAccountMessage>(TimeSpan.FromSeconds(30));

                //mt.AddRequestClient<IRegisterNewAccountRejection>(TimeSpan.FromSeconds(30));
            })
            .AddMassTransitHostedService();
        }
    }
}
