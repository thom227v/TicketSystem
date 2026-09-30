using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Models.Auth.DTO;

namespace TicketSystem.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("SignUp")]
        public IActionResult SignUp([FromBody] SignUpDTO signUpDTO)
        {
            return Ok();
        }
    }
}
