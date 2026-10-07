using TicketSystem.Server.Context;
using TicketSystem.Server.Models;

namespace TicketSystem.Server.Services
{
    public class TimelogSerivce
    {
        private TicketDbContext _context;

        public TimelogSerivce(TicketDbContext context)
        {
            _context = context;
        }

        public List<TimelogResponse> GetTimelogsByTicketId(int ticketId)
        {
            return _context.TimelogsByTicketId(ticketId);

        }

        public void AddTimelog(TimelogRequest request)
        {
            _context.AddTimelog(request);
        }
    }
}
