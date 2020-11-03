namespace ProfileAPI.App.Infrastructure
{
    using ProfileAPI.Data.Context;
    using ProfileAPI.Data.Interfaces;
    using ProfileAPI.Services.Services;
    using ProfileAPI.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependancyInjectionResolver(this IServiceCollection services)
        {
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IProfileDBContext, ProfileDBContext>();
            services.AddTransient<IImageService, ImageService>();

            return services;
        }
    }
}
