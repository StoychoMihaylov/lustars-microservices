namespace AuthAPI.App.Infrastructure
{
    using System.Diagnostics;
    using AuthAPI.Data.Context;
    using AuthAPI.Data.Interfaces;
    using AuthAPI.Services.Services;
    using AuthAPI.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependanciInjectionResolver(this IServiceCollection services)
        {
            services.AddTransient<IAuthDBContext, AuthDBContext>();
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }

        public static IServiceCollection AddPosgreSQLWithEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddEntityFrameworkNpgsql()
                .AddDbContext<AuthDBContext>((sp, opt) =>
                {
                    

                    if (Debugger.IsAttached)
                    {
                        opt.UseNpgsql(configuration.GetConnectionString("LustarsAuthDBDebug"))
                        .UseInternalServiceProvider(sp);
                    }
                    else
                    {
                        opt.UseNpgsql(configuration.GetConnectionString("LustarsAuthDBRelease"))
                           .UseInternalServiceProvider(sp);
                    }
                });
        }
    }
}
