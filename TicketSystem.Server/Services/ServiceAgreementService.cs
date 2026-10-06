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

        public List<ServiceAgreement> GetAllServiceAgreements()
        {
            return _context.GetAllServiceAgreements();
        }

        public ServiceAgreement? GetServiceAgreementById(int id)
        {
            return _context.GetServiceAgreementById(id);
        }

        public List<ServiceAgreement> GetServiceAgreements(Func<IQueryable<ServiceAgreement>, IQueryable<ServiceAgreement>> query)
        {
            return _context.GetServiceAgreements(query);
        }

        public void CreateServiceAgreement(ServiceAgreement serviceAgreement)
        {
            _context.CreateServiceAgreement(serviceAgreement);
        }
    }
}
