using TicketSystem.Server.Models;

namespace TicketSystem.Server.Context
{
    public class TicketDbContext
    {
        private readonly IConfiguration _config;
        public TicketDbContext(IConfiguration config)
        {
            _config = config;       
        }

        public List<Department> GetAllDepartments()
        {

        }

    }
}
