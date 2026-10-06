using Microsoft.AspNetCore.Identity;

namespace TicketSystem.Server.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public Department? department {  get; set; }
    }
}
