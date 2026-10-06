using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Server.Models;
using TicketSystem.Server.Models.Auth;

namespace TicketSystem.Server.Context
{
    public class UserDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Sets default schema for all tables mapped by EF Core
            modelBuilder.HasDefaultSchema("user");
        }
    }
}
