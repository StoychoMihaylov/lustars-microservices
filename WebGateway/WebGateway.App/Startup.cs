namespace WebGateway.App
{
    using System;
    using WebGateway.App.Hubs.Web;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using WebGateway.App.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        private readonly string apiCorsPolicy = "ApiCorsPolicy";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            //services.AddHealthChecks(); // Healtchecks info for the container
            services.AddSwaggerDocument(); // Swagger
            services.AddMemoryCache();
            services.AddCorsPolicy(apiCorsPolicy);
            services.AddDependancyInjectionResolver(); // DI
            services.AddMassTransitServiceBus(); // MassTransite Configuration
            services.AddSignalR(s => s.EnableDetailedErrors = true);
        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(apiCorsPolicy);
            app.UseRouting();
            app.UseOpenApi(); // Swagger
            app.UseSwaggerUi3(); // Swagger
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSignalR(routes => routes.MapHub<ChatHub>("/hubs/chat-messanger")); // Obsolete
        }
    }
}
