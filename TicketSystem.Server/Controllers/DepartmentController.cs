using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Models;
using TicketSystem.Server.Services;

namespace TicketSystem.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = _departmentService.GetDepartments();
            return Ok(departments);
        }

        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentCreationResponse createDepartment)
        {
            if (string.IsNullOrWhiteSpace(createDepartment.name))
            {
                return BadRequest("Invalid department data.");
            }
            _departmentService.CreateDepartment(createDepartment.name);
            return Ok();
        }
    }
}
