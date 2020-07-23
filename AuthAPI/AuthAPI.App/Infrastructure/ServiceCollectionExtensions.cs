namespace AuthAPI.App.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using AuthAPI.Data.Context;
    using AuthAPI.Data.Interfaces;
    using AuthAPI.Services.Services;
    using AuthAPI.Services.Interfaces;

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
                    opt.UseNpgsql(configuration.GetConnectionString("LustarsAuthDB"))
                        .UseInternalServiceProvider(sp);
                });
        }
    }
}
