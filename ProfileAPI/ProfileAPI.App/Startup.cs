namespace ProfileAPI.App
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProfileAPI.Data.Context;
    using ProfileAPI.Data.Interfaces;
    using ProfileAPI.Services.Services;
    using ProfileAPI.App.Infrastructure;
    using ProfileAPI.Services.Interfaces;
    using ProfileAPI.Data.DBInitializer;

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

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ProfileDBContext>((sp, opt) =>
                {
                    if (Debugger.IsAttached)
                    {
                        opt.UseNpgsql(Configuration.GetConnectionString("LustarsProfileDBDebug"))
                           .UseInternalServiceProvider(sp);
                    }
                    else
                    {
                        opt.UseNpgsql(Configuration.GetConnectionString("LustarsProfileBRelease"))
                           .UseInternalServiceProvider(sp);
                    }
                });

            // DI
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IProfileDBContext, ProfileDBContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ProfileDBContext context)
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
