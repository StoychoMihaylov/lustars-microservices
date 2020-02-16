namespace ProfileAPI.App
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProfileAPI.App.Infrastructure;
    using ProfileAPI.Data.Context;

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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseOpenApi(); //Swagger
            app.UseSwaggerUi3();
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);
        }
    }
}
