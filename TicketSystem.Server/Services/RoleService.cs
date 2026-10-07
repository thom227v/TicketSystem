using Microsoft.AspNetCore.Identity;

namespace TicketSystem.Server.Services
{
    public class RoleService
    {
        // USE [Authorize(Roles = "Support")] in controllers
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task SyncRoles()
        {
            string[] roles =
            [
                "Support",
                "User"
            ];

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
