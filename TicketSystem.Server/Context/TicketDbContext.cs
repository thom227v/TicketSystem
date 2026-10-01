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
            using (TicketDbContext context = new TicketDbContext(_config))
            {
               return context.department.ToList();
            }
        }

        public Department? GetDepartmentById(int id)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return context.department.FirstOrDefault(x => x.id == id);
            }
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
                context.ticket.Add(new Ticket { title = request.Title, description = request.Description, submittedby = request.SubmittedBy, priority = request.Priority, category = request.Category });
                context.SaveChanges();
            }
        }

        public List<Ticket> GetAllTickets()
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return context.ticket.ToList();
            }
        }

        public List<ServiceAgreement> GetAllServiceAgreements()
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return context.serviceAgreement.ToList();
            }
        }

        public List<ServiceAgreement> GetServiceAgreements(Func<IQueryable<ServiceAgreement>, IQueryable<ServiceAgreement>> query)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return query(context.serviceAgreement).ToList();
            }
        }

        public ServiceAgreement? GetServiceAgreementById(int id)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return context.serviceAgreement.FirstOrDefault(x => x.id == id);
            }
        }

        public void CreateServiceAgreement(ServiceAgreement serviceAgreement)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                context.serviceAgreement.Add(serviceAgreement);
                context.SaveChanges();
            }
        }

        public List<Ticket> GetTickets()
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return context.ticket.ToList();
            }
        }
    }
}