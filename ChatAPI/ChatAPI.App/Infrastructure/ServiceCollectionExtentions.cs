namespace ChatAPI.App.Infrastructure
{
    using System.Diagnostics;
    using ChatAPI.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddPosgreSQLWithEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ChatDBContext>((sp, opt) =>
                {
                    if (Debugger.IsAttached)
                    {
                        opt.UseNpgsql(configuration.GetConnectionString("LustarsChatDBDebug")) // DB connection in VS debugging
                            .UseInternalServiceProvider(sp);
                    }
                    else
                    {
                        opt.UseNpgsql(configuration.GetConnectionString("LustarsChatDBRelease")) // DB connection in docker-compose
                            .UseInternalServiceProvider(sp);
                    }
                });

            return services;
        }
    }
}
