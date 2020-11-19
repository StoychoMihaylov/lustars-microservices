namespace WebGateway.App.Infrastructure
{
    using MassTransit;
    using MassTransit.MessageData;
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
                    rmq.UseMessageData(new InMemoryMessageDataRepository());

                    // AuthAPI
                    rmq.Message<IRegisterAccountProfile>(m => m.SetEntityName("register-account-profile-exchange"));
                    rmq.Message<IDeleteAccountProfile>(m => m.SetEntityName("delete-account-profile-exchange"));

                    // ProfileAPI
                    rmq.Message<ICreateUserProfile>(m => m.SetEntityName("create-user-profile-exchange"));
                    rmq.Message<IUpdateUserProfile>(m => m.SetEntityName("update-user-profile-exchange"));
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}
