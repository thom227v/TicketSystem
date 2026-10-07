using Microsoft.EntityFrameworkCore;
using TicketSystem.Server.Models;
using TicketSystem.Server.Models.Auth.DTO;


namespace TicketSystem.Server.Context
{
    public class TicketDbContext : DbContext
    {
        public DbSet<Department> department { get; set; }
        public DbSet<Ticket> ticket { get; set; }
        public DbSet<ServiceAgreement> serviceagreement { get; set; }
        public DbSet<AffectedParty> affectedparty { get; set; }
        public DbSet<Timelog> timelog { get; set; }
        public DbSet<TicketAssign> ticketassign { get; set; }
        public DbSet<Stage> stage { get; set; }
        public DbSet<Priority> priority { get; set; }
        public DbSet<Category> category { get; set; }


        private readonly IConfiguration _config;
        public TicketDbContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_config.GetConnectionString("DefaultConnection"));
        }

        public List<Stage> GetAllStages()
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return context.stage.ToList();
            }
        }

        public List<Priority> GetAllPriorities()
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return context.priority.ToList();
            }
        }

        public List<Category> GetAllCategories()
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return context.category.ToList();
            }
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
                context.ticket.Add(new Ticket { title = request.Title, description = request.Description, submittedby = request.SubmittedBy, priorityid = request.PriorityId, categoryid = request.CategoryId, stageid = 1 });
                context.SaveChanges();
            }
            if (request.Usernames.Count > 0)
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
                bool ticketId = int.TryParse(context.ticket.FirstOrDefault(t => t.id == request.id)?.id.ToString(), out int parsedId);

                if (!ticketId)
                {
                    throw new Exception("affected party could not be created as ticket was not found");
                }

                context.affectedparty.RemoveRange(context.affectedparty.Where(x => x.ticketid == parsedId));
                
                context.affectedparty.AddRange(request.Usernames
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
                return context.affectedparty
                    .Where(x => x.ticketid == id)
                    .Select(x => x.userid)
                    .ToList();
            }
        }

        public List<Timelog> GetAllTimeLogsByTicketAssignId(int ticketAssignId)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                return context.timelog.Where(x => x.ticketassignid == ticketAssignId).ToList();
            }
        }

        public void UpdateTicket(TicketRequest request)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                Ticket ticket = context.ticket.FirstOrDefault(t => t.id == request.id);
                if (ticket == null)
                {
                    throw new Exception("Ticket not found for update.");
                }
             
                ticket.priorityid = request.PriorityId;
                ticket.categoryid = request.CategoryId;
                ticket.stageid = request.StageId;
                context.SaveChanges();
            }
            CreateAffectedUser(request);
        }

        public List<AssginesResponse> AssignesByTicketId(int ticketId)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                var assignes = context.ticketassign
                    .Where(x => x.ticketid == ticketId)
                    .Select(x => new AssginesResponse
                    {
                        Id = x.id,
                        Name = x.worker
                    })
                    .ToList();
                return assignes;
            }
        }

        public void AssignTicket(int ticketId, string username, string assignedBy)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                if (!context.ticketassign.Any(x => x.ticketid == ticketId && x.worker == username))
                {
                    context.ticketassign.Add(new TicketAssign { ticketid = ticketId, worker = username, assignedby = assignedBy, creationdatetime = DateTime.UtcNow });
                    context.SaveChanges();
                }
            }
        }

        public List<TimelogResponse> TimelogsByTicketId(int ticketId)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                var timelogs = context.timelog
                    .Join(
                        context.ticketassign,
                        timelog => timelog.ticketassignid,
                        assignment => assignment.id,
                        (timelog, assignment) => new { timelog, assignment })
                    .Where(x => x.assignment.ticketid == ticketId)
                    .Select(x => new TimelogResponse
                    {
                        Id = x.timelog.id,
                        AssignedTo = x.assignment.worker,
                        TotalHoursSpent = x.timelog.totalhoursspent,
                        Description = x.timelog.description,
                        CreationDate = x.timelog.creationdate
                    })
                    .ToList();
                return timelogs;
            }
        }

        public void AddTimelog(TimelogRequest request)
        {
            using (TicketDbContext context = new TicketDbContext(_config))
            {
                TicketAssign? ticketAssign = context.ticketassign.FirstOrDefault(x => x.ticketid == request.TicketId && x.worker == request.AssigneName);
                if (ticketAssign == null)
                {
                    throw new Exception("Ticket assignment not found for the given ID and username.");
                }
                Timelog timelog = new Timelog
                {
                    ticketassignid = ticketAssign.id,
                    totalhoursspent = request.TimeSpent,
                    description = request.Description,
                    creationdate = DateTime.UtcNow
                };
                context.timelog.Add(timelog);
                context.SaveChanges();
            }
        }
    }
}