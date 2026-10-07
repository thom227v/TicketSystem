using TicketSystem.Server.Context;
using TicketSystem.Server.Models;

namespace TicketSystem.Server.Services
{
    public class AssignedTicketService
    {
        private TicketDbContext _context { get; }
        public AssignedTicketService(TicketDbContext context)
        {
            _context = context;
        }

        public List<AssginesResponse> GetAssignesByTicketId(int ticketId)
        {
            return _context.AssignesByTicketId(ticketId).ToList();
        }

        public void AssignTicket(int ticketId, string username, string assignedBy)
        {
            _context.AssignTicket(ticketId, username, assignedBy);
        }
    }
}