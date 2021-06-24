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
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ReviewsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Reviews/5
        [HttpPost("GetReview")]
        public async Task<ActionResult> GetReview()
        {
            var user = HttpContext.User;

            return Ok(await _uow.Reviews.GetReviews(user.Claims.FirstOrDefault(z => z.Type == "userId").Value));
        }

        [HttpPost("AddReviews")]
        public async Task<ActionResult> AddReviews(Reviews reviews)
        {
            return Ok(await _uow.Reviews.PostReview(reviews));
        }

        //[HttpPost("GetNotifications")]
        //public async Task<ActionResult> GetNotifications()
        //{
        //    var user = HttpContext.User;

        //    if (user.HasClaim(z => z.Type == "userId"))
        //    {
        //        var doctors = await _uow.Reviews.GetTopRatedDoctors(user.Claims.FirstOrDefault(z => z.Type == "userId").Value);

        //        return Ok(doctors);
        //    }
        //    return Unauthorized();
        //}
    }
}