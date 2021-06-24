using DoctorApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoctorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        private IUnitOfWork _uow;

        public SpecialityController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("GetSpeciality")]
        public async Task<IActionResult> Get()
        {
            var response = await _uow.Speciality.GetSpecialities();

            return Ok(response);
        }

        [HttpPost("SearchSpecialityByKeyword")]
        public async Task<IActionResult> SearchSpecialityByKeyword([FromBody] string value)
        {
            var response = await _uow.Speciality.SearchSpeciality(value);

            return Ok(response);
        }
    }
}