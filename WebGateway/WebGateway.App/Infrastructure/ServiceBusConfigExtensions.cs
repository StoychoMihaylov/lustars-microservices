namespace WebGateway.App.Infrastructure
{
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

                    rmq.Message<IRegisterAccountProfile>(m => m.SetEntityName("register-account-profile-exchange"));
                    rmq.Message<ICreateUserProfile>(m => m.SetEntityName("create-user-profile-exchange"));
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}
