using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Services;

namespace TicketSystem.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;
        private readonly TicketService _ticketService;

        public DepartmentController(DepartmentService departmentService, TicketService ticketService)
        {
            _departmentService = departmentService;
            _ticketService = ticketService;
        }

        [HttpGet("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = _departmentService.GetDepartments();
            return Ok(departments);
        }

        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Invalid department data.");
            }
            _departmentService.CreateDepartment(name);
            return Ok();
        }
    }
}
