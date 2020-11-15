namespace Notification.App
{
    using System;
    using Notification.App.Hubs.Web;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Notification.App.Middlewares;
    using Notification.App.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerDocument(); // Swagger
            services.AddSignalR(s => 
                s.EnableDetailedErrors = true
            );
        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorsMiddleware();
            app.UseRouting();
            app.UseOpenApi(); //Swagger
            app.UseSwaggerUi3(); // Swagger
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);
            app.UseSignalR(routes => routes.MapHub<WebNotificationHub>("/webnotificationhub")); // Obsolete
        }
    }
}
