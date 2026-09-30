using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Models.Auth.DTO;

namespace TicketSystem.Server.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class AuthController(UserManager<IdentityUser> userManager) : ControllerBase
    {
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO signUpDTO)
        {
            IdentityUser newIdentityUser = new IdentityUser 
            {
                UserName = signUpDTO.UserName
            };
            IdentityResult result = await userManager.CreateAsync(newIdentityUser, signUpDTO.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            var identityErrorCodes = typeof(IdentityErrorDescriber)
                .GetMethods()
                .Where(m => m.ReturnType == typeof(IdentityError))
                .Select(m => m.Name)
                .ToHashSet();

            List<string> safeErrorDescriptions = result.Errors
            .Where(e => identityErrorCodes.Contains(e.Code))
            .Select(e => e.Description)
            .ToList();

            return BadRequest(safeErrorDescriptions);
        }
    }
}
