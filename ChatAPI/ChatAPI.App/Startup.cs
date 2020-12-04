namespace ChatAPI.App
{
    using ChatAPI.Data.Context;
    using ChatAPI.Data.DBInitializer;
    using ChatAPI.App.Infrastructure;
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

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerDocument(); //Swagger
            services.AddPosgreSQLWithEntityFramework(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ChatDBContext context)
        {
            app.UseRouting();
            app.UseOpenApi(); //Swagger
            app.UseSwaggerUi3(); // swagger
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);

            DBInitializer.SeedDb(context); // Seed the DB on Start
        }
    }
}
