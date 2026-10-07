using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Models.Auth.DTO;
using TicketSystem.Server.Services;

namespace TicketSystem.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AssignedTicketController : Controller
    {
        private AssignedTicketService _assignedTicketService { get; }
        public AssignedTicketController(AssignedTicketService assignedTicketService)
        {
            _assignedTicketService = assignedTicketService;
        }

        [HttpGet("GetAssignesByTicketId/{ticketId}")]
        public IActionResult GetAssignesByTicketId(int ticketId)
        {
            var assignes = _assignedTicketService.GetAssignesByTicketId(ticketId);
            return Ok(assignes);
        }

        [HttpPost("AssignTicket")]
        public IActionResult AssignTicket([FromBody] AssignTicketRequest request)
        {
            _assignedTicketService.AssignTicket(
                request.ticketId,
                request.username,
                this.User.Identity.Name);

            return Ok();
        }
    }
    public record AssignTicketRequest(int ticketId, string username);
}