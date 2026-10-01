using TicketSystem.Server.Context;
using TicketSystem.Server.Models;

namespace TicketSystem.Server.Services
{
    public class ServiceAgreementService
    {
        private readonly TicketDbContext _context;
        public ServiceAgreementService(TicketDbContext context)
        {
            _context = context;
        }

        public List<ServiceAgreement> GetServiceAgreements()
        {
            return _context.GetAllServiceAgreements();
        }
    }
}
