using Microsoft.EntityFrameworkCore;
using Npgsql;
using TicketSystem.Server.Models;

namespace TicketSystem.Server.Context
{
    public class TicketDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        private readonly IConfiguration _config;
        public TicketDbContext(IConfiguration config)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_config.GetConnectionString("DefaultConnection"));
        }

        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();

            using (TicketDbContext context = new TicketDbContext(_config))
            {
                var departmentsFromDb = context.Departments.ToList();
                foreach (var department in departmentsFromDb)
                {
                    departments.Add(department);
                }
            }
            return departments;
        }

    }
}
