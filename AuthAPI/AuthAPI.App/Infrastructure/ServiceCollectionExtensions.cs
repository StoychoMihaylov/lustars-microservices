namespace AuthAPI.App.Infrastructure
{
    using System;
    using GreenPipes;
    using MassTransit;
    using System.Diagnostics;
    using AuthAPI.Data.Context;
    using AuthAPI.Data.Interfaces;
    using AuthAPI.Services.Services;
    using System.Collections.Generic;
    using AuthAPI.Messaging.Consumers;
    using AuthAPI.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependanciInjectionResolver(this IServiceCollection services)
        {
            services.AddTransient<IAuthDBContext, AuthDBContext>();
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }

        public static IServiceCollection AddPosgreSQLWithEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddEntityFrameworkNpgsql()
                .AddDbContext<AuthDBContext>((sp, opt) =>
                {
                    

                    if (Debugger.IsAttached)
                    {
                        opt.UseNpgsql(configuration.GetConnectionString("LustarsAuthDBDebug"))
                        .UseInternalServiceProvider(sp);
                    }
                    else
                    {
                        opt.UseNpgsql(configuration.GetConnectionString("LustarsAuthDBRelease"))
                           .UseInternalServiceProvider(sp);
                    }
                });
        }

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

                        endpoint.ConfigureConsumer<RegisterNewAccountConsumer>(provider);
                    });
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}
