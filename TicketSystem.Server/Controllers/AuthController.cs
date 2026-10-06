using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Server.Models;
using TicketSystem.Server.Models.Auth;
using TicketSystem.Server.Models.Auth.DTO;
using TicketSystem.Server.Services;

namespace TicketSystem.Server.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, DepartmentService departmentService) : ControllerBase
    {
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO signUpDTO)
        {
            Department? department = departmentService.GetDepartmentById(signUpDTO.DepartmentId);
            if (department == null)
            {
                return BadRequest("Selected department doesnt exist");
            }

            ApplicationUser newIdentityUser = new ApplicationUser
            {
                UserName = signUpDTO.UserName,
                DepartmentId = department.id
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
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            ApplicationUser? user = await userManager.FindByNameAsync(signInDTO.UserName);
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
