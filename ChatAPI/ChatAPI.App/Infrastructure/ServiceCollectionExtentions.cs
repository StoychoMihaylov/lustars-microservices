namespace ChatAPI.App.Infrastructure
{
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
                    opt.UseNpgsql(configuration.GetConnectionString("LustarsChatDB"))
                        .UseInternalServiceProvider(sp);
                });

            return services;
        }
    }
}
