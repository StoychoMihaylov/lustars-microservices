namespace AuthAPI.App
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using AuthAPI.Data.Context;
    using AuthAPI.Data.Interfaces;
    using AuthAPI.Services.Services;
    using AuthAPI.App.Infrastructure;
    using AuthAPI.Data.DBInitializer;
    using AuthAPI.Services.Interfaces;

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
            services.AddDependanciInjectionResolver(); // DI
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
