namespace ProfileAPI.App.Infrastructure
{
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

            return services;
        }

        public static IServiceCollection AddPosgreSQLWithEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ProfileDBContext>((sp, opt) =>
                {          
                    opt.UseNpgsql(configuration.GetConnectionString("LustarsProfileDB"))
                        .UseInternalServiceProvider(sp);
                });

            return services;
        }
    }
}
