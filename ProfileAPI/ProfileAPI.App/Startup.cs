namespace ProfileAPI.App
{
    using AutoMapper;
    using ProfileAPI.Data.Context;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using ProfileAPI.App.Infrastructure;
    using ProfileAPI.Data.DBInitializer;
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
            services.AddControllers();
            services.AddSwaggerDocument(); // Swagger
            services.AddPosgreSQLWithEntityFramework(Configuration);
            services.AddDependancyInjectionResolver(); // DI
            services.AddAutoMapper(typeof(Startup));
            services.AddMassTransitServiceBus(); // MassTransite Configuration
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProfileDBContext context)
        {
            app.UseRouting();
            app.UseOpenApi(); // Swagger
            app.UseSwaggerUi3(); // Swagger
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);

            DBInitializer.SeedDb(context); // Seed the DB on Start
        }
    }
}
