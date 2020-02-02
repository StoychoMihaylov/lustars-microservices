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
        private IConfiguration configuration { get; }

        private string ApiCorsPolicy = "ApiCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddHealthChecks(); // Healtchecks info for the container
            services.AddSwaggerDocument(); //Swagger

            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy(ApiCorsPolicy,
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            // DI
            services.AddSingleton<HttpClient>(new HttpClient());
            services.AddTransient<IAccountService, AccountService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(ApiCorsPolicy);
            app.UseOpenApi(); //Swagger
            app.UseSwaggerUi3();
            app.UseHealthChecks("/health", 9000); // Healtchecks info for the container
            app.UseHttpsRedirection();
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
