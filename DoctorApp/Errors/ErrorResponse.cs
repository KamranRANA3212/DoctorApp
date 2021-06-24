using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Errors
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        private string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                404 => "Not found",
                401 => "Unautorized",
                405 => "Method not allowed",
                500=> "Server error",
                _ => null
            };
        }
    }
}