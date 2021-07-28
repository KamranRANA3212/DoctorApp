using DoctorApp.DTO_s;
using DoctorApp.Interfaces;
using DoctorApp.Modals;
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
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Perform all accont related operations
        /// </summary>
        private IUnitOfWork _uow;
       

        public AccountController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(SignIn model)
        {
            var response = await _uow.Account.Login(model);

            if (response.Status == "fail")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public IActionResult RefreshToken([FromBody] string token)
            {
            return Ok(_uow.Account.Refresh(token));
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm]SignUp signUp)
        {
            object response = await _uow.Account.Register(signUp);

            return Ok(response);
        }
       

        [HttpPost("Admins")]
        public async Task<IActionResult> GetAdmin()
        {
            var response = await _uow.Account.GetAdmins();

            return Ok(response);
        }
        
       

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAdmin()
        {
            var user = HttpContext.User;

            if (user.HasClaim(z => z.Type == "userId"))
            {
                var response = await _uow.Account.DeleteAdmin(user.Claims.FirstOrDefault(z => z.Type == "userId").Value);

                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpPost("BlockUnBlockAdmin")]
        public async Task<IActionResult> BlockUnBlockAdmin([FromBody]string email)
        {
            var response = await _uow.Account.BlockUnlockAdmin(email);

            return Ok(response);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChnagePassword chnagePassword)
        {
            var response = await _uow.Account.ChangePassword(chnagePassword);

            return Ok(response);
        }

        [HttpPost("AddCard")]
        public async Task<IActionResult> AddCard(Card card)
        {
            var user = HttpContext.User;

            card.User_Id = user.Claims.FirstOrDefault(z => z.Type == "userId").Value;

            var response = await _uow.Account.CreateCard(card);

            if (response.Status == "fail")
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _uow.Account.ForgotPassword(forgotPassword);

            if (response.Status == "fail")
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
       
    }
}