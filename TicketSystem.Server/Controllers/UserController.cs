using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Context;
using TicketSystem.Server.Models.Auth;

namespace TicketSystem.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _userContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserDbContext userContext, UserManager<ApplicationUser> userManager)
        {
            _userContext = userContext;
            _userManager = userManager;
        }

        [HttpGet("GetUserByUsername/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            return Ok(_userContext.Users.FirstOrDefault(u => u.UserName.Contains(username))?.UserName ?? "");
        }

        [HttpGet("GetAllSupportUsers")]
        public async Task<IActionResult> GetAllSupportUsers()
        {
            var supportUsers = await _userManager.GetUsersInRoleAsync("Support");

            List<SupportUserRequest> supportUserRequests = supportUsers.Select(u => new SupportUserRequest(u.Id, u.UserName)).ToList();

            return Ok(supportUserRequests);
        }   
    }
    public record SupportUserRequest(string Id, string Username);
}
