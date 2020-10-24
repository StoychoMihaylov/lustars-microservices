namespace WebGateway.App
{
    using System;
    using System.Collections.Generic;
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
            services.AddDependanciInjectionResolver(); // DI
            services.AddMassTransitServiceBus(new List<Type>()); // MassTransite Configuration
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(apiCorsPolicy);
            app.UseRouting();
            app.UseOpenApi(); //Swagger
            app.UseSwaggerUi3();
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
