using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Models;
using TicketSystem.Server.Services;

namespace TicketSystem.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly DepartmentService _departmentService;
        private readonly TicketService _ticketService;

        public TicketController(DepartmentService departmentService, TicketService ticketService)
        {
            _departmentService = departmentService;
            _ticketService = ticketService;
        }    

        [HttpPost("CreateTicket")]
        public IActionResult CreateTicket([FromBody] TicketRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Description))
            {
                return BadRequest("Invalid ticket data.");
            }
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            request.SubmittedBy = currentUser.Identity?.Name ?? "Unknown";
            _ticketService.CreateTicket(request);
            return Ok();
        }
    }
}
