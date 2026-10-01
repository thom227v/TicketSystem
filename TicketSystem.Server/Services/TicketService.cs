using TicketSystem.Server.Context;
using TicketSystem.Server.Models;

namespace TicketSystem.Server.Services
{
    public class TicketService
    {
        public TicketService(TicketDbContext context)
        {
            _context = context;
        }

        public TicketDbContext _context { get; }

        public void CreateTicket(TicketRequest request)
        {
            _context.CreateTicket(request);
        }   
    }
}
