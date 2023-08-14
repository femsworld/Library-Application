using Microsoft.AspNetCore.Http;

namespace WebApi.Business.Middlewares
{
    public class LoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // await context.Response.WriteAsync("request failed");
            await next(context);
        }
    }
}