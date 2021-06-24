using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorApp.DTO_s;
using DoctorApp.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorApp.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ErrorResponse(code));
        }
    }
}