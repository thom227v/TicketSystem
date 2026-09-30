using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Models.Auth.DTO;

namespace TicketSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> SignUp([FromBody] SignUpDTO signUpDTO)
        {
            string test = "";
            throw new NotImplementedException();
        }
    }
}
