namespace AuthAPI.App
{
    using AuthAPI.Data.Context;
    using AuthAPI.Data.Interfaces;
    using AuthAPI.Services.Services;
    using AuthAPI.App.Infrastructure;
    using AuthAPI.Services.Interfaces;
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

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<AuthDBContext>(opt =>
                    opt.UseNpgsql(Configuration.GetConnectionString("LustarsAuthDB")));

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
            app.UseHttpsRedirection();
            app.UseControllerEndpoints();
            app.UseExceptionHandling(env);
        }
    }
}
