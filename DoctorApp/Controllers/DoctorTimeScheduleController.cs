using DoctorApp.Interfaces;
using DoctorApp.Modals;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorTimeScheduleController : ControllerBase
    {
        private IUnitOfWork _uow;

        public DoctorTimeScheduleController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("TimeTable")]
        public async Task<IActionResult> Get()
        {
            var user = HttpContext.User;

            if (user.HasClaim(z => z.Type == "userId"))
            {
                var response = await _uow.DoctorTimeSchedule.GetTimeSchedule(user.Claims.FirstOrDefault(z => z.Type == "userId").Value);

                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Post(DoctorTimeSchedule model)
        {
            var response = await _uow.DoctorTimeSchedule.NewTimeSchedule(model);

            return Ok(response);
        }
    }
}