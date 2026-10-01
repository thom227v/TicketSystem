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
            List<ServiceAgreement> serviceAgreements = _serviceAgreementService.GetServiceAgreements();
            return Ok(serviceAgreements);
        }
    }
}
