using Inv.Services.Api.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Inv.Services.Api.Middlewares
{
    public class ErrorResponseMiddleware
    {
        private RequestDelegate _next;

        public ErrorResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception e)
            {
                var response = new ApiResponse(false, new string[] { e.Message });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
