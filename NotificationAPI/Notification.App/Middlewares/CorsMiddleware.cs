namespace Notification.App.Middlewares
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Builder;

    public class CorsMiddleware
    {
        private readonly RequestDelegate next;

        public CorsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue("Origin", out var originValue))
            {
                httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "x-requested-with");
                httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,OPTIONS");
                httpContext.Response.Headers.Add("Access-Control-Allow-Origin", originValue);

                if (httpContext.Request.Method == "OPTIONS")
                {
                    httpContext.Response.StatusCode = 200;
                    return httpContext.Response.WriteAsync(string.Empty);
                }
            }

            return this.next(httpContext);
        }
    }

    public static class CorsMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorsMiddleware>();
        }
    }
}
