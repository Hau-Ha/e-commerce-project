using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Api.src.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ServiceException e)
            {
                await context.Response.WriteAsJsonAsync(new {
                    StatusCode= e.StatusCode,
                    Message = e.Message
                });
            }
            catch(DbUpdateException e)
            {
                await context.Response.WriteAsJsonAsync(new {
                    StatusCode= 500,
                    Message = e.InnerException!.Message
                });
            }
            catch (Exception e)
            {
                await context.Response.WriteAsJsonAsync(new {
                    StatusCode= 500,
                    Message = e.Message
                });
            }
        }
    }
}
