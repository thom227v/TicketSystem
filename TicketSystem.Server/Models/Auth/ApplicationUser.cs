using Microsoft.AspNetCore.Identity;

namespace TicketSystem.Server.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public int DepartmentId {  get; set; }
    }
}
