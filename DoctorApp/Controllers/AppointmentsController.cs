using DoctorApp.Interfaces;
using DoctorApp.Modals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoctorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public AppointmentsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Appointments
        [HttpPost("GetAppointments")]//web
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAppointments()
        {
            var response = await _uow.Appointment.GetAppintments();

            return Ok(response);
        }

        //// GET: api/Appointments/5
        //[HttpPost("GetAppointment")]
        //public async Task<ActionResult> GetAppointment([FromBody]string filter)
        //{
        //    var user = HttpContext.User;

        //    if (user.HasClaim(z => z.Type == "userId"))
        //    {
        //        var response = await _uow.Appointment.GetAppointment(user.Claims.FirstOrDefault(z => z.Type == "userId").Value, filter);

        //        return Ok(response);
        //    }
        //    return Unauthorized();
        //}

        [HttpPost("NewAppointment")]
        public async Task<ActionResult> Post(Appointment appointment)
        {
            var response = await _uow.Appointment.PostAppointment(appointment);

            if (response.Status == "fail")
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // DELETE: api/Appointments/5
        [HttpPost("DeleteAppointment")]
        public async Task<ActionResult<Appointment>> DeleteAppointment([FromBody]int appointmentId)
        {
            var user = HttpContext.User;

            if (user.HasClaim(z => z.Type == "userId"))
            {
                var response = await _uow.Appointment.DeleteAppointment(appointmentId);
                if (response.Status == "fail")
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            return Unauthorized();
        }
    }
}