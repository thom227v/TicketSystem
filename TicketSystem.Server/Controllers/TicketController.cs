using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Services;

namespace TicketSystem.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public TicketController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetDepartments")]
        public IActionResult GetDepartments()
        {
            var departments = _departmentService.GetDepartments();
            return Ok(departments);
        }

        [HttpPost("CreateDepartment")]
        public IActionResult CreateDepartment([FromBody] string name)
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
