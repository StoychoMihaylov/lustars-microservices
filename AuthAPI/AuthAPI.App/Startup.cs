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
    using AuthAPI.Services.Interfaces;
   

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

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<AuthDBContext>((sp, opt) =>
                {
                    if (Debugger.IsAttached)
                    {
                        opt.UseNpgsql(Configuration.GetConnectionString("LustarsAuthDBDebug"))
                           .UseInternalServiceProvider(sp);
                    }
                    else
                    {
                        opt.UseNpgsql(Configuration.GetConnectionString("LustarsAuthDBRelease"))
                           .UseInternalServiceProvider(sp);
                    }
                });

            // DI
            services.AddTransient<IAuthDBContext, AuthDBContext>();
            services.AddTransient<IAccountService, AccountService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseOpenApi(); //Swagger
            app.UseSwaggerUi3();
            app.UseHealthChecks("/health", 9000); // Healtchecks info for the container
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);
        }
    }
}
