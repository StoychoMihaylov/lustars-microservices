namespace ProfileAPI.App.Infrastructure
{
    using System.Diagnostics;
    using ProfileAPI.Data.Context;
    using ProfileAPI.Data.Interfaces;
    using ProfileAPI.Services.Services;
    using ProfileAPI.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependanciInjectionResolver(this IServiceCollection services)
        {
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IProfileDBContext, ProfileDBContext>();
            services.AddTransient<IImageService, ImageService>();

            return services;
        }

        public static IServiceCollection AddPosgreSQLWithEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ProfileDBContext>((sp, opt) =>
                {
                    if (Debugger.IsAttached)
                    {
                        opt.UseNpgsql(configuration.GetConnectionString("LustarsProfileDBDebug"))
                           .UseInternalServiceProvider(sp);
                    }
                    else
                    {
                        opt.UseNpgsql(configuration.GetConnectionString("LustarsProfileBRelease"))
                           .UseInternalServiceProvider(sp);
                    }
                });

            return services;
        }
    }
}
