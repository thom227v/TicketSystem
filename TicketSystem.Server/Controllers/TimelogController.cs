using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Models;
using TicketSystem.Server.Services;

namespace TicketSystem.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TimelogController : Controller
    {
        private TimelogSerivce _timelogService { get; }
        public TimelogController(TimelogSerivce timelogSerivce)
        {
            _timelogService = timelogSerivce;
        }

        [HttpGet("GetTimelogsByTicketId/{ticketId}")]
        public ActionResult<IEnumerable<TimelogResponse>> GetTimelogsByTicketId(int ticketId)
        {
            var timelogs = _timelogService.GetTimelogsByTicketId(ticketId);
            return Ok(timelogs);
        }

        [HttpPost("AddTimelog")]
        public IActionResult AddTimelog([FromBody] TimelogRequest request)
        {
            request.AssigneName = this.User.Identity.Name;
            _timelogService.AddTimelog(request);
            return Ok();
        }
    }
}
