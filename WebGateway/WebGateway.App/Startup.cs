namespace WebGateway.App
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using WebGateway.Services.Services;
    using WebGateway.App.Infrastructure;
    using WebGateway.Services.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks(); // Healtchecks info for the container
            services.AddSwaggerDocument(); //Swagger

            // DI
            services.AddSingleton<HttpClient>(new HttpClient());
            services.AddTransient<IAccountService, AccountService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseOpenApi(); //Swagger
            app.UseSwaggerUi3();
            app.UseHealthChecks("/health", 9000); // Healtchecks info for the container
            app.UseHttpsRedirection();
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);
        }
    }
}
