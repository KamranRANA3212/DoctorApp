using DoctorApp.DTO_s;
using DoctorApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private IUnitOfWork _uow;

        public PatientController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("GetPatient")]
        public async Task<IActionResult> GetPatient()
        {
            var user = HttpContext.User;

            if (user.HasClaim(z => z.Type == "userId"))
            {
                var response = await _uow.Patient.GetPatientAsync(user.Claims.FirstOrDefault(z => z.Type == "userId").Value);

                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpPost("GetPatients")]
        public async Task<IActionResult> GetPatients()
        {
            var response = await _uow.Patient.GetPatientsAsync();

            return Ok(response);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPatient([FromForm]PatientDTO patient)
        {
            var user = HttpContext.User;

            var response = await _uow.Patient.AddPatientAsync(patient);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdatePatient(PatientUpdateDTO patient)
        {
            var user = HttpContext.User;

            var response = await _uow.Patient.UpdatePatientAsync(user.Claims.FirstOrDefault(z => z.Type == "userId").Value, patient);

            return Ok(response);
        }

        [HttpPost("Home")]
        public async Task<IActionResult> GetHome(LocationDTO location)
        {
            var user = HttpContext.User;

            var response = await _uow.Patient.Home(location);

            return Ok(response);
        }

        [HttpPost("SearchDoctor")]
        public async Task<IActionResult> SearchDoctor([FromBody]string filter)
        {
            var response = await _uow.Patient.SearchDoctor(filter);

            return Ok(response);
        }

        [HttpPost("UpdateLocation")]
        public async Task<IActionResult> UpdateLocation(LocationDTO location)
        {
            var user = HttpContext.User;

            var response = await _uow.Patient.UpdateLatLng(location, user.Claims.FirstOrDefault(z => z.Type == "userId").Value);

            return Ok(response);
        }

        [HttpPost("MyAppointments")]
        public async Task<IActionResult> MyAppointments([FromBody]string filter)
        {
            var user = HttpContext.User;

            if (user.HasClaim(z => z.Type == "userId"))
            {
                var response = await _uow.Patient.MyAppointments(user.Claims.FirstOrDefault(z => z.Type == "userId").Value, filter);
                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpPost("CancelAppointment")]
        public async Task<IActionResult> CancelAppintment(CancelAppointmentDTO cancelAppointmentDTO)
        {
            var user = HttpContext.User;

            var response = await _uow.Patient.CancelAppointment(cancelAppointmentDTO);

            return Ok(response);
        }

        [HttpPost("MyPayments")]
        public async Task<IActionResult> MyPayments()
        {
            var user = HttpContext.User;

            var response = await _uow.Patient.MyPayemts(user.Claims.FirstOrDefault(z => z.Type == "userId").Value);

            return Ok(response);
        }
        //[HttpPost("DoctorRattings")]
      /*  public double DoctorRatting([FromBody]int doc)
        {
            double doctor = _uow.Patient.GetDoctorRatings(doc);
            return doctor;
            
        }*/
    }
}