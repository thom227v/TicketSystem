using Microsoft.EntityFrameworkCore;
using Npgsql;
using TicketSystem.Server.Models;

namespace TicketSystem.Server.Context
{
    public class TicketDbContext : DbContext
    {
        public DbSet<Department> department { get; set; }
        public DbSet<Ticket> ticket { get; set; }
        public DbSet<ServiceAgreement> serviceAgreement { get; set; }
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
                departments = context.department.ToList();
            }
            return departments;
        }

        public void CreateDepartment(string departmentName)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                context.department.Add(new Department { name = departmentName });
                context.SaveChanges();
            }
        }

        public void CreateTicket(TicketRequest request)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                context.ticket.Add(new Ticket { title = request.Title, description = request.Description, submittedBy = request.SubmittedBy, priority = request.Priority, category = request.Category });
                context.SaveChanges();
            }
        }

        public List<Ticket> GetAllTickets()
        {
            List<Ticket> tickets = new List<Ticket>();

            using (TicketDbContext context = new TicketDbContext(_config))
            {
                tickets = context.ticket.ToList();
            }

            return tickets;
        }

        public List<ServiceAgreement> GetAllServiceAgreements()
        {
            List<ServiceAgreement> serviceAgreements = new List<ServiceAgreement>();

            using (TicketDbContext context = new TicketDbContext(_config))
            {
                serviceAgreements = context.serviceAgreement.ToList();
            }

            return serviceAgreements;
        }

        public void CreateServiceAgreement(ServiceAgreement serviceAgreement)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                context.serviceAgreement.Add(serviceAgreement);
                context.SaveChanges();
            }
        }
    }
}