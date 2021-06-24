using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Errors
{
    public class ExceptionResponse : ErrorResponse
    {
        public ExceptionResponse(int statusCode, string message = null, string stackTrace = null) : base(statusCode, message)
        {
            Detail = stackTrace;
        }

        public string Detail { get; set; }
    }
}