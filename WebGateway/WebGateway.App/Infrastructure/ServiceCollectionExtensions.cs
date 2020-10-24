namespace WebGateway.App.Infrastructure
{
    using MassTransit;
    using System.Net.Http;
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

        public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services)
        {
            return services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq");
                }));
            })
            .AddMassTransitHostedService();
        }
    }
}
