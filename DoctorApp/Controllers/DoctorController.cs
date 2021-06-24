using DoctorApp.DTO_s;
using DoctorApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        private IUnitOfWork _uow;

        public DoctorController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("GetDoctors")]
        public async Task<IActionResult> GetDoctors() //web
        {
            var doctors = await _uow.Doctor.GetDoctors();

            var response = DoctorApp.Utilities.Response<DoctorDTO>.GenerateResponse("success", doctors, null, new List<string>(), "");

            return Ok(response);
        }

        /// <summary>
        /// api call at doctar app dashboard return all today appointments
        /// </summary>
        /// <returns></returns>
        [HttpPost("Home")]
        public async Task<ActionResult> GetTodayAppointments()
        {
            var user = HttpContext.User;

            if (user.HasClaim(z => z.Type == "userId"))
            {
                return Ok(await _uow.Doctor.Home(user.Claims.FirstOrDefault(z => z.Type == "userId").Value));
            }
            return Unauthorized();
        }

        [HttpPost("GetDoctor")]
        public async Task<IActionResult> Get([FromBody] int id)
        {
            var doctors = await _uow.Doctor.GetDoctor(id);

            var response = DoctorApp.Utilities.Response<DoctorDTO>.GenerateResponse("success", new List<DoctorDTO>(), doctors, new List<string>(), "");

            return Ok(response);
        }

        [HttpPost("AddDoctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(DoctorDTO doctorDTO)
        {
            var user = HttpContext.User;

            var response = _uow.Doctor.AddDoctor(user.Claims.FirstOrDefault(z => z.Type == "userId").Value, doctorDTO);

            if (response.Status == "fail")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Varify Doctor if he has valid licence no
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Verify")]
        public async Task<IActionResult> Verify([FromBody]int RegistrationCode)
        {
            var doctor = await _uow.Doctor.VerifyLicenceNumber(RegistrationCode);

            return Ok(doctor);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(DoctorUpdateDTO dto)
        {
            var user = HttpContext.User;

            var response = await _uow.Doctor.UpdateDoctor(user.Claims.FirstOrDefault(z => z.Type == "userId").Value, dto);
            if (response.Status == "fail")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("ToggleOnline")]
        public async Task<IActionResult> ToggleOnline()
        {
            var user = HttpContext.User;

            var status = await _uow.Doctor.ToggleOnline(user.Claims.FirstOrDefault(z => z.Type == "userId").Value);

            return Ok(status);
        }

        [HttpPost("MyAppointments")]
        public async Task<ActionResult> MyAppointments([FromBody]string filter)
        {
            var user = HttpContext.User;

            var response = await _uow.Doctor.GetMyBookings(user.Claims.FirstOrDefault(z => z.Type == "userId").Value, filter);

            return Ok(response);
        }

        [HttpPost("MyWallet")]
        public async Task<ActionResult> MyWallet()
        {
            var user = HttpContext.User;

            return Ok(await _uow.Doctor.GetMyWallet(user.Claims.FirstOrDefault(z => z.Type == "userId").Value));
        }

        [HttpPost("UpdateLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLocation(LocationDTO location)
        {
            var user = HttpContext.User;

            if (user.HasClaim(z => z.Type == "userId"))
            {
                var response = await _uow.Doctor.UpdateLatLng(location, user.Claims.FirstOrDefault(z => z.Type == "userId").Value);

                if (response.Status == "fail")
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpPost("CancelAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelAppointment(CancelAppointmentDTO cancelAppointment)
        {
            var response = await _uow.Doctor.CancelAppointment(cancelAppointment);

            if (response.Status == "fail")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("ChangeStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDTO changeStatusDTO)
        {
            var response = await _uow.Doctor.ChangeStatus(changeStatusDTO);

            if (response.Status == "fail")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}