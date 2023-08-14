using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Business.Middlewares
{
    public class ErrorHandlerMiddware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // throw new NotImplementedException();
            try
            {
                await next(context);
            }
            catch (DbUpdateException e)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(e.Message);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
               await context.Response.WriteAsync(e.ToString());
            }
        }
    }
}