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
    public class AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : ControllerBase
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

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignUpDTO signInDTO)
        {
            IdentityUser? user = await userManager.FindByNameAsync(signInDTO.UserName);
            if (user == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, signInDTO.Password, true, false);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Username or password is incorrect");
            }
        }

        [HttpGet("UserInfo")]
        public async Task<IActionResult> UserInfo()
        {
            if (this.User.Identity != null && this.User.Identity.IsAuthenticated)
            {
                return Ok(new UserInfoDTO {UserName = this.User.Identity.Name ?? ""});
            }
            return BadRequest();
        }
    }
}
