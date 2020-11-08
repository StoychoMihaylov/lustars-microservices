namespace Notification.App.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;

    public static class CorsPolicyExtensions
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, string apiCorsPolicy)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(apiCorsPolicy,
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
    }
}
