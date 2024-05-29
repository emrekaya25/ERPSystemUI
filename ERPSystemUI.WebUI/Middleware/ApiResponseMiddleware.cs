using ERPSystemUI.Model.CustomException;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Threading.Tasks;

namespace ERPSystemUI.WebUI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex.GetType()==typeof(UnauthorizedException))
                {
                    httpContext.Response.Redirect("/Admin/ExtraPages/ErrorPages/Unauthorized.html");
                }
                else if (ex.GetType()==typeof(NotFoundException))
                {
                    httpContext.Response.Redirect("/Admin/ExtraPages/ErrorPages/NotFound.html");
                }
                else if (ex.GetType() == typeof(BadGatewayException))
                {
                    httpContext.Response.Redirect("/Admin/ExtraPages/ErrorPages/BadGateway.html");
                }
                else if (ex.GetType() == typeof(InternalServerErrorException))
                {
                    httpContext.Response.Redirect("/Admin/ExtraPages/ErrorPages/InternalServerError.html");
                }
                else if (ex.GetType() == typeof(GatewayTimeoutException))
                {
                    httpContext.Response.Redirect("/Admin/ExtraPages/ErrorPages/GatewayTimeout.html");
                }
                else if(ex.GetType() == typeof(ForbiddenException))
                {
                    httpContext.Response.Redirect("/Admin/ExtraPages/ErrorPages/Forbidden.html");
                }

            }
            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ApiResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiResponseMiddleware>();
        }
    }
}
