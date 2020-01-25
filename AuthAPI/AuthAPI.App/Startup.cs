namespace AuthAPI.App
{
    using AuthAPI.App.Infrastructure;
    using AuthAPI.Data.Context;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
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

            // DB Connection init
            var optionsBuilder = new DbContextOptionsBuilder<AuthDBContext>(); 
            optionsBuilder.UseNpgsql($"Server=192.168.74.10;Port=5432;Database=DemoEFCore;User Id=postgres;Password = dbpass;");
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
