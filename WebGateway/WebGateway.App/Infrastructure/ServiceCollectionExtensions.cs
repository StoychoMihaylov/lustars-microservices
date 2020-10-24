namespace WebGateway.App.Infrastructure
{
    using System;
    using GreenPipes;
    using MassTransit;
    using System.Net.Http;
    using System.Collections.Generic;
    using WebGateway.Services.Common;
    using WebGateway.Services.Services;
    using WebGateway.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependanciInjectionResolver(this IServiceCollection services)
        {
            services.AddSingleton<HttpClient>(new HttpClient());
            services.AddTransient<StringContentSerializer>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProfileService, ProfileService>();

            return services; 
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, string apiCorsPolicy)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(apiCorsPolicy,
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services, List<Type> consumers)
        {
            return services.AddMassTransit(mt =>
            {
                consumers.ForEach(consumer => mt.AddConsumer(consumer));

                mt.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(rmq =>
                {
                    rmq.Host("rabbitmq", host => 
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    rmq.UseHealthCheck(provider);

                    consumers.ForEach(consumer => rmq.ReceiveEndpoint(consumer.FullName, endpoint =>
                    {
                        endpoint.PrefetchCount = 6;
                        endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                        endpoint.ConfigureConsumer(provider, consumer);
                    }));
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}
