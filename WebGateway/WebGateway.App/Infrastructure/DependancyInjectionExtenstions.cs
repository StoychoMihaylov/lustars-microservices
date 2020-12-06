namespace WebGateway.App.Infrastructure
{
    using System.Net.Http;
    using WebGateway.Services.Common;
    using WebGateway.Services.Services;
    using WebGateway.Services.Interfaces;
    using WebGateway.Messaging.Interfaces;
    using WebGateway.Messaging.MessagingServices;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependancyInjectionExtenstions
    {
        public static IServiceCollection AddDependancyInjectionResolver(this IServiceCollection services)
        {
            services.AddSingleton<HttpClient>(new HttpClient());
            services.AddTransient<StringContentSerializer>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IAccountBusService, AccountBusService>();
            services.AddTransient<IProfileBusService, ProfileBusService>();
            services.AddTransient<IUserImageService, UserImageService>();
            services.AddTransient<IChatMessangerService, ChatMessangerService>();

            return services;
        }
    }
}
