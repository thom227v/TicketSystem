using Microsoft.EntityFrameworkCore;
using Npgsql;
using TicketSystem.Server.Models;

namespace TicketSystem.Server.Context
{
    public class TicketDbContext : DbContext
    {
        public DbSet<Department> department { get; set; }
        public DbSet<Ticket> ticket { get; set; }
        public DbSet<ServiceAgreement> serviceagreement { get; set; }
        public DbSet<AffectedParty> affectedparty { get; set; }
        public DbSet<Timelog> timelog { get; set; }
        public DbSet<TicketAssign> ticketAssign { get; set; }



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
            CreateAffectedUser(request);
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
                return context.serviceagreement.ToList();
            }
        }

        public List<ServiceAgreement> GetServiceAgreements(Func<IQueryable<ServiceAgreement>, IQueryable<ServiceAgreement>> query)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return query(context.serviceagreement).ToList();
            }
        }

        public ServiceAgreement? GetServiceAgreementById(int id)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return context.serviceagreement.FirstOrDefault(x => x.id == id);
            }
        }

        public void CreateServiceAgreement(ServiceAgreement serviceAgreement)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                context.serviceagreement.Add(serviceAgreement);
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

        public void CreateAffectedUser(TicketRequest request)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                bool ticketId = int.TryParse(context.ticket.FirstOrDefault(t => t.title == request.Title && t.description == request.Description)?.id.ToString(), out int parsedId);

                if (!ticketId)
                {
                    throw new Exception("affected party could not be created as ticket was not found");
                }

                IEnumerable<string> usernames = request.Usernames.Any()
                    ? request.Usernames
                    : string.IsNullOrWhiteSpace(request.Username)
                        ? Enumerable.Empty<string>()
                        : new[] { request.Username };

                context.affectedparty.AddRange(usernames
                    .Where(username => !string.IsNullOrWhiteSpace(username))
                    .Distinct()
                    .Select(username => new AffectedParty { ticketid = parsedId, userid = username }));
                context.SaveChanges();
            }
        }

        public IEnumerable<string> GetAffectedPartiesNameByTicketId(int id)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return affectedparty.Where(x => x.ticketid == id).Select(x => x.userid);
            }
        }

        public List<Timelog> GetAllTimeLogsByTicketAssignId(int ticketAssignId)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return context.timelog.Where(x => x.ticketassignid == ticketAssignId).ToList();
            }
        }
    }
}