using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Context;

namespace TicketSystem.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _userContext;

        public UserController(UserDbContext userContext)
        {
            _userContext = userContext;
        }

        [HttpGet("GetUserByUsername/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            return Ok(_userContext.Users.FirstOrDefault(u => u.UserName.Contains(username))?.UserName ?? "");
        }
    }
}
