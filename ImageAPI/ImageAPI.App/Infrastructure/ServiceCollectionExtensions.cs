namespace ImageAPI.App.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;

    using ImageAPI.Services.Services;
    using ImageAPI.Services.Interfaces;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependanciInjectionResolver(this IServiceCollection services)
        {
            services.AddTransient<IImageService, ImageService>();

            return services;
        }
    }
}
