using DoctorApp.Errors;
using DoctorApp.Modals;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;

namespace DoctorApp.CustomFilters
{
    public class LogException : IExceptionFilter
    {
        public async void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.ContentType = "application/json";

            var response = new ErrorResponse(context.HttpContext.Response.StatusCode);

            var json = JsonSerializer.Serialize(response);

            await context.HttpContext.Response.WriteAsync(json);
        }
    }
}