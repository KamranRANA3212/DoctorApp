using DoctorApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoctorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private IUnitOfWork _uow;

        public DashBoardController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("get")]
        public async Task<IActionResult> Get()
        {
            var response = await _uow.DashBoard.GetDashBoardData();
            return Ok(response);
        }
    }
}