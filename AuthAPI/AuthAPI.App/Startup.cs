namespace AuthAPI.App
{
    using AuthAPI.Data.Context;
    using AuthAPI.App.Infrastructure;
    using AuthAPI.Data.DBInitializer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerDocument(); //Swagger
            services.AddPosgreSQLWithEntityFramework(Configuration);
            services.AddDependancyInjectionResolver(); // DI
            services.AddMassTransitServiceBus(); // MassTransite Configuration
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AuthDBContext context)
        {
            app.UseRouting();
            app.UseOpenApi(); //Swagger
            app.UseSwaggerUi3();
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);

            DBInitializer.SeedDb(context); // Seed the DB on Start
        }
    }
}
