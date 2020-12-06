namespace ProfileAPI.App.Infrastructure
{
    using ProfileAPI.Data.Context;
    using ProfileAPI.Data.Interfaces;
    using ProfileAPI.Services.Services;
    using ProfileAPI.Services.Interfaces;
    using ProfileAPI.Messaging.Interfaces;
    using ProfileAPI.Messaging.MessagingServices;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependancyInjectionResolver(this IServiceCollection services)
        {
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IProfileDBContext, ProfileDBContext>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<INotificationBusService, NotificationBusService>();
            services.AddTransient<IChatService, ChatService>();

            return services;
        }
    }
}
