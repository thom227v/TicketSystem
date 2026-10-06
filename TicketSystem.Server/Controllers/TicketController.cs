using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Models;
using TicketSystem.Server.Models.DTOs;
using TicketSystem.Server.Services;

namespace TicketSystem.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }    

        [HttpPost("CreateTicket")]
        public IActionResult CreateTicket([FromBody] TicketRequest request)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            request.SubmittedBy = currentUser.Identity?.Name ?? "Unknown";
            _ticketService.CreateTicket(request);
            return Ok();
        }

        [HttpGet("GetTickets")]
        public IActionResult GetTickets()
        {
            List<TicketTableDTO> tickets = _ticketService.GetTicketsForTable();
            return Ok(tickets);
        }
    }
}
