namespace WebGateway.Messaging
{
    using System;
    using MassTransit;
    using MassTransit.RabbitMqTransport;

    public static class BusConfigurator
    {
        //public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        //{
        //    return Bus.Factory.CreateUsingRabbitMq(cfg =>
        //    {
        //        var host = cfg.Host(new Uri(RabbitMqConstants.RabbitMqUri), hst =>
        //        {
        //            hst.Username(RabbitMqConstants.UserName);
        //            hst.Password(RabbitMqConstants.Password);
        //        });

        //        registrationAction?.Invoke(cfg, host);

        //        cfg.Durable = true;
        //    });
        //}
    }
}
