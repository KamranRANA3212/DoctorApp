using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Errors
{
    public class APIValidationErrorsResponse : ErrorResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public APIValidationErrorsResponse() : base(400)
        {
        }
    }
}