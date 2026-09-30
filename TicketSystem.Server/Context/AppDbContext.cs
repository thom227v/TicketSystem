using Microsoft.EntityFrameworkCore;
using TicketSystem.Server.Models;

namespace TicketSystem.Server.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(IConfiguration config, DbContextOptionsBuilder options)
        {
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        }
    }

}