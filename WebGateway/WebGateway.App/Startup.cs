namespace WebGateway.App
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using WebGateway.App.Infrastructure;

    public class Startup
    {
        private readonly string apiCorsPolicy = "ApiCorsPolicy";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddHealthChecks(); // Healtchecks info for the container
            services.AddSwaggerDocument(); //Swagger
            services.AddMemoryCache();
            services.AddCorsPolicy(apiCorsPolicy);
            services.AddDependanciInjectionResolver(); // DI
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors(apiCorsPolicy);
            app.UseOpenApi(); //Swagger
            app.UseSwaggerUi3();
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
