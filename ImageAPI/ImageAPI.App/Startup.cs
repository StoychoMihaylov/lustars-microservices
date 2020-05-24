namespace ImageAPI.App
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ImageAPI.App.Infrastructure;

    public class Startup
    {
        private readonly string apiCorsPolicy = "ApiCorsPolicy";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerDocument(); //Swagger
            services.AddDependanciInjectionResolver(); // DI
            services.AddCors(options =>
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(apiCorsPolicy);
            app.UseRouting();
            app.UseOpenApi(); //Swagger
            app.UseSwaggerUi3();
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);
            app.UseEndpoints(endpoints  => { endpoints.MapControllers(); });  
            app.UseStaticFiles(); // Enable static files
        }
    }
}
