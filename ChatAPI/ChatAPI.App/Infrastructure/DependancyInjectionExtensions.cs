namespace ChatAPI.App.Infrastructure
{
    using ChatAPI.Data.Context;
    using ChatAPI.Data.Interfaces;
    using ChatAPI.Services.Services;
    using ChatAPI.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependancyInjectionExtensions
    {
        public static IServiceCollection AddDependancyInjectionResolver(this IServiceCollection services)
        {
            services.AddTransient<IChatDBContext, ChatDBContext>();
            services.AddTransient<IMessageService, MessageService>();

            return services;
        }
    }
}
