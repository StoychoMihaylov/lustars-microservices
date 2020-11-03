namespace AuthAPI.App.Infrastructure
{
    using AuthAPI.Data.Context;
    using AuthAPI.Data.Interfaces;
    using AuthAPI.Services.Services;
    using AuthAPI.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependancyInjectionExtentions
    {
        public static IServiceCollection AddDependancyInjectionResolver(this IServiceCollection services)
        {
            services.AddTransient<IAuthDBContext, AuthDBContext>();
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }
    }
}
