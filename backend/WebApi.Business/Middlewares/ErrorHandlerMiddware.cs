using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
            }catch (Exception e)
            {
                // context.Response.StatusCode = 400;
               await context.Response.WriteAsync(e.ToString());
            }
        }
    }
}