using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Server.Models;
using TicketSystem.Server.Services;

namespace TicketSystem.Server.Controllers
{
    [Authorize(Roles = "Support")]
    [Route("[controller]")]
    [ApiController]
    public class ServiceAgreementController : ControllerBase
    {
        private readonly ServiceAgreementService _serviceAgreementService;

        public ServiceAgreementController(ServiceAgreementService serviceAgreementService)
        {
            _serviceAgreementService = serviceAgreementService;
        }

        [HttpGet("GetServiceAgreements")]
        public async Task<IActionResult> GetServiceAgreements()
        {
            List<ServiceAgreement> serviceAgreements = _serviceAgreementService.GetServiceAgreements(
                q =>
                q.Include(x => x.department)
            );
            return Ok(serviceAgreements);
        }

        [HttpPost("CreateServiceAgreement")]
        public async Task<IActionResult> CreateServiceAgreement([FromBody] ServiceAgreementCreationResponse serviceAgreementResponse)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            string currentUsername = currentUser.Identity?.Name ?? "";
            if (string.IsNullOrWhiteSpace(currentUsername))
            {
                return BadRequest();
            }

            ServiceAgreement serviceAgreement = new ServiceAgreement
            {
                title = serviceAgreementResponse.Title,
                description = serviceAgreementResponse.Description,
                departmentid = serviceAgreementResponse.department.id,
                createdby = currentUsername,
                signedby = currentUsername
            };

            _serviceAgreementService.CreateServiceAgreement(serviceAgreement);
            return Ok();
        }
    }
}
