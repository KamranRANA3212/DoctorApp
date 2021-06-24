using DoctorApp.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpPost("GetNotifications")]
        public async Task<ActionResult<IEnumerable<Notifications>>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        //// GET: api/Notifications/5
        //[HttpPost("GetNotifications{id}")]
        //public async Task<ActionResult<Notifications>> GetNotifications(int id)
        //{
        //    var notifications = await _context.Notifications.FindAsync(id);

        //    if (notifications == null)
        //    {
        //        return NoContent();
        //    }

        //    return notifications;
        //}
    }
}