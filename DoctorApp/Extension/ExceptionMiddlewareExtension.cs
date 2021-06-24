using DoctorApp.Modals;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace DoctorApp.Extension
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var exception = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exception != null)
                    {
                        //log into db
                        await context.Response.WriteAsync(
                            new ErrorModel()
                            {
                                Status = context.Response.StatusCode,
                                Message = "Internal Server Error",
                            }.ToString());
                    }
                });
            });
        }
    }
}